using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Swappler.Database;
using Swappler.Models;
using Swappler.Models.Status;
using Swappler.Services.Interfaces;
using Swappler.Utilities;

namespace Swappler.Services
{
    ///
    /// <summary>Service class for Swap requests.</summary>
    ///
    public class SwapRequestService : Service<SwapRequest, SwapplerSqliteContext>, ISwapRequestService
    {
        public SwapRequestStatus Add(SwapRequest swapRequest)
        {
            try
            {
                Context.Entry(swapRequest).State = EntityState.Added;
                Context.SaveChanges();
            }
            catch (Exception exception)
            {
                Logger.Write(LogType.Exception, Logger.ExceptionMessage(exception));

                return SwapRequestStatus.Error;
            }

            return SwapRequestStatus.Added;
        }

        public SwapRequestStatus Remove(SwapRequest swapRequest)
        {
            try
            {
                Context.SwapRequests.Attach(swapRequest);
                Context.SwapRequests.Remove(swapRequest);
                Context.SaveChanges();
            }
            catch (Exception e)
            {
                Logger.Write(LogType.Exception, e.Message + "\n" + e.StackTrace);
                return SwapRequestStatus.Error;
            }

            return SwapRequestStatus.Removed;
        }

        public SwapRequestStatus Update(SwapRequest swapRequest, params string[] updateFields)
        {
            try
            {
                if (updateFields == null || updateFields.Length == 0)
                {
                    Context.SwapRequests.Attach(swapRequest);
                    Context.Entry(swapRequest).State = EntityState.Modified;
                }
                else
                {
                    Context.SwapRequests.Attach(swapRequest);
                    foreach (string updateField in updateFields)
                    {
                        Context.Entry(swapRequest).Property(updateField).IsModified = true;
                    }
                }
                Context.SaveChanges();
            }
            catch (Exception e)
            {
                Logger.Write(LogType.Exception, e.Message + "\n" + e.StackTrace);
                return SwapRequestStatus.Error;
            }

            return SwapRequestStatus.Updated;
        }

        public List<SwapRequest> FindWhere(Func<SwapRequest, bool> wherePredicate)
        {
            try
            {
                var swapRequests = Context.SwapRequests.Where(wherePredicate);
                return swapRequests.ToList();
            }
            catch (Exception e)
            {
                Logger.Write(LogType.Exception, e.Message + "\n" + e.StackTrace);
                return null;
            }
        }

        public SwapRequest SendRequest(SwapItem requestedSwapItem, User requestorUser, SwapItem swapItemOffer, DateTime dateCreated, int? moneyOffer)
        {
            SwapRequest swapRequest = new SwapRequest
            {
                Guid = Guid.NewGuid(),
                Active = true,
                Date = dateCreated,
                UserId = requestorUser.UserId,
                MoneyOffer = moneyOffer,
                SwapItemId = requestedSwapItem.SwapItemId,
                SwapItemOfferId = (swapItemOffer == null ? (long?)null : swapItemOffer.SwapItemId)
            };

            if (Add(swapRequest)==SwapRequestStatus.Added)
            {
                // TODO: Notify through parse framework
                return swapRequest;
            }

            return null;
        }

        public void AcceptRequest(SwapRequest swapRequest)
        {
            try
            {
                SwapItem requestedSwapItem = new SwapItem()
                {
                    SwapItemId = swapRequest.SwapItemId
                };

                Context.SwapItems.Attach(requestedSwapItem);
                requestedSwapItem.Swapped = true;
                Context.Entry(requestedSwapItem).Property("Swapped").IsModified = true;

                Context.SaveChanges();

                // TODO: Notify through parse framework
            }
            catch (Exception exception)
            {
                Logger.Write(LogType.Exception, Logger.ExceptionMessage(exception));
            }
        }

        public void DeclineRequest(SwapRequest swapRequest)
        {
            try
            {
                Context.SwapRequests.Attach(swapRequest);
                swapRequest.Active = false;
                Context.Entry(swapRequest).Property("Active").IsModified = true;

                Context.SaveChanges();

                // TODO: Notify through parse framework
            }
            catch (Exception exception)
            {
                Logger.Write(LogType.Exception, Logger.ExceptionMessage(exception));
            }
        }

        public List<SwapRequest> FindRequestsByUser(string username)
        {
            try
            {
                var swapRequests = from swapRequest in Context.SwapRequests
                    join user in Context.Users on swapRequest.UserId equals user.UserId
                    where user.Username == username
                    select swapRequest;

                return swapRequests.ToList();
            }
            catch (Exception exception)
            {
                Logger.Write(LogType.Exception, Logger.ExceptionMessage(exception));
                return null;
            }
        }
    }
}
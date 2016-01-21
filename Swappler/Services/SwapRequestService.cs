using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
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

        public SwapRequest FindByGuid(Guid guid)
        {
            try
            {
                var swapRequests = FindWhere(sr => sr.Guid == guid);

                var swapRequest = swapRequests.FirstOrDefault();

                return swapRequest;
            }
            catch (Exception exception)
            {
                Logger.Write(LogType.Exception, exception.Message);
                return null;
            }
        }

        public List<SwapRequest> FindRequestsByUser(string username)
        {
            try
            {
                var swapRequests = from swapRequest in Context.SwapRequests
                                   join user in Context.Users on swapRequest.RequestorUserId equals user.UserId
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

        public List<SwapRequest> FindUnreadByUser(User user)
        {
            try
            {
                var swapRequests = from swapRequest in Context.SwapRequests
                                   where swapRequest.SwapItem.UserId == user.UserId &&
                                         swapRequest.Read == false
                                   select swapRequest;

                return swapRequests.ToList();
            }
            catch (Exception exception)
            {
                Logger.Write(LogType.Exception, Logger.ExceptionMessage(exception));
                return null;
            }
        }

        public SwapRequest SendRequest(SwapItem requestedSwapItem, User requestorUser, SwapItem swapItemOffer, int? moneyOffer, DateTime dateCreated)
        {
            SwapRequest swapRequest = new SwapRequest
            {
                Guid = Guid.NewGuid(),
                Date = dateCreated,
                SwapItemId = requestedSwapItem.SwapItemId,
                RequestorUserId = requestorUser.UserId,
                SwapItemOfferId = (swapItemOffer == null ? (long?)null : swapItemOffer.SwapItemId),
                MoneyOffer = moneyOffer,
                Accepted = false,
                Declined = false,
                Read = false
            };

            var swapRequestStatus = Add(swapRequest);

            if (swapRequestStatus ==SwapRequestStatus.Added)
            {
                // TODO: Notify through parse framework
                return swapRequest;
            }

            return null;
        }

        public SwapRequestStatus MarkAsRead(SwapRequest swapRequest)
        {
            try
            {
                Context.SwapRequests.Attach(swapRequest);
                swapRequest.Read = true;
                Context.Entry(swapRequest).Property("Read").IsModified = true;

                Context.SaveChanges();
                return SwapRequestStatus.MarkedAsRead;
            }
            catch (Exception exception)
            {
                Logger.Write(LogType.Exception, Logger.ExceptionMessage(exception));
                return SwapRequestStatus.Error;
            }
        }

        public SwapRequestStatus AcceptRequest(SwapRequest swapRequest)
        {
            try
            {
                // Mark swap request as accepted
                Context.SwapRequests.Attach(swapRequest);
                swapRequest.Accepted = true;
                Context.Entry(swapRequest).Property("Accepted").IsModified = true;

                // Mark swap item as swapped
                SwapItem requestedSwapItem = new SwapItem()
                {
                    SwapItemId = swapRequest.SwapItemId
                };

                Context.SwapItems.Attach(requestedSwapItem);
                requestedSwapItem.Swapped = true;
                Context.Entry(requestedSwapItem).Property("Swapped").IsModified = true;

                Context.SaveChanges();
                return SwapRequestStatus.Accepted;
            }
            catch (Exception exception)
            {
                Logger.Write(LogType.Exception, Logger.ExceptionMessage(exception));
                return SwapRequestStatus.Error;
            }
        }

        public SwapRequestStatus DeclineRequest(SwapRequest swapRequest)
        {
            try
            {
                Context.SwapRequests.Attach(swapRequest);
                swapRequest.Declined = true;
                Context.Entry(swapRequest).Property("Declined").IsModified = true;

                Context.SaveChanges();
                return SwapRequestStatus.Declined;
            }
            catch (Exception exception)
            {
                Logger.Write(LogType.Exception, Logger.ExceptionMessage(exception));
                return SwapRequestStatus.Error;
            }
        }

         
    }
}
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Net.Mime;
using Swappler.Database;
using Swappler.Models;
using Swappler.Models.Status;
using Swappler.Services.Interfaces;
using Swappler.Utilities;

namespace Swappler.Services
{
    ///
    /// <summary>Service class for Swap items.</summary>
    ///
    public class SwapItemService : Service<SwapItem, SwapplerSqliteContext>, ISwapItemService
    {
        private string imagesPath;

        private string ImagesFullPath
        {
            get { return AppDomain.CurrentDomain.BaseDirectory + imagesPath; }
            set { imagesPath = value; }
        }

        public SwapItemService(string imagesPath)
        {
            this.ImagesFullPath = imagesPath;
        }

        public SwapItemStatus Add(SwapItem swapItem)
        {
            try
            {
                Context.Entry(swapItem).State = EntityState.Added;
                Context.SaveChanges();
            }
            catch (Exception exception)
            {
                Logger.Write(LogType.Exception, Logger.ExceptionMessage(exception));

                return SwapItemStatus.Error;
            }

            return SwapItemStatus.Added;
        }

        public SwapItemStatus Remove(SwapItem swapItem)
        {
            try
            {
                Context.SwapItems.Attach(swapItem);
                Context.SwapItems.Remove(swapItem);
                Context.SaveChanges();
            }
            catch (Exception e)
            {
                Logger.Write(LogType.Exception, e.Message + "\n" + e.StackTrace);
                return SwapItemStatus.Error;
            }

            return SwapItemStatus.Removed;
        }

        public SwapItemStatus Update(SwapItem swapItem, params string[] updateFields)
        {
            try
            {
                if (updateFields == null || updateFields.Length == 0)
                {
                    Context.SwapItems.Attach(swapItem);
                    Context.Entry(swapItem).State = EntityState.Modified;
                }
                else
                {
                    Context.SwapItems.Attach(swapItem);
                    foreach (string updateField in updateFields)
                    {
                        Context.Entry(swapItem).Property(updateField).IsModified = true;
                    }
                }
                Context.SaveChanges();
            }
            catch (Exception e)
            {
                Logger.Write(LogType.Exception, e.Message + "\n" + e.StackTrace);
                return SwapItemStatus.Error;
            }

            return SwapItemStatus.Updated;
        }

        public List<SwapItem> FindWhere(Func<SwapItem, bool> wherePredicate)
        {
            try
            {
                var swapItems = Context.SwapItems.Where(wherePredicate);
                return swapItems.ToList();
            }
            catch (Exception e)
            {
                Logger.Write(LogType.Exception, e.Message + "\n" + e.StackTrace);
                return null;
            }
        }

        /// 
        /// <summary>Add new swap item to database.</summary>
        /// <param name="guid"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="photoUrl"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        ///  
        public SwapItemStatus Publish(string name, string description, Image photo, User user)
        {
            var guid = Guid.NewGuid();
            var dateTime = DateTime.Now;

            var photoGuid = Guid.NewGuid();
            var photoName = "si-" + photoGuid.ToString("D") + photo.RawFormat.ExtensionName();
            var photoFullFilename = ImagesFullPath + photoName;

            photo.Save(photoFullFilename);

            SwapItem swapItem = new SwapItem
            {
                Guid = guid,
                Name = name,
                Description = description,
                PhotoFilename = photoName,
                Date = dateTime,
                UserId = user.UserId                
            };

            var swapItemStatus = Add(swapItem);

            if (swapItemStatus == SwapItemStatus.Added)
            {
                swapItemStatus = SwapItemStatus.Published;
            }

            return swapItemStatus;
        }

        ///
        /// <summary>Remove swap item from database.</summary>
        /// <param name="swapItemGuid"></param>
        /// <returns></returns>
        /// 
        public bool Remove(Guid swapItemGuid)
        {
            SwapItem swapItem = new SwapItem
            {
                Guid = swapItemGuid
            };

            return Remove(swapItem) == SwapItemStatus.Removed;
        }

        public List<SwapItem> FindRange(int offset, int max)
        {
            try
            {
                var swapItems = Context.SwapItems
                    .OrderByDescending(sr => sr.Date)
                    .Skip(offset)
                    .Take(max)
                    .ToList();
                return swapItems;
            }
            catch (Exception exception)
            {
                Logger.Write(LogType.Exception, exception.Message);
                return null;
            }
        }

        ///
        /// <summary>
        /// Get swap items based on popularity measure.
        /// </summary>
        /// <returns></returns>
        ///  
        public List<SwapItem> FindMostPopularSwapItems()
        {
            throw new NotImplementedException(); //TODO: Implement popularity search.
        }

        ///
        /// <summary>
        /// Search swap items by name or part of name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<SwapItem> FindByName(string name)
        {
            try
            {
                var swapItems = from swapItem in Context.SwapItems
                                where swapItem.Name == name
                                select swapItem;

                return swapItems.ToList();
            }
            catch (Exception exception)
            {
                Logger.Write(LogType.Exception, exception.Message);
                return null;
            }
        }

        public List<SwapItem> LoadNewest(int takeCount)
        {
            var swapItems =
                (from swapItem in Context.SwapItems
                 select swapItem)
                .OrderByDescending(item => item.Date)
                .Take(takeCount);

            return swapItems.ToList();
        }

        public List<SwapItem> LoadNewest(DateTime afterDate)
        {
            var swapItems =
                (from swapItem in Context.SwapItems
                 where swapItem.Date > afterDate
                 select swapItem)
                .OrderByDescending(item => item.Date);
            return swapItems.ToList();
        }

        public List<SwapItem> LoadMore(DateTime beforeDateTime, int takeCount)
        {
            var swapItems =
                (from swapItem in Context.SwapItems
                 where swapItem.Date < beforeDateTime
                 select swapItem)
                .Take(takeCount)
                .OrderByDescending(item => item.Date);
            return swapItems.ToList();
        }

    }
}
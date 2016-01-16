using System;
using System.Collections.Generic;
using System.Drawing;
using Swappler.Models;
using Swappler.Models.Status;


namespace Swappler.Services.Interfaces
{
    public interface ISwapItemService : IService<SwapItem, SwapItemStatus>
    {
        bool Remove(Guid swapItemGuid);

        SwapItemStatus Publish(string name, string description, Image photo, User user);

        List<SwapItem> FindRange(int offset, int max);

        List<SwapItem> FindMostPopularSwapItems();

        List<SwapItem> FindByName(string name);

        SwapItem FindByGuid(Guid guid);

        List<SwapItem> FindByUser(User user);

        List<SwapItem> LoadNewest(int takeCount);

        List<SwapItem> LoadNewest(DateTime afterDate);

        List<SwapItem> LoadMore(DateTime beforeDateTime, int takeCount);
    }
}

using System;
using System.Collections.Generic;
using Swappler.Models;
using Swappler.Models.Status;

namespace Swappler.Services.Interfaces
{
    public interface ISwapRequestService : IService<SwapRequest, SwapRequestStatus>
    {
        SwapRequest FindByGuid(Guid guid);

        List<SwapRequest> FindUnreadByUser(User user);

        List<SwapRequest> FindRequestsByUser(string username);

        SwapRequest SendRequest(SwapItem requestedSwapItem, User requestorUser, SwapItem swapItemOffer, int? moneyOffer, DateTime dateCreated);

        SwapRequestStatus MarkAsRead(SwapRequest swapRequest);

        SwapRequestStatus AcceptRequest(SwapRequest swapRequest);

        SwapRequestStatus DeclineRequest(SwapRequest swapRequest);

        
    }
}
using System;
using System.Collections.Generic;
using Swappler.Models;
using Swappler.Models.Status;

namespace Swappler.Services.Interfaces
{
    public interface ISwapRequestService : IService<SwapRequest, SwapRequestStatus>
    {
        SwapRequest SendRequest(SwapItem requestedSwapItem, User requestorUser, SwapItem swapItemOffer, int? moneyOffer, DateTime dateCreated);

        void AcceptRequest(SwapRequest swapRequest);

        void DeclineRequest(SwapRequest swapRequest);

        List<SwapRequest> FindUnreadByUser(User user);
        
        List<SwapRequest> FindRequestsByUser(string username);
    }
}
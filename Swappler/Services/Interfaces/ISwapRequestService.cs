using Swappler.Models;
using Swappler.Models.Status;
using System;

namespace Swappler.Services.Interfaces
{
    public interface ISwapRequestService : IService<SwapRequest, SwapRequestStatus>
    {
         SwapRequest SendRequest(SwapItem requestedSwapItem, User requestorUser, SwapItem swapItemOffer, DateTime dateCreated, int? moneyOffer);
    }
}
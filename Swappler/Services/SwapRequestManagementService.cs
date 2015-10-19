using Swappler.Models;
using Swappler.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Swappler.Services
{
    /*
     * Swap request management service.
     * 
     */ 
    public class SwapRequestManagementService
    {

        private SwapRequestRepository swapRequestRepository;
        private ISwapRequestRepository swapRequestRepositoryInterface;

        public SwapRequestManagementService()
        {
            swapRequestRepository = new SwapRequestRepository();
            swapRequestRepositoryInterface = (ISwapRequestRepository) swapRequestRepository;
        }

        public SwapRequest sendRequest(String swapRequestGuid, SwapItem swapItem, SwapItem offerItem, DateTime dateCreated, int money)
        {
            //TODO: Send notification to user.
            SwapRequest newRequest = new SwapRequest(swapRequestGuid, swapItem, offerItem, dateCreated, money);
            swapRequestRepositoryInterface.addSwapRequest(newRequest);
            return newRequest;
        }

        public void acceptRequest(SwapRequest swapRequest)
        {
            //TODO: Send notification to user.
            //TODO: Update request flags in database.
        }

        public void declineRequest()
        {
            //TODO: Send notification to user.
            //TODO: Delete request from database.
        }

        public List<SwapRequest> getRequestsByUser(String username)
        {
            return swapRequestRepositoryInterface.query(new RequestSpecificationByUser(username));
        }

    }
}
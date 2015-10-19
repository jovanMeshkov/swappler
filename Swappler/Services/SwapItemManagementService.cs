using Swappler.Models;
using Swappler.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Swappler.Services
{
    /*
     * Swap item service.
     * 
     */
    public class SwapItemManagementService
    {
        /* Specifies number of items included in newest items feed. */
        private const int LATEST_SWAP_ITEMS_NUMBER = 15;

        private SwapItemRepository swapItemRepository;
        private ISwapItemRepository swapItemRepositoryInterface;

        /*
         * Constructor.
         * 
         */
        public SwapItemManagementService() 
        {
            swapItemRepository = new SwapItemRepository();
            swapItemRepositoryInterface = (ISwapItemRepository) swapItemRepository;
        }

        /*
         * Get swap items based on popularity measure.
         * 
         */
        public List<SwapItem> getMostPopularSwapItems() 
        {
            throw new NotImplementedException(); //TODO: Implement popularity search.
        }

        /*
         * Get last swap items.Number of items defined as LATEST_ITEMS_CONSTANT.
         * 
         */
        public List<SwapItem> getNewestSwapItems()
        {
            return swapItemRepositoryInterface.query(new SwapItemSpecificationByAge(LATEST_SWAP_ITEMS_NUMBER));
        }

        /*
         * Search swap items by name or part of name.
         *  
         */
        public List<SwapItem> getSwapItemByName(String name) 
        {
            return swapItemRepositoryInterface.query(new SwapItemSpecificationByName(name));
        }

        /*
         * Add new swap item to database.
         * 
         */
        public SwapItem addNewSwapItem(String name, String description, DateTime date, User user)  
        {
            SwapItem newSwapItem = new SwapItem(Guid.NewGuid().ToString(), name, description, date, user);
            swapItemRepository.addSwapItem(newSwapItem);
            return newSwapItem;
        }

        /*
         * Remove swap item from database.
         * 
         */
        public Boolean removeSwapItem(String swapItemGuid) 
        {
            return swapItemRepositoryInterface.removeSwapItem(swapItemGuid);
        }

    }
}
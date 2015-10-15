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
    public class SwapItemService
    {
        private const int LATEST_SWAP_ITEMS_NUMBER = 15;

        private SwapItemRepository swapItemRepository;
        private ISwapItemRepository swapItemRepositoryInterface;

        /*
         * Constructor.
         * 
         */
        public SwapItemService()
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
            throw new NotImplementedException();
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
        public Boolean addNewSwapItem(String name, String description, DateTime date, User user)  
        {
            return false;
        }

        /*
         * Remove swap item from database.
         * 
         */
        public Boolean removeSwapItem(Guid swapItemGuid) 
        {
            return false;    
        }

    }
}
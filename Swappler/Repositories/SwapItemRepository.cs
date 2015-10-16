using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Swappler.Models;
using Swappler.Database;

namespace Swappler.Repositories
{
    /*
     * Repository class for Swap items.
     * 
     */
    public class SwapItemRepository : ISwapItemRepository
    {
        private SwapItemsDAO swapItemsDAO = new SwapItemsDAO();

        public Boolean addSwapItem(SwapItem swapItem)
        {
            return swapItemsDAO.addSwapItem(swapItem);
        }

        public Boolean removeSwapItem(String swapItemGuid)
        {
            return swapItemsDAO.removeSwapItem(swapItemGuid);
        }

        public Boolean updateSwapItem(SwapItem swapItem)
        {
            throw new NotImplementedException(); //TODO: Implement edit swap item.
        }

        public List<SwapItem> query(ISwapItemSpecification specification)
        {
            return swapItemsDAO.query(specification.toSqlClause());
        }
   
    }
}
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
            throw new NotImplementedException();
        }

        public Boolean removeSwapItem(Guid swapItemGuid)
        {
            throw new NotImplementedException();
        }

        public Boolean updateSwapItem(SwapItem swapItem)
        {
            throw new NotImplementedException();
        }

        public List<SwapItem> query(ISwapItemSpecification specification)
        {
            return swapItemsDAO.query(specification.toSqlClause());
        }

        public List<SwapItem> getAll()
        {
            throw new NotImplementedException();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Swappler.Models;
using Swappler.Database;

namespace Swappler.Repositories
{
    public class SwapRequestRepository : ISwapRequestRepository
    {
        SwapRequestDAO swapRequestDAO = new SwapRequestDAO();

        public bool addSwapRequest(SwapRequest swapRequest)
        {
            throw new NotImplementedException();
        }

        public bool removeSwapItem(String swapRequestGuid)
        {
            throw new NotImplementedException();
        }

        public bool updateSwapItem(SwapRequest swapItem)
        {
            throw new NotImplementedException();
        }

        public List<SwapRequest> query(ISwapRequestSpecification specification)
        {
            return swapRequestDAO.query(specification.toSqlClause());
        }
    }
}
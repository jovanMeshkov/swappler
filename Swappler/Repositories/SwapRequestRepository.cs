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
            return swapRequestDAO.addSwapRequest(swapRequest);
        }

        public bool removeSwapRequest(String swapRequestGuid)
        {
            return swapRequestDAO.removeSwapRequest(swapRequestGuid);
        }

        public bool updateSwapRequest(SwapRequest swapRequest)
        {
            throw new NotImplementedException(); //TODO: Implement edit swap request.
        }

        public List<SwapRequest> query(ISwapRequestSpecification specification)
        {
            return swapRequestDAO.query(specification.toSqlClause());
        }
    }
}
using Swappler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Swappler.Repositories
{
    public interface ISwapRequestRepository
    {
        Boolean addSwapRequest(SwapRequest swapRequest);
        Boolean removeSwapItem(String swapRequestGuid);
        Boolean updateSwapItem(SwapRequest swapItem);
        List<SwapRequest> query(ISwapRequestSpecification specification);
    }
}
using Swappler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swappler.Repositories
{
    interface ISwapItemRepository
    {
        // TODO: Define SwapItemRepository interface.
        Boolean addSwapItem(SwapItem swapItem);
        Boolean removeSwapItem(Guid swapItemGuid);
        Boolean updateSwapItem(SwapItem swapItem);
        List<SwapItem> query(ISwapItemSpecification specification);
        List<SwapItem> getAll();
    }
}

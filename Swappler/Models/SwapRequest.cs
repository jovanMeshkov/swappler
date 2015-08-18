using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Swappler.Models
{
    public class SwapRequest
    {

        private SwapItem swapItem { get; set; }
        private SwapItem offerItem { get; set; }
        private DateTime date;
        private int money { get; set; }
        private Boolean flagActive;
    }
}
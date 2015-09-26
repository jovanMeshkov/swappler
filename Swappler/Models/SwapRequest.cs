using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Swappler.Models
{
    /*
     * Model for user-to-user swap request.
     *
     * */
    public class SwapRequest
    {

        private SwapItem SwapItem { get; set; }

        private SwapItem OfferItem { get; set; }
        
        private DateTime Date;
        
        private int Money { get; set; }
        
        private Boolean FlagActive;

        /*
         * Constructor.
         * 
         * */
        public SwapRequest(SwapItem swapItem, SwapItem offerItem, DateTime date, int money)
        {
            this.SwapItem = swapItem;
            this.OfferItem = offerItem;
            this.Date = date;
            this.Money = money;
            this.FlagActive = true;
        }
    }
}
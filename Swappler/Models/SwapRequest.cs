using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Swappler.Models
{
    /*
     * Model for user-to-user swap request.
     *
     * 
     */
    public class SwapRequest
    {

        private String swapRequestGuid;
        public String SwapRequestGuid
        {
            get
            {
                return swapRequestGuid;
            }
            set
            {
                swapRequestGuid = value;
            }
        }

        private DateTime date;
        public DateTime Date
        {
            get
            {
                return date;
            }
            set
            {
                date = value;
            }
        }

        private int money;
        public int Money
        {
            get
            {
                return money;
            }
            set
            {
                money = value;
            }
        }

        private Boolean flagActive;
        public Boolean FlagActive
        {
            get
            {
                return flagActive;
            }
            set
            {
                flagActive = value;
            }
        }

        private SwapItem swapItem;
        public SwapItem SwapItem
        {
            get
            {
                return swapItem;
            }
            set
            {
                swapItem = value;
            }
        }

        private SwapItem offerItem;
        public SwapItem OfferItem
        {
            get
            {
                return offerItem;
            }
            set
            {
                offerItem = value;
            }
        }
        
        

        /*
         * Constructor.
         *  
         */
        public SwapRequest(String swapRequestGuid, SwapItem swapItem, SwapItem offerItem, DateTime date, int money)
        {
            this.swapRequestGuid = swapRequestGuid;
            this.SwapItem = swapItem;
            this.OfferItem = offerItem;
            this.Date = date;
            this.Money = money;
            this.FlagActive = true;
        }
    }
}
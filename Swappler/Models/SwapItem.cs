using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Swappler.Models
{
    /*
     * Model for swap item.
     * 
     * */
    public class SwapItem
    {
        private String Name { get; set; }

        private String Description { get; set; }

        private DateTime Date { get; set; }
                
        private User UserId { get; set; }
        
        private String PhotoUrl { get; set; }

        private Boolean Flag_swapped { get; set; }

        /*
         * Constructor
         * 
         * */
        public SwapItem(String name, String description, DateTime date, User userId)
        {
            this.Name = name;
            this.Description = description;
            this.Date = date;
            this.UserId = userId;
            this.Flag_swapped = false;
        }
    }
}
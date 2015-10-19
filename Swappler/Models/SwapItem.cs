using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Swappler.Models
{
    /*
     * Model for swap item.
     * 
     */
    public class SwapItem
    {
        public String SwapItemGuid { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public DateTime Date { get; set; }
        public User UserId {get; set;}
        public String PhotoUrl { get; set; }
        public bool Swapped { get; set; }
        public int Popularity { get; set; }

        /*
         * Empty constructor. 
         */
        public SwapItem() {}

        /*
         * Constructor
         * 
         * 
         */
        public SwapItem(String swapItemGuid, String name, String description, DateTime date, User userId)
        {
            this.SwapItemGuid = swapItemGuid;
            this.Name = name;
            this.Description = description;
            this.Date = date;
            this.UserId = userId;
            this.Swapped = false;
            this.Popularity = 0;
        }
    }
}
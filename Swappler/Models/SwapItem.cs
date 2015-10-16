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

        private String swapItemGuid;
        public String SwapItemGuid
        {
            get
            {
                return swapItemGuid;
            }
            set
            {
                swapItemGuid = value;
            }
        }

        private String name;
        public String Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        private String description;
        public String Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
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

        private User userId;
        public User UserId
        {
            get
            {
                return userId;
            }
            set
            {
                userId = value;
            }
        }

        private String photoUrl;
        public String PhotoUrl
        {
            get
            {
                return photoUrl;
            }
            set
            {
                photoUrl = value;
            }
        }

        private Boolean flag_swapped;
        public Boolean Flag_swapped
        {
            get
            {
                return flag_swapped;
            }
            set
            {
                flag_swapped = value;
            }
        }

        private int popularity;
        public int Popularity
        {
            get
            {
                return popularity;
            }
            set
            {
                popularity = value;
            }
        }

        /*
         * Constructor
         * 
         * 
         */
        public SwapItem(String swapItemGuid, String name, String description, DateTime date, User userId)
        {
            this.swapItemGuid = swapItemGuid;
            this.Name = name;
            this.Description = description;
            this.Date = date;
            this.UserId = userId;
            this.Flag_swapped = false;
            this.Popularity = 0;
        }
    }
}
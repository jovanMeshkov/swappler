using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Swappler.Models
{
    public class SwapItem
    {
        private String Name { get; set; }
        private String Description { get; set; }
        private DateTime Date { get; set; }
        private Boolean Flag_swapped { get; set; }
        private User UserId { get; set; }
        private Address Address { get; set; }
        private String PhotoUrl { get; set; }

        public SwapItem(String name, DateTime date, User userId)
        {
        }
    }
}
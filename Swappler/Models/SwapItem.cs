using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Swappler.Models
{
    public class SwapItem
    {
        private String name { get; set; }
        private String description { get; set; }
        private DateTime date { get; set; }
        private Boolean flag_swapped { get; set; }
        private User userId { get; set; }
        private Address address { get; set; }
        private String photoURL { get; set; }
    }
}
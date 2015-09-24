using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Swappler.Models
{
    /*
     * Model for address that encapsulates location data.
     * 
     * */
    public class Address
    {
        private String Country { get; set; }

        private String City { get; set; }
        
        private String Street { get; set; }

        /*
         * Constructor.
         * 
         * */
        public Address(String country, String city, String street)
        {
            this.Country = country;
            this.City = city;
            this.Street = street;
        }
    }
}
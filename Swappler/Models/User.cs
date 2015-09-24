using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Swappler.Models
{
    /*
     *  Model for user.
     *  
     *  */
    public class User
    {

        private String Name { get; set; }

        private String LastName { get; set; }
        
        private String Email { get; set; }
        
        private String Password { get; set; }
        
        private String Username { get; set; }
        
        private String Phone { get; set; }
        
        private String PhotoURL { get; set; }
        
        private Address Address { get; set; }

        /*
         * Constructor.
         * 
         * */
        public User(String name, String lastName, String email, String password, String username, String phone, Address address)
        {
            this.Name = name;
            this.LastName = lastName;
            this.Email = email;
            this.Password = password;
            this.Username = username;
            this.Phone = phone;
            this.Address = address;
        }
    }
}
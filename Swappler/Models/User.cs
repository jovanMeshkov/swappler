using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Swappler.Models
{
    /*
     *  Model for user.
     *    
     */
    public class User
    {
        private String name;
        public String Name
        {
            get
            {
                return this.name;
            }
            set
            {

            }
        }

        private String lastName;
        public String LastName
        {
            get
            {
                return lastName;
            }
            set
            {

            }
        }

        private String email;
        public String Email
        {
            get
            {
                return email;
            }
            set
            {

            }
        }

        private String password;
        public String Password
        {
            get
            {
                return password;
            }
            set
            {

            }
        }

        private String username;
        public String Username
        {
            get
            {
                return username;
            }
            set
            {

            }
        }

        private String phone;
        public String Phone
        {
            get
            {
                return phone;
            }
            set
            {

            }
        }

        private String photoURL;
        public String PhotoURL
        {
            get
            {
                return photoURL;
            }
            set
            {

            }
        }

        private String address;
        public String Address
        {
            get
            {
                return address;
            }
            set
            {

            }

        }

        /*
         * Constructor.
         * 
         * */
        public User(String name, String lastName, String email, String password, String username, String phone, String address)
        {
            this.name = name;
            this.lastName = lastName;
            this.email = email;
            this.password = password;
            this.username = username;
            this.phone = phone;
            this.address = address;
        }

    }
}
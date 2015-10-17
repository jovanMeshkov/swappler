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
        //TODO: Modify user add/remove with guid. Guid not implemented(DAO,Repo,Service..) !
        private Guid userGuid;
        public Guid UserGuid
        {
            get
            {
                return userGuid;
            }
            set
            {
                userGuid = value;
            }
        }

        private String name;
        public String Name
        {
            get
            {
                return this.name;
            }
            set
            {
                name = value;
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
                lastName = value;
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
                email = value;
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
                password = value;
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
                username = value;
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
                phone = value;
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
                photoURL = value;
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
                address = value;
            }

        }

        /*
         * Empty constructor
         * 
         */
        public User()
        {
        }

        /*
         * Constructor.
         * 
         */
        public User(String name, String lastName, String email, String password, String username, String phone, String address)
        {
            this.userGuid = Guid.NewGuid();
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
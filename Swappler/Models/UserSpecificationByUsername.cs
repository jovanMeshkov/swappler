using Swappler.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Swappler.Models
{
    /*
     * Model of Specification criteria by username.
     *  
     */
    public class UserSpecificationByUsername : IUserSpecification
    {
        private String username { get; set; }

        /*
         * Empty constructor.
         * 
         */
        public UserSpecificationByUsername() { }

        /*
         * Constructor.
         * 
         */
        public UserSpecificationByUsername(String username)
        {
            this.username = username;
        }


        public String toSqlClause()
        {
            if (username != null)
            {
                return "select * from User u where u.Username='" + username + "'";
            }
            else
            {
                return "";
            }
        }
    }
}
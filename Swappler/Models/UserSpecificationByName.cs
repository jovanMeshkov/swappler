using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Swappler.Repositories
{
    /*
     * Model of Specification criteria by name or last name of user.
     * 
     */
    public class UserSpecificationByName : IUserSpecification 
    {
        private String namePart { get; set; }

        /**
         * Empty constructor(No querying specification).
         * 
         */
        public UserSpecificationByName() { }

        /**
         * Constructor.
         * 
         */
        public UserSpecificationByName(String namePart)
        {
            this.namePart = namePart;
        }

        String IUserSpecification.toSqlClause()
        {
            String sqlClause;
            if (namePart != null)
            {
                sqlClause = "select * from User u where u.Name = " + namePart + " or u.LastName = " + namePart;
            }
            else
            {
                sqlClause = "select * from User";
            }
            return sqlClause;
        }
    }
}
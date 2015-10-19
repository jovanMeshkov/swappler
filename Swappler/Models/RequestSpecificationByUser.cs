using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Swappler.Models
{
    public class RequestSpecificationByUser : ISwapRequestSpecification
    {
        private String username;

        public RequestSpecificationByUser(String username)
        {
            this.username = username;
        }

        public string toSqlClause()
        {
            return "select * " +
            " from SwapRequest r inner join SwapItem i on r.SwapItemId = i.Guid or r.OfferItemId = i.Guid where i.UserId = '" + username + "'";
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Swappler.Repositories
{
    public interface IUserSpecification
    {
         String toSqlClause();
    }
}
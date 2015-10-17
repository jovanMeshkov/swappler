using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Swappler.Models
{
    public interface ISwapItemSpecification
    {
        String toSqlClause();
    }
}
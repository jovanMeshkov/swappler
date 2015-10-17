using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Swappler.Models
{
    /*
     * Specification criteria by name part for Swap item.
     * 
     */
    public class SwapItemSpecificationByName : ISwapItemSpecification
    {
        private String name;

        /*
         * Constructor.
         * 
         */
        public SwapItemSpecificationByName(String name)
        {
            this.name = name;
        }
        
        public String toSqlClause()
        {
            return "select * from SwapItem s where s.Name like '%" + this.name + "%'";
        }
    }
}
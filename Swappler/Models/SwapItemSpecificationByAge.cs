using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Swappler.Models
{
    public class SwapItemSpecificationByAge : ISwapItemSpecification
    {
        private int numberOfSwapItems;
        private DateTime from;
        private DateTime to;

        public SwapItemSpecificationByAge(int numberOfSwapItems)
        {
            this.numberOfSwapItems = numberOfSwapItems;
        }

        public SwapItemSpecificationByAge(DateTime from, DateTime to)
        {
            this.numberOfSwapItems = -1;
            this.from = from;
            this.to = to;
        }

        public SwapItemSpecificationByAge(int numberOfSwapItems, DateTime from, DateTime to)
        {
            this.numberOfSwapItems = numberOfSwapItems;
            this.from = from;
            this.to = to;
        }

        public String toSqlClause()
        {
            if (numberOfSwapItems != -1)
            {
                return "select * from SwapItem s order by s.Date desc limit " + numberOfSwapItems;
                //return "select top(" + numberOfSwapItems + ") * from SwapItem s order by s.Date desc";
            }
            else
            {
                return "select * from SwapItem s where s.Date between " + this.from + " and " + this.to;
            }
        }
    }
}
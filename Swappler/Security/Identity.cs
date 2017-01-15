using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace Swappler.Security
{
    public class Identity : IIdentity
    {
        public bool IsAuthenticated { get; private set; }
        public string Name { get; private set; }

        public Identity(string name, bool isAuthenticated)
        {
            IsAuthenticated = isAuthenticated;
            Name = name;
        }

        public string AuthenticationType
        {
            get { return "Custom"; }
        }
        
        
    }
}
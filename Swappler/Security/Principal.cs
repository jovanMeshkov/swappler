using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace Swappler.Security
{
    public class Principal : IPrincipal
    {
        public IIdentity Identity { get; private set; }

        public long UserId { get; set; }

        public Principal(string username, bool isAuthenticated)
        {
            Identity = new Identity(username, isAuthenticated);
        }

        public bool IsInRole(string role)
        {
            return false;
        }
    }
}
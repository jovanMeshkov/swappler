using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Swappler.Attributes;

namespace Swappler.Controllers
{
    public class AuthController : Controller
    {
        [Authenticate]
        public ActionResult Login()
        {
           
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }
    }
}
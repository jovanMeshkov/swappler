using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Swappler.Attributes;
using System.Diagnostics;

namespace Swappler.Controllers
{
    public class AuthController : Controller
    {
        [Authenticate]
        public ActionResult Login(String username, String password)
        {

            return View();
        }

        public ActionResult Register()
        {
            return View();
        }
    }
}
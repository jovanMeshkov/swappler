using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Swappler.Database;

namespace Swappler.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            SwapplerDAO.getAllUsers();
            return View();
        }
        public ActionResult EditProfile()
        {
            return View();
        }

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
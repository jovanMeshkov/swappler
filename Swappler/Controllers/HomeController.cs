using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Swappler.Models;
using Swappler.Services;
using System.Diagnostics;
using Swappler.Repositories;
using Swappler.Database;

namespace Swappler.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(listAllItems);
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
        public ActionResult AddItem()
        {
            return View();
        }

    }
}
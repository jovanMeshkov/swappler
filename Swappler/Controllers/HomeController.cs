using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Swappler.Models;
using Swappler.Services;
using System.Diagnostics;
using Swappler.Repositories;

namespace Swappler.Controllers
{
    public class HomeController : Controller
    {
        ManageUsersService manageUsersService;

        public ActionResult Index()
        {
            //TODO: Test method.Delete it.
            manageUsersService = new ManageUsersService();
            UsersDAO dao = new UsersDAO();
            Debug.WriteLine("Broj na korisnici registrirani:  " + dao.queryUsers("select * from User").Capacity);
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
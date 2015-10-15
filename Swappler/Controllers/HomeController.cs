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

            // Search by username
            User searchedUser = manageUsersService.getUserByUsername("schenock");

            // Search by name or last name
            List<User> searchedUsers = manageUsersService.getUserByName("Dan");

            // Add new user
            manageUsersService.addNewUser("Dane", "Mitrev", "dane@swappler.com", "76476", "Schenock5", "02331", "51Ave");

            // Delete user with username
            //manageUsersService.removeUser("daneto");

            // Get all users
            List<User> allUsers = manageUsersService.getAllUsers();
            foreach (User user in allUsers)
            {
                Debug.WriteLine("Username: " + user.Username + "| Name: " + user.Name + ", Last name: " + user.LastName); 
            }

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
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
        ManageUsersService manageUsersService;
        SwapItemService swapItemService;


        public ActionResult Index()
        {
            //TODO: Test method.Delete it.
            manageUsersService = new ManageUsersService();
            swapItemService = new SwapItemService();

            // Search by username
            User searchedUser = manageUsersService.getUserByUsername("Schenock5");

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

            List<SwapItem> swapItems = swapItemService.getSwapItemByName("Motorka");

            // Add swap item
             SwapItemsDAO swDAO = new SwapItemsDAO();
            // swDAO.addSwapItem(new SwapItem(Guid.NewGuid().ToString(), "Muljac", "Zacuvan muljac za groizje.", new DateTime(), searchedUser));

            // Remove swap item.
             swDAO.removeSwapItem("b39db431-1d1a-47c2-a92f-042cfc7fc8a2");

            // Get swap items
            List<SwapItem> listAllItems = swDAO.query("select * from SwapItem");
            Debug.WriteLine("size: " + listAllItems.Count);
            foreach (SwapItem swapItem in listAllItems)
            {
                Debug.WriteLine("Item : " + swapItem.SwapItemGuid + " |  Name: " + swapItem.Name + " Owner: " + swapItem.UserId.Name);
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
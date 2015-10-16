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
            SwapItemsDAO swDAO = new SwapItemsDAO();

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

            // Remove swap item.
            swapItemService.removeSwapItem("8e11970d-05fd-430e-afd9-af5cf2c766be");

            // Search swap items.
            List<SwapItem> searchResults = swapItemService.getSwapItemByName("Mulj");
            Debug.WriteLine("Found: " + searchResults.ElementAt(0).Name);

            // Generate newest items feed.
            List<SwapItem> feedList = swapItemService.getNewestSwapItems();
            Debug.WriteLine("Latest swap items: ");
            foreach (SwapItem itemInFeed in feedList)
            {
                Debug.WriteLine("Item : Name: " + itemInFeed.Name + " Owner: " + itemInFeed.UserId.Name);
            }

            // Get all swap items
            List<SwapItem> listAllItems = swDAO.query("select * from SwapItem");
            Debug.WriteLine("Swap items total: " + listAllItems.Count);
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
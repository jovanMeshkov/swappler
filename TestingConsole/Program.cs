using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Swappler;
using Swappler.Models;
using Swappler.Services;
using Swappler.Repositories;
using Swappler.Database;


namespace TestingConsole
{
    class Program
    {
        

        private static void Testing() {
            ManageUsersService manageUsersService;
        SwapItemService swapItemService;
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

            // Add swap request.
            SwapItem testItem = listAllItems.ElementAt(4);
            SwapRequestDAO requestDAO = new SwapRequestDAO();
            requestDAO.addSwapRequest(new SwapRequest(Guid.NewGuid().ToString(), testItem, testItem, new DateTime(), 155));

           


            // TEST ALL
            User user1 = manageUsersService.addNewUser("Ant", "Roryy", "em@ail.com", "passw", "R5", "075666773", "address");
            User user2 = manageUsersService.addNewUser("Pep", "Haua", "ja@ail.com", "passw", "Hauuu", "872010111", "52 St.");
           
            SwapItem item1 = swapItemService.addNewSwapItem("Motorka", "aparat", new DateTime(), user1);
            SwapItem item2 = swapItemService.addNewSwapItem("Muljac", "grozje", new DateTime(), user2);
            SwapRequest HoolJohnSwap = new SwapRequest(Guid.NewGuid().ToString(), item1, item2, new DateTime(), 257);
            requestDAO.addSwapRequest(HoolJohnSwap);

            // Get all swap requests
            Debug.WriteLine("GETTING REQUESTS...");
            List<SwapRequest> allRequests = requestDAO.query("select * from SwapRequest");
            foreach (SwapRequest req in allRequests)
            {
                if (req.SwapItem.UserId != null && req.OfferItem.UserId != null)
                {
                    Debug.WriteLine("REQUEST >>> Money=" + req.Money + " eur   ITEM SWAPPED: " + req.SwapItem.Name + "(" + req.SwapItem.UserId.Name + ")   < - >  ITEM OFFERED:  " + req.OfferItem.Name + "(" + req.OfferItem.UserId.Name + ")");
                }
            }
        }
        static void Main(string[] args)
        {
        }
    }
}

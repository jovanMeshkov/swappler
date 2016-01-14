using System;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using Swappler.Database;
using Swappler.Models;
using Swappler.Services;

namespace TestingConsole
{
    class Program
    {
        private static void Testing()
        {
            //UserManagementService manageUsersService;
            //SwapItemManagementService swapItemService;
            ////TODO: Test method.Delete it.
            //manageUsersService = new UserManagementService();
            //swapItemService = new SwapItemManagementService();
            //SwapItemsDAO swDAO = new SwapItemsDAO();

            //// Search by username
            //User searchedUser = manageUsersService.getUserByUsername("Schenock5");

            //// Search by name or last name
            //List<User> searchedUsers = manageUsersService.FindUserByNameOrSurname("Dan");

            //// Add new user
            //manageUsersService.addNewUser("Dane", "Mitrev", "dane@swappler.com", "76476", "Schenock5", "02331", "51Ave");

            //// Delete user with username
            ////manageUsersService.removeUser("daneto");

            //// Get all users
            //List<User> allUsers = manageUsersService.AllUsers();
            //foreach (User user in allUsers)
            //{
            //    Debug.WriteLine("Username: " + user.Username + "| Name: " + user.Name + ", Last name: " + user.LastName);
            //}

            //List<SwapItem> swapItems = null; //swapItemService.getSwapItemByName("Motorka");

            //// Add swap item

            //// Remove swap item.
            ////swapItemService.removeSwapItem("8e11970d-05fd-430e-afd9-af5cf2c766be");

            //// Search swap items.
            //List<SwapItem> searchResults = swapItemService.getSwapItemByName("Mulj");
            //Debug.WriteLine("Found: " + searchResults.ElementAt(0).Name);

            //// Generate newest items feed.
            //List<SwapItem> feedList = swapItemService.getNewestSwapItems();
            //Debug.WriteLine("Latest swap items: ");
            //foreach (SwapItem itemInFeed in feedList)
            //{
            //    Debug.WriteLine("Item : Name: " + itemInFeed.Name + " Owner: " + itemInFeed.UserId.Name);
            //}

            //// Get all swap items
            //List<SwapItem> listAllItems = swDAO.query("select * from SwapItem");
            //Debug.WriteLine("Swap items total: " + listAllItems.Count);
            //foreach (SwapItem swapItem in listAllItems)
            //{
            //    Debug.WriteLine("Item : " + swapItem.Guid + " |  Name: " + swapItem.Name + " Owner: " + swapItem.UserId.Name);
            //}

            //// Add swap request.
            //SwapItem testItem = listAllItems.ElementAt(4);
            //SwapRequestDAO requestDAO = new SwapRequestDAO();
            //requestDAO.addSwapRequest(new SwapRequest(Guid.NewGuid().ToString(), testItem, testItem, new DateTime(), 155));




            //// TEST ALL
            //User user1 = manageUsersService.addNewUser("Ant", "Roryy", "em@ail.com", "passw", "R5", "075666773", "address");
            //User user2 = manageUsersService.addNewUser("Pep", "Haua", "ja@ail.com", "passw", "Hauuu", "872010111", "52 St.");

            //SwapItem item1 = swapItemService.addNewSwapItem("Motorka", "aparat", new DateTime(), user1);
            //SwapItem item2 = swapItemService.addNewSwapItem("Muljac", "grozje", new DateTime(), user2);
            //SwapRequest HoolJohnSwap = new SwapRequest(Guid.NewGuid().ToString(), item1, item2, new DateTime(), 257);
            //requestDAO.addSwapRequest(HoolJohnSwap);

            //// Get all swap requests
            //Debug.WriteLine("GETTING REQUESTS...");
            //List<SwapRequest> allRequests = requestDAO.query("select * from SwapRequest");
            //foreach (SwapRequest req in allRequests)
            //{
            //    if (req.SwapItemId.UserId != null && req.SwapItemOfferId.UserId != null)
            //    {
            //        Debug.WriteLine("REQUEST >>> Money=" + req.MoneyOffer + " eur   ITEM SWAPPED: " + req.SwapItemId.Name + "(" + req.SwapItemId.UserId.Name + ")   < - >  ITEM OFFERED:  " + req.SwapItemOfferId.Name + "(" + req.SwapItemOfferId.UserId.Name + ")");
            //    }
            //}
        }


        static void t(int? money)
        {
            Console.WriteLine(money);
        }
        static void Main(string[] args)
        {
            var s = new SwapRequestService();

            int? money = 2;

            t(money);

            return;
        }


        public const int SaltByteSize = 24;
        public const int HashByteSize = 20; // to match the size of the PBKDF2-HMAC-SHA-1 hash 
        public const int Pbkdf2Iterations = 1000;
        public const int IterationIndex = 0;
        public const int SaltIndex = 1;
        public const int Pbkdf2Index = 2;

        public static string HashPassword(string password)
        {
            var cryptoProvider = new RNGCryptoServiceProvider();
            byte[] salt = new byte[SaltByteSize];
            cryptoProvider.GetBytes(salt);

            var hash = GetPbkdf2Bytes(password, salt, Pbkdf2Iterations, HashByteSize);
            return Pbkdf2Iterations + ":" +
                   Convert.ToBase64String(salt) + ":" +
                   Convert.ToBase64String(hash);
        }

        public static bool ValidatePassword(string password, string correctHash)
        {
            char[] delimiter = {':'};
            var split = correctHash.Split(delimiter);
            var iterations = Int32.Parse(split[IterationIndex]);
            var salt = Convert.FromBase64String(split[SaltIndex]);
            var hash = Convert.FromBase64String(split[Pbkdf2Index]);

            var testHash = GetPbkdf2Bytes(password, salt, iterations, hash.Length);
            return SlowEquals(hash, testHash);
        }

        private static bool SlowEquals(byte[] a, byte[] b)
        {
            var diff = (uint) a.Length ^ (uint) b.Length;
            for (int i = 0; i < a.Length && i < b.Length; i++)
            {
                diff |= (uint) (a[i] ^ b[i]);
            }
            return diff == 0;
        }

        private static byte[] GetPbkdf2Bytes(string password, byte[] salt, int iterations, int outputBytes)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt);
            pbkdf2.IterationCount = iterations;
            return pbkdf2.GetBytes(outputBytes);
        }
    }

}

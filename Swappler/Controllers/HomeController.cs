using System;
using System.Collections.Generic;
using System.Drawing;
using System.Web.Mvc;
using Swappler.Models;
using Swappler.Services;
using Swappler.Attributes;
using Swappler.Models.Status;
using Swappler.Services.Interfaces;
using Swappler.Utilities;
using Swappler.ViewModels;

namespace Swappler.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService userService = new UserService(Models.User.ImagesPath);
        private readonly ISwapItemService swapItemService = new SwapItemService(Models.SwapItem.ImagesPath);
        private readonly ISwapRequestService swapRequestService = new SwapRequestService();

        [Authenticate]
        [HttpGet]
        public ActionResult Index()
        {
            IndexViewModel indexViewModel = new IndexViewModel();

            var swapItems = swapItemService.LoadNewest(2);

            indexViewModel.SwapItems = swapItems;

            if (swapItems == null || swapItems.Count ==0)
            {
                indexViewModel.SwapItems = new List<SwapItem>(0);
                return View(indexViewModel);
            }

            if (swapItems.Count == 1)
            {
                SessionHelper.FirstSwapItemDate = swapItems[0].Date;
                SessionHelper.LastSwapItemDate = swapItems[0].Date;
            }
            else
            {
                SessionHelper.FirstSwapItemDate = swapItems[0].Date;
                SessionHelper.LastSwapItemDate = swapItems[swapItems.Count - 1].Date;
            }
            
            return View(indexViewModel);
        }

        [HttpPost]
        public ActionResult LoadMoreSwapItems()
        {
            var beforeDate = SessionHelper.LastSwapItemDate ?? DateTime.Now;

            var swapItems = swapItemService.LoadMore(beforeDate, 2);

            if (swapItems == null || swapItems.Count == 0)
            {
                swapItems = new List<SwapItem>(0);
                return PartialView("~/Views/Partials/SwapItemsForFeed.cshtml", swapItems);
            }

            SessionHelper.LastSwapItemDate = swapItems[swapItems.Count - 1].Date;

            return PartialView("~/Views/Partials/SwapItemsForFeed.cshtml", swapItems);
        }
        
        [Authenticate]
        [HttpGet]
        public ActionResult EditProfile()
        {
            User user = userService.FindUserById(SessionHelper.SignedUser.UserId);

            return View(user);
        }
        
        [Authenticate]
        [HttpPost]
        public JsonResult SaveProfile(SaveProfileViewModel saveProfileViewModel)
        {
            return Json(new
            {
                Success = true,
                SuccessMessage = "Changes applied"
            });
        }

        [HttpGet]
        public ActionResult PublishSwapItem()
        {
            return View();
        }

        [HttpPost]
        public JsonResult PublishSwapItem(PublishSwapItemViewModel publishSwapItemViewModel)
        {
            // TODO: Implement validation for PublishSwapItemViewModel
            var signedUser = SessionHelper.SignedUser;

            var name = publishSwapItemViewModel.Name;
            var description = publishSwapItemViewModel.Description;
            var photo = publishSwapItemViewModel.Photo;
            var user = signedUser;

            var swapItemStatus = swapItemService.Publish(name, description, Image.FromStream(photo.InputStream), user);

            if (swapItemStatus == SwapItemStatus.Published)
            {
                return Json(new
                {
                    Success = true,
                    SuccessMessage = "Successfully published!"
                });
            }

            // Assuming error happend
            return Json(new
            {
                Error = true,
                ErrorMessage = "Error happend.. Try again!"
            });
        }

        // Na klik na kopceto SWAP se povikuva via handler, treba da vrate View: CreateSwapRequest.html so izbraniot item
        [HttpGet]
        public ActionResult CreateSwapRequest(Guid requestedSwapItemGuid)
        {
            Console.WriteLine(" asda  Ajax call came..! ");
            List<SwapItem> resultItems = swapItemService.FindWhere(x => x.Guid == requestedSwapItemGuid);
            return View(resultItems[0]);
        }

        // TODO: Pishi na angliski... mi gi bode ochte nogu!
        // ne e voopsto testiran
        // vo CreateSwapRequest ima kopce SEND, na klik se povikuva via handler i treba da prate 2 itemi i ako ima doplata: money
        [HttpPost]
        public ActionResult SendSwapRequest(Guid requestedSwapItemGuid, Guid offeredSwapItemGuid, int moneyOffered)
        {
            List<SwapItem> resultItems = swapItemService.FindWhere(x => x.Guid == requestedSwapItemGuid);
            List<SwapItem> resultItems2 = swapItemService.FindWhere(x => x.Guid == offeredSwapItemGuid);

            if (resultItems.Capacity == 1 && resultItems2.Capacity == 1) {
                
                SwapItem requestedSwapItem = resultItems[0];
                SwapItem offeredSwapItem = resultItems2[0];
                User signedUser = SessionHelper.SignedUser;

                SwapRequest swapRequest = swapRequestService.SendRequest(requestedSwapItem, signedUser, offeredSwapItem, new DateTime(), moneyOffered);
                return Index();
            } else {
                // TODO: Return json ?
                return Index();
            }
        }

        // TODO: Pishi na angliski i tua... mi gi bode ochte nogu!
        // prebaruvanje po del od ime, dodadena e skripta swappler.search.js
        [HttpGet] 
        public ActionResult SearchSwapItems(string partOfSwapItemName)
        {
            List<SwapItem> searchResults = swapItemService.FindWhere(x => x.Name.Contains(partOfSwapItemName));
            return PartialView("~/Views/Partials/SwapItemsForFeed.cshtml", searchResults);
        }

        [Authenticate]
        [HttpGet]
        public ActionResult SwapItem(Guid guid)
        {
            var swapItem = swapItemService.FindByGuid(guid);
            return View(swapItem);
        }
    }
}
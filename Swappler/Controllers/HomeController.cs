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
            var swapRequests = swapRequestService.FindUnreadByUser(SessionHelper.SignedUser);

            indexViewModel.SwapItems = swapItems;
            indexViewModel.SwapRequests = swapRequests;

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

        // TODO: Pishi na angliski i tua... mi gi bode ochte nogu!
        // prebaruvanje po del od ime, dodadena e skripta swappler.search.js
        [HttpGet]
        public ActionResult SearchSwapItems(string partOfSwapItemName)
        {
            List<SwapItem> searchResults = swapItemService.FindWhere(x => x.Name.Contains(partOfSwapItemName));
            return PartialView("~/Views/Partials/SwapItemsForFeed.cshtml", searchResults);
        }
    }
}
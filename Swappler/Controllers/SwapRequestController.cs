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
    public class SwapRequestController : Controller
    {
        private readonly IUserService userService = new UserService(Models.User.ImagesPath);
        private readonly ISwapItemService swapItemService = new SwapItemService(SwapItem.ImagesPath);
        private readonly ISwapRequestService swapRequestService = new SwapRequestService();

        // TODO: To be get or not to be post. Do not touch!
        [HttpPost]
        public ActionResult SendSwapRequest(Guid requestedSwapItemGuid, Guid offeredSwapItemGuid, int moneyOffered)
        {
            List<SwapItem> resultItems = swapItemService.FindWhere(x => x.Guid == requestedSwapItemGuid);
            List<SwapItem> resultItems2 = swapItemService.FindWhere(x => x.Guid == offeredSwapItemGuid);

            if (resultItems.Capacity == 1 && resultItems2.Capacity == 1)
            {

                SwapItem requestedSwapItem = resultItems[0];
                SwapItem offeredSwapItem = resultItems2[0];
                User signedUser = SessionHelper.SignedUser;

                SwapRequest swapRequest = swapRequestService.SendRequest(requestedSwapItem, signedUser, offeredSwapItem, new DateTime(), moneyOffered);
                return Redirect("/Home/Index");
            }
            else
            {
                // TODO: Return json ?
                return Redirect("/Home/Index");
            }
        }
        
        // Na klik na kopceto SWAP se povikuva via handler, treba da vrate View: CreateSwapRequest.html so izbraniot item
        [HttpGet]
        public ActionResult Create(Guid requestedSwapItemGuid)
        {
            SwapItem requestedSwapItem = swapItemService.FindByGuid(requestedSwapItemGuid);
            
            List<SwapItem> userSwapItems = swapItemService.FindByUser(SessionHelper.SignedUser);

            var createSwapRequestViewModel = new CreateSwapRequestViewModel();
            createSwapRequestViewModel.RequestedSwapItem = requestedSwapItem;
            createSwapRequestViewModel.UserSwapItems = userSwapItems;

            return View(createSwapRequestViewModel);
        }
    }
}
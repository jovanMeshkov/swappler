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
    [Authenticate]
    public class SwapRequestController : DefaultController
    {
        private readonly IUserService userService = new UserService(Models.User.ImagesPath);
        private readonly ISwapItemService swapItemService = new SwapItemService(SwapItem.ImagesPath);
        private readonly ISwapRequestService swapRequestService = new SwapRequestService();

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

        //vraka json od objekt SwapItem no vraka sekogas NULL, da se proveri zosto
        public string GetSwapItemByGuid()
        {
            if (!String.IsNullOrEmpty(Request.QueryString["guid"]))
            {
                string guids = Request.QueryString["guid"].ToString();
                System.Guid guid = System.Guid.Parse(guids);
                var swapRequest = swapItemService.FindByGuid(guid);
                return Json(swapRequest).ToString();
                
            }
            return Json("").ToString();
            
        }

        // TODO: To be get or not to be post. Do not touch!
        [HttpPost]
        public ActionResult SendSwapRequest(Guid requestedSwapItemGuid, Guid offeredSwapItemGuid, int moneyOffered)
        {
            Console.WriteLine("dsadasd");
            List<SwapItem> resultItems = swapItemService.FindWhere(x => x.Guid == requestedSwapItemGuid);
            List<SwapItem> resultItems2 = swapItemService.FindWhere(x => x.Guid == offeredSwapItemGuid);

            if (resultItems.Capacity == 1 && resultItems2.Capacity == 1)
            {
                SwapItem requestedSwapItem = resultItems[0];
                SwapItem offeredSwapItem = resultItems2[0];
                User signedUser = SessionHelper.SignedUser;

                SwapRequest swapRequest = swapRequestService.SendRequest(requestedSwapItem, signedUser, offeredSwapItem, moneyOffered, DateTime.Now);
            }

            // TODO: Return json ?
            return Redirect("/Home/Index");
        }

        [HttpPost]
        public ActionResult MarkAsRead(Guid swapRequestGuid)
        {
            var swapRequest = swapRequestService.FindByGuid(swapRequestGuid);

            if (swapRequest == null)
            {
                return Json(new
                {
                    Success = true,
                });
            }

            if (swapRequest.SwapItem.UserId != SessionHelper.SignedUser.UserId)
            {
                return Json(new
                {
                    Success = true,
                });
            }

            var swapRequestStatus = swapRequestService.MarkAsRead(swapRequest);

            if (swapRequestStatus == SwapRequestStatus.Error)
            {
                return Json(new
                {
                    Error = true,
                    Message = "Error happend, try again!"
                });
            }

            return Json(new
            {
                Success = true,
            });
        }

        [HttpPost]
        public ActionResult Accept(Guid swapRequestGuid)
        {
            var swapRequest = swapRequestService.FindByGuid(swapRequestGuid);

            if (swapRequest != null)
            {
                var swapRequestStatus = swapRequestService.AcceptRequest(swapRequest);

                if (swapRequestStatus == SwapRequestStatus.Accepted)
                {
                    return Json(new
                    {
                        Success = true,
                    });
                }
            }

            return Json(new
            {
                Error = true,
            });
        }

        [HttpPost]
        public ActionResult Decline(Guid swapRequestGuid)
        {
            var swapRequest = swapRequestService.FindByGuid(swapRequestGuid);

            if (swapRequest != null)
            {
                var swapRequestStatus = swapRequestService.DeclineRequest(swapRequest);

                if (swapRequestStatus == SwapRequestStatus.Declined)
                {
                    return Json(new
                    {
                        Success = true,
                    });
                }
            }

            return Json(new
            {
                Error = true,
            });
        }

    }
}
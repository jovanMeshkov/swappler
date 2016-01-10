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
        private readonly ISwapItemService swapItemService = new SwapItemService(SwapItem.ImagesPath);
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
        public JsonResult SaveProfile(UserUpdateViewModel userUpdateViewModel)
        {
            if (!ModelState.IsValid || !userUpdateViewModel.DataAvailable)
            {
                return Json(new
                {
                    Error = true,
                    ErrorMessage = "Input errors, fix it!"
                });
            }

            if (userUpdateViewModel.UserId != SessionHelper.SignedUser.UserId)
            {
                return Json(new
                {
                    Error = true,
                    ErrorMessage = "Error happend, try again."
                });
            }

            UserStatus userStatus = userService.Update(userUpdateViewModel);

            if (userStatus == UserStatus.Updated)
            {
                SessionHelper.SignedUser = userService.FindUserById(userUpdateViewModel.UserId);

                return Json(new
                {
                    Success = true,
                    SuccessMessage = userStatus.Description()
                });
            }

            
            return Json(new
            {
                Error = true,
                ErrorMessage = userStatus.Description()
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

    }
}
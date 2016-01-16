using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Swappler.Attributes;
using Swappler.Models.Status;
using Swappler.Services;
using Swappler.Services.Interfaces;
using Swappler.Utilities;
using Swappler.ViewModels;

namespace Swappler.Controllers
{
    public class SwapItemController : Controller
    {
        private readonly IUserService userService = new UserService(Models.User.ImagesPath);
        private readonly ISwapItemService swapItemService = new SwapItemService(Models.SwapItem.ImagesPath);
        private readonly ISwapRequestService swapRequestService = new SwapRequestService();
        
        [Authenticate]
        [HttpGet]
        public ActionResult Show(Guid guid)
        {
            var swapItem = swapItemService.FindByGuid(guid);
            return View(swapItem);
        }

        [HttpGet]
        public ActionResult Publish()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Publish(PublishSwapItemViewModel publishSwapItemViewModel)
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
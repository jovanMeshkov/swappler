using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Swappler.Attributes;
using Swappler.Models;
using Swappler.Models.Status;
using Swappler.Services;
using Swappler.Services.Interfaces;
using Swappler.Utilities;
using Swappler.ViewModels;

namespace Swappler.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService = new UserService(Models.User.ImagesPath);
        
        [Authenticate]
        [HttpGet]
        public ActionResult Profile()
        {
            User user = userService.FindUserById(SessionHelper.SignedUser.UserId);

            return View(user);
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
        public JsonResult UpdateProfile(UserUpdateViewModel userUpdateViewModel)
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
    }
}
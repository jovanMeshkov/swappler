using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
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
        [Route("User/{username?}/Profile")]
        public ActionResult Profile(string username)
        {
            User user = null;
            if (username.IsNullOrWhiteSpace())
            {
                user = userService.FindUserByUsername(SessionHelper.SignedUser.Username);
            }
            else
            {
                user = userService.FindUserByUsername(username);
            }


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
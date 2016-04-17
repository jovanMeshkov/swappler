using System.Web.Helpers;
using System.Web.Mvc;
using Swappler.Models;
using Swappler.Models.Status;
using Swappler.Security;
using Swappler.Services;
using Swappler.Services.Interfaces;
using Swappler.Utilities;
using Swappler.ViewModels;

namespace Swappler.Controllers
{
    public class AuthController : DefaultController
    {
        private readonly IUserService userService = new UserService(Models.User.ImagesPath);

        [HttpGet]
        public ActionResult Login()
        {
            Principal principal = ControllerContext.RequestContext.HttpContext.User as Principal;

            if (principal != null)
            {
                if (principal.Identity.IsAuthenticated)
                {
                    SessionHelper.SignedUser = SignedUser;
                    return Redirect("/Home/Index");
                }
            }

            return View();
        }

        [HttpPost]
        public ActionResult Login(UserLoginViewModel loginViewModel)
        {
            User signedUser;

            var userStatus = userService
                             .ValidateCredentials(loginViewModel.EmailOrUsername, loginViewModel.Password, out signedUser);

            if (userStatus == UserStatus.ValidCredentials)
            {
                SignedUser = signedUser;
                SessionHelper.SignedUser = signedUser;

                var authCookie = CookieHelper.CreateAuthCookie(loginViewModel.EmailOrUsername, CurrentSessionId, signedUser.UserId);
                Response.Cookies.Set(authCookie);

                return Json(new
                {
                    Redirect = true,
                    RedirectTo = "/Home/Index"
                });
            }

            if (userStatus == UserStatus.EmailOrUsernameDoesNotExist ||
                userStatus == UserStatus.InvalidPassword ||
                userStatus == UserStatus.Error)
            {
                return Json(new
                {
                    Error = true,
                    ErrorMessage = userStatus.Description()
                });
            }

            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Register(UserRegistrationViewModel userRegistration)
        {
            if (ModelState.IsValid)
            {
                UserStatus userStatus = userService
                                        .Register(userRegistration.FirstName, 
                                                  userRegistration.LastName, 
                                                  userRegistration.Username,
                                                  userRegistration.Email,
                                                  userRegistration.Password);

                // Successfully registered
                if (userStatus == UserStatus.Registered)
                {
                    return Json(new
                    {
                        Success = true,
                        SuccessMessage = userStatus.Description()
                    });
                }
                
                // Info messages
                if (userStatus == UserStatus.EmailAlreadyExist ||
                    userStatus == UserStatus.UsernameAlreadyExist ||
                    userStatus == UserStatus.EmailAndUsernameExist)
                {
                    return Json(new
                    {
                        Info = true,
                        InfoMessage = userStatus.Description()
                    });
                }
                
                // Errors
                if (userStatus == UserStatus.Error)
                {
                    return Json(new
                    {
                        Error = true,
                        ErrorMessage = "Error happened, try again!"
                    });
                }
            }

            // Something weird happend, say it is error
            return Json(new
            {
                Error = true,
                ErrorMessage = "Error happened, try again!"
            });
        }

        public ActionResult Logout()
        {
            HttpContext.Request.Cookies.Clear();
            Session.Clear();
            Session.Abandon();

            var redirectToAction = RedirectToAction("Login", "Auth");
            return redirectToAction;
        }
    }
}
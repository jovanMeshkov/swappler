using System.Web.Mvc;
using Swappler.Models;
using Swappler.Models.Status;
using Swappler.Services;
using Swappler.Services.Interfaces;
using Swappler.Utilities;
using Swappler.ViewModels;

namespace Swappler.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserService userService = new UserService(Models.User.ImagesPath);

        [HttpGet]
        public ActionResult Login()
        {
            var authCookieValue = CookieHelper.AuthCookieValue();
            var signedUser = SessionHelper.SignedUser;

            if (signedUser != null)
            {
                if (authCookieValue == signedUser.UserId)
                {
                    return Redirect("/Home/Index");
                }
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel loginViewModel)
        {
            User signedUser;

            var userStatus = userService
                             .ValidateCredentials(loginViewModel.EmailOrUsername, loginViewModel.Password, out signedUser);

            if (userStatus == UserStatus.ValidCredentials)
            {
                SessionHelper.SignedUser = signedUser;

                var authenticationCookie = CookieHelper.AuthCookie(signedUser.UserId);
                Response.Cookies.Set(authenticationCookie);

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
            Session.Clear();
            Session.Abandon();

            var redirectToAction = RedirectToAction("Login", "Auth");
            return redirectToAction;
        }
    }
}
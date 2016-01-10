using System.Web.Mvc;
using Swappler.Models;
using Swappler.Services;
using Swappler.Services.Interfaces;
using Swappler.Utilities;

namespace Swappler.Attributes
{
    public class AuthenticateAttribute : ActionFilterAttribute
    {
        private IUserService userService = new UserService(User.ImagesPath);

        public override void OnActionExecuting(ActionExecutingContext actionExecutingContext)
        {
            var authCookieValue = CookieHelper.AuthCookieValue();
            var signedUser = SessionHelper.SignedUser;

            if (authCookieValue == -1 ||
                signedUser == null ||
                authCookieValue != signedUser.UserId)
            {
                actionExecutingContext.Result = new RedirectResult("/Login");
            }
        }
    }
}
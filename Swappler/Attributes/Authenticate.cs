using System.Web.Mvc;
using Swappler.Services;
using Swappler.Services.Interfaces;
using Swappler.Utilities;

namespace Swappler.Attributes
{
    public class AuthenticateAttribute : ActionFilterAttribute
    {
        private IUserService userService = new UserService();

        public override void OnActionExecuting(ActionExecutingContext actionExecutingContext)
        {
            var authCookieValue = CookieHelper.AuthCookieValue();
            var signedUserId = SessionHelper.SignedUserId;

            if (authCookieValue == -1 ||
                signedUserId == null ||
                authCookieValue != signedUserId)
            {
                actionExecutingContext.Result = new RedirectResult("/Login");
            }
        }
    }
}
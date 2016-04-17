using System.Web.Mvc;
using Swappler.Models;
using Swappler.Security;
using Swappler.Services;
using Swappler.Services.Interfaces;
using Swappler.Utilities;

namespace Swappler.Attributes
{
    public class AuthenticateAttribute : ActionFilterAttribute
    {
        private IUserService userService = new UserService(User.ImagesPath);

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Principal principal = context.RequestContext.HttpContext.User as Principal;

            if (principal != null)
            {
                if (principal.Identity.IsAuthenticated == false)
                {
                    context.Result = new RedirectResult("/Login");
                }
                SessionHelper.SignedUser = userService.FindUserById(principal.UserId);
            }
            else
            {
                context.Result = new RedirectResult("/Login");
            }
        }
    }
}
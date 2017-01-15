using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Swappler.Models;
using Swappler.Security;
using Swappler.Services;

namespace Swappler.Controllers
{
    public abstract class DefaultController : Controller
    {
        private User signedUser;
        public string CurrentSessionId
        {
            get
            {
                if (HttpContext.Session != null)
                {
                    return HttpContext.Session.SessionID;
                }
                return null;
            }
        }

        public User SignedUser
        {
            get
            {
                if (signedUser != null)
                {
                    return signedUser;
                }

                var principal = User as Principal;
                if (principal != null)
                {
                    signedUser = new UserService(Models.User.ImagesPath).FindUserById(principal.UserId);
                }
                return signedUser;
            }
            set
            {
                signedUser = value;
            }
        }
    }
}
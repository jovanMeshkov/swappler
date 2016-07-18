using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using Swappler.Security;
using Swappler.Services;
using Swappler.Utilities;

namespace Swappler
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            var cookies = Context.Request.Cookies;
            var session = Context.Session;

            Principal principal = null;

            if (cookies[CookieHelper.AuthCookieName] != null)
            {
                var authCookieData = cookies[CookieHelper.AuthCookieName].Value;

                if (authCookieData != null)
                {
                    try
                    {
                        var authCookieTicket = FormsAuthentication.Decrypt(authCookieData);

                        if (!authCookieTicket.Expired)
                        {
                            var authUserData = new AuthUserData();

                            if (authUserData.Import(authCookieTicket.UserData))
                            {
                                var isAuthenticated = false;

                                if (session.IsNewSession || session.SessionID != authUserData.SessionId)
                                {
                                    isAuthenticated = false;
                                }
                                else
                                {
                                    isAuthenticated = true;
                                }

                                principal = new Principal(authUserData.UserId.ToString(), isAuthenticated)
                                {
                                    UserId = authUserData.UserId
                                };
                            }
                        }
                    }
                    catch (Exception exception)
                    {

                    }
                }
            }

            Context.User = principal;
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            
        }

    }
}

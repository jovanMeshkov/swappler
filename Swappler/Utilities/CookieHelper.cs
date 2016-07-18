using System;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Security;
using Microsoft.AspNet.SignalR.Hubs;
using Swappler.Security;

namespace Swappler.Utilities
{
    public static class CookieHelper
    {
        public static readonly string AuthCookieName = "sa";

        public static HttpCookieCollection Cookies
        {
            get
            {
                return HttpContext.Current.Request.Cookies;
            }
        }

        public static HttpCookie CreateAuthCookie(string loginIdentifier, string sessionId, long userId)
        {
            var authUserData = new AuthUserData(sessionId, userId);
            var formsAuthTicket = new FormsAuthenticationTicket(
                1,
                loginIdentifier,
                DateTime.Now,
                DateTime.Now.AddDays(7), 
                true,
                authUserData.Export());

            var authTicketEncryptedData = FormsAuthentication.Encrypt(formsAuthTicket);
            var httpCookie = new HttpCookie(AuthCookieName, authTicketEncryptedData);
            return httpCookie;
        }

        public static AuthUserData AuthCookieData()
        {
            AuthUserData authUserData = null;

            if (Cookies[AuthCookieName] != null)
            {
                var authHttpCookie = Cookies[AuthCookieName].Value;
                var formsAuthTicket = FormsAuthentication.Decrypt(authHttpCookie);

                if (formsAuthTicket != null)
                {
                    authUserData = new AuthUserData();
                    authUserData.Import(formsAuthTicket.UserData);
                }
            }

            return authUserData;
        }

        //public static HttpCookie CreateAuthCookie(long userId)
        //{
        //    var httpCookie = new HttpCookie(UserIdKey, userId.ToString());

        //    httpCookie.Expires = DateTime.Now.AddDays(7);

        //    return httpCookie;
        //}

        //public static long UserIdCookieValue()
        //{
        //    var httpCookie = Cookies.Get(UserIdKey);

        //    if (httpCookie == null)
        //    {
        //        return -1;
        //    }

        //    long userId;

        //    if (long.TryParse(httpCookie.Value,out userId))
        //    {
        //        return userId;
        //    }

        //    return -1;
        //}
    }
}
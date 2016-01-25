using System;
using System.Runtime.Remoting.Contexts;
using System.Web;
using Microsoft.AspNet.SignalR.Hubs;

namespace Swappler.Utilities
{
    public static class CookieHelper
    {
        public static readonly string UserIdKey = "sw-userid";

        public static HttpCookieCollection Cookies
        {
            get
            {
                return HttpContext.Current.Request.Cookies;
            }
        }

        public static HttpCookie CreateUserIdCookie(long userId)
        {
            var httpCookie = new HttpCookie(UserIdKey, userId.ToString());

            httpCookie.Expires = DateTime.Now.AddDays(7);

            return httpCookie;
        }

        public static long UserIdCookieValue()
        {
            var httpCookie = Cookies.Get(UserIdKey);

            if (httpCookie == null)
            {
                return -1;
            }

            long userId;

            if (long.TryParse(httpCookie.Value,out userId))
            {
                return userId;
            }

            return -1;
        }
    }
}
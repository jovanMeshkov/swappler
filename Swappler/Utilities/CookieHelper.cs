using System;
using System.Web;

namespace Swappler.Utilities
{
    public static class CookieHelper
    {
        public static HttpCookieCollection Cookies
        {
            get { return HttpContext.Current.Request.Cookies; }
        }

        public static HttpCookie AuthCookie(long userId)
        {
            var httpCookie = new HttpCookie("user-id", userId.ToString());

            httpCookie.Expires = DateTime.Now.AddDays(7);

            return httpCookie;
        }

        public static long AuthCookieValue()
        {
            var httpCookie = Cookies.Get("user-id");

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
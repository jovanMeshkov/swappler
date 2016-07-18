using System;
using System.Web;
using System.Web.SessionState;
using Swappler.Models;

namespace Swappler.Utilities
{
    public static class SessionHelper
    {
        private static HttpSessionState Session
        {
            get { return HttpContext.Current.Session; }
        }

        public static User SignedUser
        {
            get { return (User)Session["SignedUser"]; }
            set { Session["SignedUser"] = value; }
        }

        public static DateTime? FirstSwapItemDate
        {
            get { return (DateTime?)Session["FirstSwapItemDate"]; }
            set { Session["FirstSwapItemDate"] = value; }
        }

        public static DateTime? LastSwapItemDate
        {
            get { return (DateTime?) Session["LastSwapItemDate"]; }
            set { Session["LastSwapItemDate"] = value; }
        }
    }
}
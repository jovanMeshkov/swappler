﻿using System;
using System.Web;
using System.Web.SessionState;

namespace Swappler.Utilities
{
    public static class SessionHelper
    {
        private static HttpSessionState Session
        {
            get { return HttpContext.Current.Session; }
        }

        public static long? SignedUserId
        {
            get { return (long?)Session["SignedUser"]; }
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
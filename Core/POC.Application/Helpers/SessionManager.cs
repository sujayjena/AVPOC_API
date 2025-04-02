using POC.Application.Models;
using Microsoft.AspNetCore.Http;

namespace POC.Application.Helpers
{
    public class SessionManager
    {
        private static long _loggedInUserId;
        public static long LoggedInUserId { set { _loggedInUserId = value; } get { return _loggedInUserId; } }

        public SessionManager()
        {
            //UsersLoginSessionData? sessionData = (UsersLoginSessionData?)new HttpContextAccessor().HttpContext.Items["SessionData"]!;
            //if (sessionData != null)
            //{
            //    LoggedInUserId = sessionData.UserId ?? 0;
            //}
        }

        //public static void InitializesSessionData()
        //{ 

        //}
    }
}

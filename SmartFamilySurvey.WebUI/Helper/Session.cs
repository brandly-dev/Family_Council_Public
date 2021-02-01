
using Microsoft.AspNetCore.Http;
using System;

namespace MatSource.Library.Helper
{
    public class Session
    {
        public static Func<IHttpContextAccessor> ContextAccessor = () => new HttpContextAccessor();
        public static string userID
        {
            get { return ContextAccessor().HttpContext.Session.GetString("userID"); }
        }
        public static string role
        {
            get { return ContextAccessor().HttpContext.Session.GetString("role"); }
        }
        public static string email
        {
            get { return ContextAccessor().HttpContext.Session.GetString("email"); }
        }
        public static string userName
        {
            get { return ContextAccessor().HttpContext.Session.GetString("userName"); }
        }
        
    }
}

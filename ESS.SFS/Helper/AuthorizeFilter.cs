using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;

namespace MatSource.Library.Helper
{
    public class AuthorizeFilter : ActionFilterAttribute
    {
        public static Func<IHttpContextAccessor> ContextAccessor = () => new HttpContextAccessor();
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            var myAttr = filterContext.ActionDescriptor
            .FilterDescriptors
            .Where(x => x.Filter is AllowFilter)
            .ToArray();
            if (myAttr.Length == 1)
            {
                return;
            }

            if (ContextAccessor().HttpContext.Session.GetString("userID") == null)
            {
                filterContext.Result = new RedirectResult("/Account/Login");
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}

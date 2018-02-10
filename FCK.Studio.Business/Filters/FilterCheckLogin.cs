using FCK.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FCK.Studio.Business.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    
    public class FilterCheckLogin : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //检查登录是否过期
            if (CookieHelper.getCookie("RegisterId") == null)
            {
                string loginUrl = "/Home/Index";
                filterContext.HttpContext.Response.Redirect(loginUrl, true);
            }
        }
    }
}

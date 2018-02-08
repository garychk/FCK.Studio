using FCK.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FCK.Studio.Admin.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class CheckLogin : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //检查登录是否过期
            if (CookieHelper.getCookie("AdminID") == null || CookieHelper.getCookie("RegisterId") == null)
            {
                //string loginUrl = "/Home/Login?ReturnUrl=" + filterContext.HttpContext.Server.UrlEncode(filterContext.HttpContext.Request.Url.AbsoluteUri);
                string loginUrl = "/Home/Index";
                filterContext.HttpContext.Response.Redirect(loginUrl, true);
            }
            else
            {
                Utility.SetSession("RegisterId", CookieHelper.CookieVal("RegisterId"));
            }
        }
    }
}
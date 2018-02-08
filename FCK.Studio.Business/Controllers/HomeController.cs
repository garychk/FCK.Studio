using FCK.Studio.Core;
using FCK.Studio.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FCK.Studio.Business.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Login(LoginModel model)
        {
            FCKAdmin admin = new FCKAdmin();
            ErrorMsg obj = admin.Login(model);
            return Json(obj);
        }

        public JsonResult Logout()
        {
            ErrorMsg obj = new ErrorMsg();
            try
            {
                Common.CookieHelper.delCookie("AdminID", "");
                Common.CookieHelper.delCookie("AdminName", "");
                Common.CookieHelper.delCookie("RegisterId", "");
                obj.code = 100;
                obj.message = "SUCCESS";
            }
            catch(Exception e)
            {
                obj.code = 102;
                obj.message = e.Message;
            }
            return Json(obj);
        }
    }
}
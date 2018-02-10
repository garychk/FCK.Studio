using FCK.Studio.Core;
using FCK.Studio.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FCK.Studio.Members.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            MemberDto model = new MemberDto();
            if (MemberId > 0)
            {
                FCKMembers member = new FCKMembers();
                model = member.GetModel(MemberId);
            }
            return View(model);
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Reg()
        {
            return View();
        }

        public ActionResult QuickLogin()
        {
            return View();
        }

        public JsonResult DoLogin(LoginModel model)
        {
            FCKMembers member = new FCKMembers();
            var result = member.Login(model);
            if (result.code == 100)
            {
                Common.CookieHelper.setCookie("MemberId", result.id.ToString(),1);
                Common.CookieHelper.setCookie("MemberName", result.message, 1);
                Common.Utility.SetSession("MemberId", result.id);
            }
            return Json(result);
        }

        public JsonResult DoReg(MemberDto model)
        {
            FCKMembers members = new FCKMembers();
            ErrorMsg result = members.CreateOrUpdate(model);
            return Json(result);
        }

        public JsonResult Logout()
        {
            ErrorMsg obj = new ErrorMsg();
            try
            {
                Common.CookieHelper.delCookie("MemberId", "");
                Common.CookieHelper.delCookie("MemberName", "");
                obj.code = 100;
                obj.message = "SUCCESS";
            }
            catch (Exception e)
            {
                obj.code = 102;
                obj.message = e.Message;
            }
            return Json(obj);
        }

        public JsonResult AddOrder(OrderEdit model)
        {
            FCKOrders core = new FCKOrders();
            var result= core.AddOrder(model);
            return Json(result);
        }

    }
}

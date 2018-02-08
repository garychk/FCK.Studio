using FCK.Common;
using FCK.Studio.Core;
using FCK.Studio.Dto;
using FCK.Studio.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FCK.Studio.Web.Controllers
{
    public class BaseController : Controller
    {
        FCKMembers members = new FCKMembers();

        public static MemberDto member = new MemberDto();

        public BaseController()
        {
            int UserId = Utility.cInt(CookieHelper.CookieVal("UserId"));
            member = members.GetModel(UserId);
            ViewData["UserHeader"] = member.Member_Header;
        }

        public JsonResult UserLogin(LoginModel model)
        {
            ErrorMsg result = members.Login(model);
            if (result.code == 100)
            {
                CookieHelper.setCookie("UserId", result.id.ToString(), 1);
                CookieHelper.setCookie("UserName", model.UserName, 1);
            }
            return Json(result);
        }

        public JsonResult UserRegist(MemberDto model)
        {
            ErrorMsg result = members.CreateOrUpdate(model);
            return Json(result);
        }

        public JsonResult UserLogout()
        {
            ErrorMsg result = new ErrorMsg();
            try
            {
                CookieHelper.delCookie("UserId", "");
                CookieHelper.delCookie("UserName", "");
                result.code = 100;
            }
            catch (Exception e)
            {
                result.code = 102;
                result.message = e.Message;
            }
            return Json(result);
        }

        public FileContentResult VeriCode()
        {
            string code = Common.Utility.CreateRandomCode(5);
            byte[] imageByte = Common.Utility.CreateImage(code);
            Session["VeriCode"] = code;
            return File(imageByte, "image/gif");
        }

        /// <summary>
        /// 验证验证码
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public JsonResult CheckVcode(string code)
        {
            bool result = false;
            try
            {
                if (Session["VeriCode"] != null)
                {
                    if (code.ToLower() == Session["VeriCode"].ToString().ToLower())
                        result = true;
                }
            }
            catch (Exception) { }
            return Json(result);
        }

        public JsonResult SendEmail(MailSendDto input)
        {
            ErrorMsg result = new ErrorMsg();
            try
            {
                string textBoxMailBody = "您好！我是" + input.inputName + "，" + input.inputContent + "。我的联系电话：" + input.inputTel + "，电子邮箱：" + input.inputEmail + "。";
                string msg = Utility.SendEmail(input.inputMailto, input.inputTitle, textBoxMailBody);
                if (msg == "true")
                    result.code = 100;
                else
                {
                    result.code = 102;
                    result.message = msg;
                }
            }
            catch (Exception e)
            {
                result.code = 102;
                result.message = e.Message;
            }
            return Json(result);
        }

    }
}

using FCK.Common;
using FCK.Studio.Core;
using FCK.Studio.Design.Models;
using FCK.Studio.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FCK.Studio.Design.Controllers
{
    public class BaseController : Controller
    {
        public FileContentResult VeriCode()
        {
            string code = Common.Utility.CreateRandomCode(5);
            byte[] imageByte = Common.Utility.CreateImage(code);
            Session["VeriCode"] = code;
            return File(imageByte, "image/gif");
        }

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

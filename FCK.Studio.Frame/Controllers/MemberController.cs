using FCK.Studio.Frame.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace FCK.Studio.Frame.Controllers
{
    public class MemberController : BaseController
    {
        public JsonResult Login(string UserName, string Password, string IP = "")
        {
            ReturnData<string> obj = new ReturnData<string>();
            try
            {
                Dictionary<string, string> para = new Dictionary<string, string>();
                para.Add("partner", api_partner);
                para.Add("UserName", UserName);
                para.Add("Password", Password);
                para.Add("IP", IP);
                string result = HttpHelper.HttpWebPost(api_url + "Member/Login", para);
                try
                {
                    obj = JsonConvert.DeserializeObject<ReturnData<string>>(result);
                    if (obj.code == 100)
                    {
                        CookieHelper.setCookie("UserId", obj.id.ToString(), 1);
                        CookieHelper.setCookie("UserName", obj.message, 1);
                    }
                }
                catch
                {
                    obj.code = 102;
                    obj.message = result;
                }
            }
            catch (Exception e)
            {
                obj.code = 102;
                obj.message = e.Message;
            }

            return Json(obj);
        }

    }
}

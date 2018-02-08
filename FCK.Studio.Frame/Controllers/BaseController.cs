using FCK.Studio.Frame.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Text;
using System.Web.Mvc;

namespace FCK.Studio.Frame.Controllers
{
    public class BaseController : Controller
    {
        public static string api_url = ConfigurationManager.AppSettings["api_url"];
        public static string api_partner = ConfigurationManager.AppSettings["api_partner"];
        public static string api_key = ConfigurationManager.AppSettings["api_key"];
        public static string imgServer = ConfigurationManager.AppSettings["imgServer"];

        public JsonResult Get<T>(string action)
        {
            ReturnData<T> obj = new ReturnData<T>();
            try
            {
                Dictionary<string, string> para = new Dictionary<string, string>();
                para.Add("partner", api_partner);
                NameValueCollection form = Request.Form;
                foreach (string key in form.Keys)
                {
                    para.Add(key, form[key]);
                }

                var request = HttpHelper.BuildRequest(para, Encoding.GetEncoding("utf-8"));
                string result = HttpHelper.HttpWebGet(api_url + action + "?" + request);
                try
                {
                    var datas = JsonConvert.DeserializeObject<T>(result);
                    obj.code = 100;
                    obj.datas = datas;
                    obj.message = "SUCCESS";
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

        public JsonResult Post<T>(string action)
        {
            ReturnData<T> obj = new ReturnData<T>();
            try
            {
                Dictionary<string, string> para = new Dictionary<string, string>();
                para.Add("partner", api_partner);
                NameValueCollection form = Request.Form;
                foreach (string key in form.Keys)
                {
                    para.Add(key, form[key]);
                }

                string result = HttpHelper.HttpWebPost(api_url + action, para);
                try
                {
                    var datas = JsonConvert.DeserializeObject<T>(result);
                    obj.code = 100;
                    obj.datas = datas;
                    obj.message = "SUCCESS";
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

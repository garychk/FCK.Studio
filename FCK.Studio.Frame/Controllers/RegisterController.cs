using FCK.Studio.Frame.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace FCK.Studio.Frame.Controllers
{
    public class RegisterController : BaseController
    {
        public JsonResult GetModelByNumber()
        {
            ReturnData<RegisterDto> obj = new ReturnData<RegisterDto>();
            try
            {
                Dictionary<string, string> para = new Dictionary<string, string>();
                para.Add("partner", api_partner);
                para.Add("number", api_partner);
                var request = HttpHelper.BuildRequest(para, Encoding.GetEncoding("utf-8"));
                string result = HttpHelper.HttpWebGet(api_url + "Register/GetModelByNumber?" + request);
                try
                {
                    obj = JsonConvert.DeserializeObject<ReturnData<RegisterDto>>(result);                    
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

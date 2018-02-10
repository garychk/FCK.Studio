using FCK.Studio.API.Filter;
using FCK.Studio.Core;
using FCK.Studio.Dto;
using System.Web.Http;

namespace FCK.Studio.API.Controllers
{
    [BasicAuthentication]
    public class CommonController : ApiController
    {
        [HttpGet]
        public bool IsMobile(string phone)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(phone, @"^[1]+[1,2,3,4,5,6,7,8,9]+\d{9}");
        }

        [HttpGet]
        public string GetCity()
        {
            return FCK.Common.Utility.GetCity();
        }

        [HttpGet]
        public string GetIP()
        {
            return FCK.Common.Utility.GetIP();
        }

        [HttpPost]
        public string SendEmail(MailDto model)
        {
            return FCK.Common.Utility.SendEmail(model.inputMailsTo, model.inputMailSubject, model.textBoxMailBody, model.mailServer, model.mailAcount, model.mailPassword);
        }

        [HttpGet]
        public FCK.Common.SmsResponse SendSms(string mobile, string content)
        {
            return FCK.Common.JisuAPI.SmsSend(mobile, content);
        }
    }
}

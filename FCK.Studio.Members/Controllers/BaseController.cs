using FCK.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FCK.Studio.Members.Controllers
{
    public class BaseController : Controller
    {
        /// <summary>
        /// 邮件服务器
        /// </summary>
        public string MailServer = ConfigurationManager.AppSettings["MailServer"];
        /// <summary>
        /// 邮箱账号
        /// </summary>
        public string MailAcount = ConfigurationManager.AppSettings["MailAcount"];
        /// <summary>
        /// 邮箱密码
        /// </summary>
        public string MailPassword = ConfigurationManager.AppSettings["MailPassword"];
        /// <summary>
        /// 当前会员编号（ID）
        /// </summary>
        public int MemberId = Utility.cInt(CookieHelper.CookieVal("MemberId"));

    }
}

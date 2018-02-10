using FCK.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FCK.Studio.Business.Controllers
{
    public class BaseController : Controller
    {
        /// <summary>
        /// 邮件服务器
        /// </summary>
        public static string MailServer = ConfigurationManager.AppSettings["MailServer"];
        /// <summary>
        /// 邮箱账号
        /// </summary>
        public static string MailAcount = ConfigurationManager.AppSettings["MailAcount"];
        /// <summary>
        /// 邮箱密码
        /// </summary>
        public static string MailPassword = ConfigurationManager.AppSettings["MailPassword"];
        /// <summary>
        /// 当前商户编号（ID）
        /// </summary>
        public int RegisterId = Utility.cInt(CookieHelper.CookieVal("RegisterId"));
        /// <summary>
        /// 图片服务器
        /// </summary>
        public static string ImgServer = ConfigurationManager.AppSettings["ImgServer"];

    }
}

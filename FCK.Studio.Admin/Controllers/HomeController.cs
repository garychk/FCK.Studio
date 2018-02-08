using FCK.Common;
using FCK.Studio.Admin.Filters;
using FCK.Studio.Core;
using FCK.Studio.Dto;
using System.Web.Mvc;

namespace FCK.Studio.Admin.Controllers
{
    public class HomeController : Controller
    {
        private FCKAdmin admin = new FCKAdmin();
        
        public ActionResult Index()
        {
            AdminsJson items = admin.GetAdmins(1, 0);
            if (items.datas.Count <= 0)
                ViewBag.AdminCount = 0;
            else
                ViewBag.AdminCount = items.datas.Count;
            ViewData["PageTitle"] = FCKSystem.GetVal("Company");
            return View();
        }

        [CheckLogin]
        public ActionResult Dashboard()
        {
            ViewData["AdminName"] = CookieHelper.CookieVal("AdminName");
            ViewData["PageTitle"] = FCKSystem.GetVal("Company");
            return View();
        }

        [CheckLogin]
        public ActionResult Main()
        {
            return View();
        }

        [CheckLogin]
        public ActionResult LogDetail(int id = 0)
        {
            ViewData["LogID"] = id;
            return View();
        }

        public JsonResult GetLogs(int page, int pageSize, string stime = "", string etime = "")
        {
            FCKLog log = new FCKLog();
            LogJson result = log.GetLogs(page, pageSize, stime, etime);
            return Json(result);
        }

        public JsonResult GetLogDetail(int logid = 0)
        {
            FCKLog log = new FCKLog();
            LogData obj = log.GetDetail(logid);
            return Json(obj);
        }
        
    }
}

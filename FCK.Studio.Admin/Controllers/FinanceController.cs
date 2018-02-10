using FCK.Studio.Admin.Filters;
using FCK.Studio.Core;
using FCK.Studio.Dto;
using System.Collections.Generic;
using System.Web.Mvc;

namespace FCK.Studio.Admin.Controllers
{
    [CheckLogin]
    public class FinanceController : Controller
    {
        FCKFinance core = new FCKFinance();

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetFinances(int page, int pageSize, string membername = "", string ordernumber = "", string stime = "", string etime = "", string orderindex = "timedesc")
        {
            FinanceJson result = core.GetFinances(page, pageSize, membername, ordernumber, stime, etime, orderindex);
            return Json(result);
        }

        public JsonResult GetFinanceChartData(string stime = "", string etime = "")
        {
            int regid = Common.Utility.cInt(Common.CookieHelper.CookieVal("RegisterId"));
            List<FinanceChartData> result = core.GetFinanceChartData(stime, etime, regid);
            return Json(result);
        }
    }
}

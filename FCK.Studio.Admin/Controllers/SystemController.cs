using FCK.Studio.Admin.Filters;
using FCK.Studio.Core;
using FCK.Studio.Dto;
using System.Collections.Generic;
using System.Web.Mvc;

namespace FCK.Studio.Admin.Controllers
{
    [CheckLogin]
    public class SystemController : Controller
    {
        FCKSystem core = new FCKSystem();

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetCodeTypes()
        {
            List<string> lists = new List<string>();
            lists = core.GetCodeTypes();
            return Json(lists);
        }

        public JsonResult GetCodes(int page, int pageSize, string codename = "", string codetype = "")
        {
            CodeJson result = core.GetCodes(page, pageSize, codename, codetype);
            return Json(result);
        }

        public JsonResult EditCode(CodeData model)
        {
            ErrorMsg result = core.EditCode(model);
            return Json(result);
        }

        public JsonResult DelCode(int codeid)
        {
            ErrorMsg result = core.DelCode(codeid);
            return Json(result);
        }

        public JsonResult GetModel(int codeid)
        {
            CodeData result = core.GetModel(codeid);
            return Json(result);
        }

        public JsonResult GetVal(string codename)
        {
            string result = FCKSystem.GetVal(codename);
            return Json(result);
        }
    }
}

using FCK.Studio.Admin.Filters;
using FCK.Studio.Core;
using FCK.Studio.Dto;
using System.Web.Mvc;

namespace FCK.Studio.Admin.Controllers
{
    [CheckLogin]
    public class RegistersController : Controller
    {
        FCKRegisters core = new FCKRegisters();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Edit(int id = 0)
        {
            var result = core.GetModel(id);
            RegisterDto model = new RegisterDto();
            if (result.code == 100)
                model = result.datas;
            return View(model);
        }

        public ActionResult Setting(int id = 0)
        {
            var result = core.GetModel(id);
            RegisterDto model = new RegisterDto();
            if (result.code == 100)
                model = result.datas;
            return View(model);
        }

        public JsonResult GetPageList(int page, int pageSize, string keywords = "")
        {
            PageDatas<RegisterDto> result = core.GetPageList(page, pageSize, keywords);
            return Json(result);
        }

        public JsonResult CreateOrUpdate(RegisterDto model)
        {
            if (model.Register_ID <= 0)
                model.Register_DeadLine = System.DateTime.Now.AddYears(1);
            var result = core.CreateOrUpdate(model);
            return Json(result);
        }

        public JsonResult UpdateStatus(string ids, int status)
        {
            ErrorMsg result = new ErrorMsg();
            string[] idarr = ids.Split(',');
            foreach (var item in idarr)
            {
                int id = FCK.Common.Utility.cInt(item);
                result = core.UpdateStatus(id, status);
            }
            return Json(result);
        }

        public JsonResult UpdateDeadLine(string ids, int days)
        {
            ErrorMsg result = new ErrorMsg();
            string[] idarr = ids.Split(',');
            foreach (var item in idarr)
            {
                int id = FCK.Common.Utility.cInt(item);
                result = core.UpdateDeadLine(id, days);
            }
            return Json(result);
        }

        public JsonResult UpdatePower(int registid, string powers)
        {
            ErrorMsg result = core.UpdatePower(registid, powers);
            return Json(result);
        }
    }
}

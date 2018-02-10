using FCK.Studio.Admin.Filters;
using FCK.Studio.Core;
using FCK.Studio.Dto;
using System.Web.Mvc;

namespace FCK.Studio.Admin.Controllers
{
    [CheckLogin]
    public class FckAPIController : Controller
    {
        FCKAPI core = new FCKAPI();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Edit(int id=0)
        {
            var result = core.GetModel(id);
            FCKAPIDto model = new FCKAPIDto();
            if (result.code == 100)
            {
                model = result.datas;
            }
            return View(model);
        }

        public JsonResult Del(string ids)
        {
            ErrorMsg result = new ErrorMsg();
            var idarr = ids.Split(',');
            foreach (var item in idarr)
            {
                int ID = Common.Utility.cInt(item);
                result = core.Del(ID);
            }
            return Json(result);
        }

        public ActionResult Paras(int id = 0)
        {
            var result = core.GetModel(id);
            FCKAPIDto model = new FCKAPIDto();
            if (result.code == 100)
            {
                model = result.datas;
            }
            return View(model);
        }

        public ActionResult ParasEdit(int id = 0)
        {
            var result = core.GetPara(id);
            FCKAPIParaDto model = new FCKAPIParaDto();
            if (result.code == 100)
            {
                model = result.datas;
            }
            return View(model);
        }

        public JsonResult GetAPIs(int page, int pageSize, string keywords = "")
        {
            PageDatas<FCKAPIDto> result = core.GetPageList(page, pageSize, keywords);
            return Json(result);
        }

        public JsonResult CreateOrUpdate(FCKAPIDto input)
        {
            var result = core.CreateOrUpdate(input);
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

        public JsonResult GetParas(int page, int pageSize, string keywords = "", int apid = 0)
        {
            PageDatas<FCKAPIParaDto> result = core.GetParas(page, pageSize, keywords, apid);
            return Json(result);
        }

        public JsonResult EditPara(FCKAPIParaDto input)
        {
            var result = core.EditPara(input);
            return Json(result);
        }

    }
}

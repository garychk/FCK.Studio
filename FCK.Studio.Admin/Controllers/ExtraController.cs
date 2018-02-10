using FCK.Studio.Admin.Filters;
using FCK.Studio.Core;
using FCK.Studio.Dto;
using System.Web.Mvc;

namespace FCK.Studio.Admin.Controllers
{
    [CheckLogin]
    public class ExtraController : Controller
    {
        FCKExtra core = new FCKExtra();

        public JsonResult GetPageList(int page, int pageSize, string keywords = "", int outid = 0)
        {
            var result = core.GetPageList(page, pageSize, keywords, outid);
            return Json(result);
        }

        public JsonResult CreateOrUpdate(ExtraDto model)
        {
            ErrorMsg result = core.CreateOrUpdate(model);
            return Json(result);
        }

        public JsonResult Del(string ID)
        {
            ErrorMsg result = new ErrorMsg();
            string[] ids = ID.Split(',');
            foreach (var item in ids)
            {
                int id = Common.Utility.cInt(item);
                result = core.Del(id);
            }
            return Json(result);
        }

    }
}

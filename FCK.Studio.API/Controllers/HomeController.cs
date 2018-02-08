using FCK.Studio.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace FCK.Studio.API.Controllers
{
    public class HomeController : Controller
    {
        Core.FCKAPI core = new Core.FCKAPI();
        Core.FCKCategory category = new Core.FCKCategory();

        public ActionResult Index()
        {
            List<APIDTO> model = new List<APIDTO>();
            var apis = core.GetPageList(1, 0);
            var APIRoot = category.GetList(0, "API", 0).FirstOrDefault();
            if (APIRoot != null)
            {
                var cates = category.GetList(APIRoot.Category_ID, "API", 0).ToList();
                foreach (var item in cates)
                {
                    APIDTO obj = new APIDTO();
                    obj.CateName = item.Category_Name;
                    if (apis.datas != null)
                    {
                        obj.APIS = apis.datas.Where(o => o.CateId == item.Category_ID).ToList();
                    }
                    model.Add(obj);
                }
            }
            return View(model);
        }

        public ActionResult Paras(int id = 0)
        {
            var api = core.GetModel(id);
            if (api.code == 100)
            {
                ViewData["API_Title"] = api.datas.Title;
                ViewData["API"] = api.datas.API;
            }
            var result = core.GetParas(1, 0, "", id);
            List<FCKAPIParaDto> model = new List<FCKAPIParaDto>();
            if (result.total > 0)
            {
                model = result.datas;
            }
            return View(model);
        }

        public ActionResult Apitest()
        {
            return View();
        }
    }
}

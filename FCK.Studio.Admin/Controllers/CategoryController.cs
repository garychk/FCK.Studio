using FCK.Studio.Admin.Filters;
using FCK.Studio.Core;
using FCK.Studio.Dto;
using System.Collections.Generic;
using System.Web.Mvc;

namespace FCK.Studio.Admin.Controllers
{
    [CheckLogin]
    public class CategoryController : Controller
    {
        FCKCategory core = new FCKCategory();

        public ActionResult Index(int id = 0)
        {
            ViewData["ParentID"] = id;
            return View();
        }

        public ActionResult Edit(int id)
        {
            CategoryDto model = core.GetCate(id);
            ViewData["ParentName"] = "顶级";
            if (model.Category_ParentID > 0)
            {
                ViewData["ParentName"] = core.GetCateName(model.Category_ParentID);
            }
            return View(model);
        }

        public JsonResult GetCateTree(int parentid = 0, string ctype = "")
        {
            List<TreeNode> result = core.GetCateTree(parentid, ctype);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCates(int parentid, string ctype = "")
        {
            List<CategoryDto> result = core.GetList(parentid, ctype);
            return Json(result);
        }

        public JsonResult EditCate(CategoryDto model)
        {
            ErrorMsg result = core.CreateOrUpdate(model);
            return Json(result);
        }

        public JsonResult DelCate(int id)
        {
            ErrorMsg result = core.Del(id);
            return Json(result);
        }

        public JsonResult GetCate(int id)
        {
            CategoryDto result = core.GetCate(id);
            return Json(result);
        }

    }
}

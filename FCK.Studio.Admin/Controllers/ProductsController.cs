using FCK.Studio.Admin.Filters;
using FCK.Studio.Core;
using FCK.Studio.Dto;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace FCK.Studio.Admin.Controllers
{
    [CheckLogin]
    public class ProductsController : Controller
    {
        FCKProducts core = new FCKProducts();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Edit(int id = 0)
        {
            ProductDto model = core.GetModel(id);
            return View(model);
        }

        public ActionResult Extra(int id = 0)
        {
            ViewData["productid"] = id;
            return View();
        }

        public ActionResult ExtraEdit(int id = 0)
        {
            FCKExtra extra = new FCKExtra();
            var result = extra.GetModel(id);
            ExtraDto model = new ExtraDto();
            if (result.code == 100)
                model = result.datas;
            return View(model);
        }

        public JsonResult GetProducts(int page, int pageSize, int cateid = 0, string keywords = "", string pricerange = "", string orderindex = "order", int isrec = 0)
        {
            PageDatas<ProductDto> result = core.GetPageList(page, pageSize, cateid, keywords, pricerange, orderindex, isrec);
            return Json(result);
        }

        public JsonResult EditProduct(ProductDto model)
        {
            model.Product_Content = HttpUtility.UrlDecode(model.Product_Content);
            ErrorMsg result = core.CreateOrUpdate(model);
            return Json(result);
        }

        public JsonResult GetModel(int ID)
        {
            ProductDto result = core.GetModel(ID);
            return Json(result);
        }

        public JsonResult DelProduct(int ID)
        {
            ErrorMsg result = core.Del(ID);
            return Json(result);
        }

        public JsonResult RecProduct(string ID)
        {
            ErrorMsg result = core.Recommend(ID);
            return Json(result);
        }

        public JsonResult GetCates(string cates, string sname = "")
        {
            List<string> result = core.GetCates(cates, sname);            
            return Json(result);
        }

        public JsonResult GetPrice(string name, string grade)
        {
            ProductDto result = core.GetPrice(name, grade);
            return Json(result);
        }
    }
}

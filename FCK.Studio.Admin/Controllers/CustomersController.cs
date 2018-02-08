using FCK.Studio.Admin.Filters;
using FCK.Studio.Core;
using FCK.Studio.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FCK.Studio.Admin.Controllers
{
    [CheckLogin]
    public class CustomersController : Controller
    {
        FCKCustomers core = new FCKCustomers();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Edit(int id = 0)
        {
            CustomerDto model = new CustomerDto();
            var result = core.GetModel(id);
            if (result.code == 100)
                model = result.datas;
            return View(model);
        }

        public JsonResult GetPageList(int page, int pageSize, string keywords = "", int memberid = 0)
        {
            PageDatas<CustomerDto> result = core.GetPageList(page, pageSize, keywords, memberid);
            return Json(result);
        }

        public JsonResult CreateOrUpdate(CustomerDto model)
        {
            ErrorMsg result = core.CreateOrUpdate(model);
            return Json(result);
        }

        public JsonResult GetModel(int ID)
        {
            CustomerDto model = new CustomerDto();
            var result = core.GetModel(ID);
            if (result.code == 100)
                model = result.datas;
            return Json(model);
        }

        public JsonResult Del(int ID)
        {
            ErrorMsg result = core.Del(ID);
            return Json(result);
        }

    }
}

using FCK.Studio.Admin.Filters;
using FCK.Studio.Core;
using FCK.Studio.Dto;
using System.Collections.Generic;
using System.Web.Mvc;

namespace FCK.Studio.Admin.Controllers
{
    [CheckLogin]
    public class OrdersController : Controller
    {
        FCKOrders core = new FCKOrders();

        public ActionResult Index(string id)
        {
            ViewData["OrderNumber"] = id;
            return View();
        }

        public ActionResult ODList(string id)
        {
            ViewData["OrderNumber"] = id;
            return View();
        }
        public ActionResult Edit()
        {
            return View();
        }
        public ActionResult Search()
        {
            return View();
        }
        public ActionResult GetService(string id)
        {
            ViewData["OrderNumber"] = id;
            return View();
        }

        public JsonResult GetOrders(int page, int pageSize, string OrderNumber = "", int MemberID = 0, string stime = "", string etime = "", int status = -1, string OrderIndex = "")
        {
            OrdersJson result = core.GetPageList(page, pageSize, OrderNumber, MemberID, stime, etime, status, OrderIndex);
            return Json(result);
        }

        public JsonResult GetOrderDetails(string Order_Number)
        {
            List<OrderDetailData> result = new List<OrderDetailData>();
            result = core.GetDetails(Order_Number);
            return Json(result);
        }

        public JsonResult UpdateOrderStatus(string OrderNumber, int status)
        {
            ErrorMsg result = core.UpdateStatus(OrderNumber, status);
            return Json(result);
        }
        

        public JsonResult AddOrder(OrderEdit model)
        {
            ErrorMsg result = core.AddOrder(model);
            return Json(result);
        }

        public JsonResult TakeService(string OrderNumber, string OrderCode)
        {
            ErrorMsg result = core.TakeService(OrderNumber, OrderCode);
            return Json(result);
        }

        

    }
}

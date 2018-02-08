using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FCK.Studio.Business.Filters;
using FCK.Studio.Dto;
using FCK.Studio.Core;
using FCK.Common;

namespace FCK.Studio.Business.Controllers
{
    [FilterCheckLogin]
    public class RegisterController : BaseController
    {
        FCKRegisters core = new FCKRegisters();
        public ActionResult Index()
        {
            var result = core.GetModel(RegisterId);
            RegisterDto model = new RegisterDto();
            if (result.code == 100)
                model = result.datas;
            return View(model);
        }

        public ActionResult Message()
        {
            FCKLog logs = new FCKLog();
            var model = logs.GetLogs(1, 0, "", "", -1, RegisterId);
            return View(model);
        }

        public ActionResult Products()
        {
            FCKProducts products = new FCKProducts();
            var model = products.GetPageList(1, 10, 0, "", "", "", 0, RegisterId);
            return View(model);
        }

        public ActionResult Orders()
        {
            FCKOrders orders = new FCKOrders();
            var model = orders.GetPageList(1, 0, "", 0, "", "", -1, "time_desc", RegisterId);
            return View(model);
        }

        public ActionResult OrderDetail(int id)
        {
            OrderDetailDto model = new OrderDetailDto();
            FCKOrders orders = new FCKOrders();
            var order = orders.GetModel(id);
            if (order.code == 100)
            {
                if (order.datas != null)
                {
                    var OD = order.datas;
                    var ODT = orders.GetDetails(OD.Order_Number);
                    model.Order = OD;
                    model.OrderDetails = ODT;
                }          
                
            }
            return View(model);
        }

        public ActionResult Finance()
        {
            FCKFinance finance = new FCKFinance();
            var model = finance.GetPageList(1, 0, 0, "", "", "", "", RegisterId);
            return View(model);
        }

        public ActionResult Setting()
        {
            var result = core.GetModel(RegisterId);
            RegisterDto model = new RegisterDto();
            if (result.code == 100)
                model = result.datas;
            return View(model);
        }
        public JsonResult CreateOrUpdate(RegisterDto model)
        {
            var result = core.CreateOrUpdate(model);
            return Json(result);
        }

        public JsonResult GetAmount(string type, string keywords,string stime, string etime)
        {
            FCKFinance finance = new FCKFinance();
            var result = finance.GetPageList(1, 0, 0, "", stime, etime, "", RegisterId);
            decimal amount = 0;
            if (result.total > 0)
            {
                if (!string.IsNullOrEmpty(type))
                {
                    result.datas = result.datas.Where(o => o.Finance_Type == type).ToList();
                }
                if (!string.IsNullOrEmpty(keywords))
                {
                    result.datas = result.datas.Where(o => o.Finance_Memo.Contains(keywords)).ToList();
                }
                foreach (var item in result.datas)
                {
                    amount += Utility.cDecimal(item.Finance_Amount);
                }
            }

            return Json(amount);
        }

        public JsonResult GetFinanceChartData(string stime = "", string etime = "")
        {
            FCKFinance finance = new FCKFinance();
            List<FinanceChartData> result = finance.GetFinanceChartData(stime, etime, RegisterId);
            return Json(result);
        }
        public JsonResult GetHighchartDatas(string stime = "", string etime = "")
        {
            FCKOrders orders = new FCKOrders();
            var result = orders.GetVSales(1, 0, 2, 0, RegisterId, "", stime, etime);

            List<HighchartDatas> model = new List<HighchartDatas>();
            if(result.total>0)
            {
                var datas = result.datas;
                var data = (from a in datas
                            group a by new { a.Category_Name } into b
                            select new
                            {
                                name = b.Key.Category_Name,
                                amount = b.Sum(c => c.Order_Amount)
                            });
                decimal taoalamount = 0;
                foreach(var it in data)
                {
                    taoalamount += it.amount;
                }
                int k = 1;
                foreach (var item in data)
                {
                    HighchartDatas obj = new HighchartDatas();
                    obj.name = item.name;
                    obj.amount = item.amount.ToString("0.00");
                    obj.y = Math.Round(item.amount / taoalamount, 2);
                    if (k == 1)
                    {
                        obj.sliced = true;
                        obj.selected = true;
                    }
                    model.Add(obj);
                    k++;
                }
            }
            return Json(model);
        }

        public JsonResult RecProduct(string ids)
        {
            FCKProducts products = new FCKProducts();
            ErrorMsg result = products.Recommend(ids);
            return Json(result);
        }

        public JsonResult OnSaleProduct(string ids, int status = 0)
        {
            FCKProducts products = new FCKProducts();
            ErrorMsg result = products.OnSale(ids, status);
            return Json(result);
        }
    }

    public class HighchartDatas
    {
        public string name { get; set; }
        public string amount { get; set; }
        public decimal y { get; set; }
        public bool sliced { get; set; }
        public bool selected { get; set; }
    }

    public class OrderDetailDto
    {
        public OrdersData Order { get; set; }
        public List<OrderDetailData> OrderDetails { get; set; }
    }
}
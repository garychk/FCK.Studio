using FCK.Studio.Core;
using FCK.Studio.Dto;
using FCK.Studio.Members.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FCK.Studio.Members.Controllers
{
    [FilterCheckLogin]
    public class MemberController : BaseController
    {
        FCKMembers core = new FCKMembers();
        public ActionResult Index()
        {
            MemberDto model = new MemberDto();
            if (MemberId > 0)
            {
                model = core.GetModel(MemberId);
            }
            return View(model);
        }

        public ActionResult Message()
        {
            FCKLog logs = new FCKLog();
            var model = logs.GetLogs(1, 0, "", "", -1, -1, MemberId);
            return View(model);
        }

        public ActionResult Shares()
        {
            FCKShares shares = new FCKShares();
            var model = shares.GetPageList(1, 0, MemberId);
            return View(model);
        }

        public ActionResult Collections()
        {
            FCKCollections core = new FCKCollections();
            var model = core.GetByPageList(1, 0, MemberId, "", 1);
            return View(model);
        }

        public ActionResult Orders()
        {
            FCKOrders orders = new FCKOrders();
            var model = orders.GetPageList(1, 0, "", MemberId, "", "", -1, "time_desc");
            return View(model);
        }

        public ActionResult OrderDetail(string id)
        {
            OrderDetailDto model = new OrderDetailDto();
            FCKOrders orders = new FCKOrders();
            var order = orders.GetOrderByNumber(id);
            if(order.code==100)
            {
                var orderdetail = orders.GetDetails(id);
                model.Order = order.datas;
                model.OrderDetails = orderdetail;
            }
            return View(model);
        }

        public ActionResult Finance()
        {
            FCKFinance finance = new FCKFinance();
            var model = finance.GetPageList(1, 0, MemberId);
            return View(model);
        }

        public ActionResult Setting()
        {
            MemberDto model = new MemberDto();
            if (MemberId > 0)
            {
                model = core.GetModel(MemberId);
            }
            return View(model);
        }

        public JsonResult CreateOrUpdate(MemberDto model)
        {
            var result = core.CreateOrUpdate(model);
            return Json(result);
        }

        public JsonResult GetAmount(string type, string keywords)
        {
            FCKFinance finance = new FCKFinance();
            var result = finance.GetPageList(1, 0, MemberId);
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
                    amount += Common.Utility.cDecimal(item.Finance_Amount);
                }
            }

            return Json(amount);
        }

    }

    public class OrderDetailDto
    {
        public OrdersData Order { get; set; }
        public List<OrderDetailData> OrderDetails { get; set; }
    }
}

using FCK.Studio.Core;
using FCK.Studio.Dto;
using FCK.Studio.Members.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FCK.Studio.Members.Controllers
{
    public class ProductsController : BaseController
    {
        FCKProducts core = new FCKProducts();
        public ActionResult _PartialProducts(int page, int pageSize, int cateid = 0, string keywords = "", string pricerange = "", string orderindex = "")
        {
            var model = core.GetPageList(page, pageSize, cateid, keywords, pricerange, orderindex, 0, -1, 1);
            return PartialView(model);
        }

        public ActionResult Detail(int id = 0)
        {
            ProductDto model = new ProductDto();
            if (id > 0)
            {
                model = core.GetModel(id);
            }
            return View(model);
        }

        public ActionResult Order()
        {
            OrderPageDto model = new OrderPageDto();
            string ProductIds = Request.Form["ProductIds"].ToString();
            int ProductNumber = Common.Utility.cInt(Request.Form["ProductNumber"]);
            string ShareCode = Request.Form["ShareCode"].ToString();
            int Register_ID = 0;
            List<OrderDetailEdit> ODE = new List<OrderDetailEdit>();
            if (!string.IsNullOrEmpty(ProductIds))
            {
                var Array_ProductId = ProductIds.Split(',');
                foreach (var id in Array_ProductId)
                {
                    var prod = core.GetModel(Common.Utility.cInt(id));
                    if (prod != null)
                    {
                        OrderDetailEdit item = new OrderDetailEdit();
                        item.Product_ID = prod.Product_ID;
                        item.Product_Name = prod.Product_Name;
                        item.Product_Number = ProductNumber;
                        item.Product_Price = prod.Product_Price;
                        item.Product_Pic = prod.Product_Pic;
                        Register_ID = prod.Register_ID;
                        ODE.Add(item);
                    }
                }
            }
            FCKMembers members = new FCKMembers();
            model.Member = members.GetModel(MemberId);
            model.OrderDetail = JsonConvert.SerializeObject(ODE);
            model.Products = ODE;
            model.Register_ID = Register_ID;
            model.Share_Code = ShareCode;
            return View(model);
        }

        public ActionResult _PartialShopInfo(int registerid)
        {
            FCKRegisters registers = new FCKRegisters();
            RegisterDto model = new RegisterDto();
            var result = registers.GetModel(registerid);
            if (result.code == 100)
            {
                model = result.datas;
            }
            return PartialView(model);
        }

        /// <summary>
        /// 会员余额支付
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonResult PayByLocal(OrderEdit model)
        {
            ErrorMsg result = new ErrorMsg();
            FCKOrders orders = new FCKOrders();

            result = orders.AddOrder(model);

            return Json(result);
        }
        /// <summary>
        /// 支付结果，传递订单编号参数
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult PayResult(string id)
        {
            PayResultDto model = new PayResultDto();
            FCKOrders orders = new FCKOrders();
            var order = orders.GetOrderByNumber(id).datas;
            if (order != null)
            {
                //会员余额支付
                if (order.Order_Payway.ToLower() == "local")
                {
                    var flag = orders.CheckMemberAmount(order.Order_Amount.ToString(), order.Member_ID);
                    if (flag.code == 100)
                    {
                        orders.PayOK(order.Order_Number, order.Order_Amount.ToString());
                    }
                }
                //网络支付，需要第三方返回结果
                else
                {
                    orders.PayOK(id, model.Orders.Order_Amount.ToString());
                }
            }
            model.Orders = orders.GetOrderByNumber(id).datas;
            model.OrdersDetails = orders.GetDetails(id);

            return View(model);
        }

        public JsonResult AddFav(CollectionsDto data)
        {
            FCKCollections collection = new FCKCollections();
            var result = collection.AddProd(data);
            return Json(result);
        }

        public JsonResult Share(ShareDto model)
        {
            ErrorMsg result = new ErrorMsg();
            if (model.Member_ID > 0)
            {
                FCKShares shares = new FCKShares();
                string share_code = Common.Utility.CreateRandomCode(6);
                model.Share_Code = share_code;
                model.Share_Time = DateTime.Now;
                bool flag = true;
                var items = shares.GetPageList(1, 0, model.Member_ID);
                if (items.total > 0)
                {
                    foreach (var item in items.datas)
                    {
                        if (model.Product_ID == item.Product_ID && model.Member_ID == item.Member_ID)
                        {
                            flag = false;
                            break;
                        }
                    }
                }
                if (flag)
                {
                    result = shares.CreateOrUpdate(model);
                }
                else
                {
                    result.code = 101;
                    result.message = "您已分享";
                }

            }
            return Json(result);
        }

    }
}

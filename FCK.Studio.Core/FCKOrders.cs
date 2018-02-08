using FCK.Common;
using FCK.Studio.Entity;
using FCK.Studio.Dto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace FCK.Studio.Core
{
    public class FCKOrders : FCKBase
    {
        public FCKOrders()
        {
            try
            {
                string SMSAccount = FCKSystem.GetVal("SMSAccount");
                string SMSPassword = FCKSystem.GetVal("SMSPassword");
                if (!string.IsNullOrEmpty(SMSAccount) && !string.IsNullOrEmpty(SMSPassword))
                {
                    SMS.ACCOUNT = SMSAccount;
                    SMS.AUTHKEY = SMSPassword;
                }
            }
            catch (Exception err)
            {
                FCKLog.AddLog(err.Message, enumLogType.system);
            }
        }

        /// <summary>
        /// 添加订单详情
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ErrorMsg AddOrderDetail(OrderDetailEdit model)
        {
            ErrorMsg result = new ErrorMsg();
            try
            {
                FCK_OrderDetail OrderDetail = new FCK_OrderDetail();
                OrderDetail = Utility.MapTo<FCK_OrderDetail>(model);
                OrderDetail.Register_ID = RegisterID;
                db.FCK_OrderDetail.Add(OrderDetail);
                db.SaveChanges();
                result.code = 100;
                result.id = OrderDetail.OrderDetail_ID;
                result.message = "SUCCESS";
            }
            catch (Exception err)
            {
                result.code = 100;
                result.message = err.Message;
            }
            return result;
        }
        /// <summary>
        /// 添加订单，成功返回code=100，message=订单编号
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ErrorMsg AddOrder(OrderEdit model)
        {
            ErrorMsg result = new ErrorMsg();
            try
            {
                #region 添加订单信息
                FCK_Orders Order = new FCK_Orders();
                string OrderNumber = Utility.CreateRandNum("");
                Random rnd = new Random();
                int ordercode = rnd.Next(100000, 999999);
                FCKMembers members = new FCKMembers();
                var member = members.GetModel(model.Member_ID);
                Order.Member_ID = member.Member_ID;
                Order.Member_Mobile = member.Member_Mobile;
                Order.Member_Name = member.Member_Name;
                Order.Member_Telphone = model.Member_Telphone;
                Order.Order_Amount = model.Order_Amount;
                Order.Order_Discount = model.Order_Discount;
                Order.Order_Memo = model.Order_Memo;
                Order.Order_Number = OrderNumber;
                Order.Order_Status = model.Order_Status;
                Order.Order_Time = DateTime.Now;
                Order.Order_Code = ordercode.ToString();
                Order.Register_ID = model.Register_ID;
                Order.Order_PayMoney = model.Order_PayMoney;
                Order.Order_Payway = model.Order_Payway;
                Order.Share_Code = model.Share_Code;
                db.FCK_Orders.Add(Order);
                db.SaveChanges();
                #endregion
                if (Order.Order_ID > 0)
                {
                    #region 添加订单详情
                    List<OrderDetailEdit> OrderDetails = JsonConvert.DeserializeObject<List<OrderDetailEdit>>(model.OrderDetail);
                    ErrorMsg result2 = new ErrorMsg();
                    List<int> OD = new List<int>();
                    foreach (var item in OrderDetails)
                    {
                        item.Order_Number = OrderNumber;
                        result2 = AddOrderDetail(item);
                        if (result2.code == 102)
                        {
                            result.code = 102;
                            result.message = result2.message;
                            break;
                        }
                        else
                        {
                            OD.Add(result2.id);
                        }
                    }
                    //订单详情添加失败，则回滚删除其它订单详情记录
                    if (result2.code == 102)
                    {
                        foreach (int id in OD)
                        {
                            var obj = dbr.FCK_OrderDetail.Where(o => o.OrderDetail_ID == id).FirstOrDefault();
                            if (obj != null)
                            {
                                db.Entry(obj).State = EntityState.Deleted;
                                db.SaveChanges();
                            }
                        }
                        //订单详情添加失败，则回滚删除主订单记录
                        db.Entry(Order).State = EntityState.Deleted;
                        db.SaveChanges();

                    }

                    result.code = 100;
                    result.message = OrderNumber;
                    #endregion
                }
                else
                {
                    result.code = 102;
                    result.message = "ADD_ORDER_ERROR";
                }

            }
            catch (Exception err)
            {
                FCKLog.AddLog(err.Message, enumLogType.order);
                result.code = 102;
                result.message = err.Message;
            }
            return result;
        }
        /// <summary>
        /// 检查会员余额是否充足
        /// </summary>
        public ErrorMsg CheckMemberAmount(string money, int memberid)
        {
            ErrorMsg result = new ErrorMsg();
            try
            {
                FCKMembers members = new FCKMembers();
                var member = members.GetModel(memberid);
                var Paymongy = Utility.cDecimal(money);
                if (member.Member_Amount > Paymongy)
                    result.code = 100;
                else
                    result.code = 103;
            }
            catch (Exception err)
            {
                result.code = 102;
                result.message = err.Message;
            }
            return result;
        }


        /// <summary>
        /// 添加会员账单信息
        /// </summary>
        /// <returns></returns>
        public ErrorMsg AddMemberFinance(string amount, string ordernumber, int memberid, string type = "", string memo = "")
        {
            ErrorMsg result = new ErrorMsg();
            try
            {
                FCKFinance finance = new FCKFinance();
                EditFinance item = new EditFinance();
                item.Finance_Amount = amount;
                item.Order_Number = ordernumber;
                item.Member_ID = memberid;
                item.Finance_Type = type;
                item.Finance_Time = DateTime.Now;
                item.Finance_Memo = memo;
                finance.AddFinance(item);
                result.code = 100;
                result.id = item.Finance_ID;
                result.message = "SUCCESS";
            }
            catch (Exception ex)
            {
                result.code = 102;
                result.message = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 添加商户账单信息
        /// </summary>
        /// <returns></returns>
        public ErrorMsg AddRegisterFinance(string amount, string ordernumber, int registerid, string type = "", string memo = "")
        {
            ErrorMsg result = new ErrorMsg();
            try
            {
                FCKFinance finance = new FCKFinance();
                EditFinance item = new EditFinance();
                item.Finance_Amount = amount;
                item.Order_Number = ordernumber;
                item.Register_ID = registerid;
                item.Finance_Type = type;
                item.Finance_Time = DateTime.Now;
                item.Finance_Memo = memo;
                finance.AddFinance(item);
                result.code = 100;
                result.id = item.Finance_ID;
                result.message = "SUCCESS";
            }
            catch (Exception ex)
            {
                result.code = 102;
                result.message = ex.Message;
            }
            return result;
        }
        /// <summary>
        /// 更新订单状态
        /// </summary>
        /// <param name="ordernumber"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public ErrorMsg UpdateStatus(string ordernumber, int status)
        {
            ErrorMsg result = new ErrorMsg();
            try
            {
                var item = dbr.FCK_Orders.Where(o => o.Order_Number == ordernumber).FirstOrDefault();
                if (item != null)
                {
                    if (IsAllow(status, item.Order_Status))
                    {
                        item.Order_Status = status;
                        db.Entry(item).State = EntityState.Modified;
                        db.SaveChanges();
                        result.code = 100;
                        result.message = "UPDATE_SUCCESS";
                    }
                    else
                    {
                        result.code = 102;
                        result.message = "NOT_ALLOW";
                    }
                }
                else
                {
                    result.code = 101;
                    result.message = "OBJECT_NOT_EXIST";
                }
            }
            catch (Exception err)
            {
                result.code = 102;
                result.message = err.Message;
            }
            return result;
        }
        /// <summary>
        /// 支付成功操作
        /// </summary>
        public ErrorMsg PayOK(string ordernumber, string payamount)
        {
            ErrorMsg result = new ErrorMsg();
            var order = dbr.FCK_Orders.Where(o => o.Order_Number == ordernumber).FirstOrDefault();
            if (order != null)
            {
                var order_detail = GetDetails(ordernumber);
                ErrorMsg IsOk = UpdateStatus(ordernumber, 2);
                if (IsOk.code == 100)
                {
                    #region 更新会员积分和余额
                    FCKMembers members = new FCKMembers();
                    var member = members.GetModel(order.Member_ID);
                    if (order.Order_Payway.ToLower() == "local")
                    {
                        member.Member_Amount -= Utility.cDecimal(payamount);
                    }
                    member.Member_Points += 1;
                    members.CreateOrUpdate(member);
                    AddMemberFinance(payamount, ordernumber, order.Member_ID, enumFinanceType.payout.ToString());
                    #endregion

                    #region 更新产品销量
                    FCKProducts products = new FCKProducts();
                    foreach (var item in order_detail)
                    {
                        products.UpdateSales(item.Product_ID, 1);
                    }
                    #endregion

                    #region 分享利润                    
                    decimal sharemoney = 0; //分享佣金
                    if (!string.IsNullOrEmpty(order.Share_Code))
                    {
                        FCKShares shares = new FCKShares();
                        var share = shares.GetModel(order.Share_Code);
                        if (share.code == 100)
                        {
                            sharemoney = share.datas.Share_Money;
                            var sharemember = share.datas.Member_ID;
                            var shareregid = share.datas.Register_ID;
                            member = members.GetModel(sharemember);
                            member.Member_Amount += sharemoney;
                            members.CreateOrUpdate(member);
                            AddMemberFinance(sharemoney.ToString(), ordernumber, sharemember, enumFinanceType.comein.ToString(), "分享佣金");

                        }
                    }
                    #endregion

                    #region 更新商户余额
                    FCKRegisters registers = new FCKRegisters();
                    var reg = registers.GetModel(order.Register_ID);
                    if (reg.code == 100)
                    {
                        var commission = Utility.cDecimal(FCKSystem.GetVal("Commission")) * Utility.cDecimal(payamount); //平台佣金
                        commission = Math.Round(commission, 2);
                        var avamoney = Utility.cDecimal(payamount) - sharemoney - commission; //最终商户所得收入
                        var register = reg.datas;
                        register.Register_Money += avamoney;
                        registers.CreateOrUpdate(register);
                        AddRegisterFinance(payamount, ordernumber, order.Register_ID, enumFinanceType.comein.ToString());
                        if (sharemoney > 0)
                        {
                            AddRegisterFinance(sharemoney.ToString(), ordernumber, order.Register_ID, enumFinanceType.payout.ToString(), "分享佣金");
                        }
                        if (commission > 0)
                        {
                            AddRegisterFinance(commission.ToString(), ordernumber, order.Register_ID, enumFinanceType.payout.ToString(), "平台抽成");
                        }

                    }
                    #endregion

                    SendOrderSms(ordernumber, payamount, order.Register_ID, order.Member_ID);

                    result.code = 100;
                    result.id = order.Order_ID;
                    result.message = order.Order_Number;
                }
            }
            else
            {
                result.code = 101;
                result.message = "ORDER_NOT_EXIST";
            }
            return result;
        }

        /// <summary>
        /// 向商户和会员发送通知短信
        /// </summary>
        public ErrorMsg SendOrderSms(string ordernumber, string paymoney, int registerid, int memberid)
        {
            ErrorMsg result = new ErrorMsg();
            try
            {
                FCKMembers members = new FCKMembers();
                var member = members.GetModel(memberid);
                //向会员发送短信通知
                string smsContent = Utility.SMS_NewOrder;
                smsContent = smsContent.Replace("[UserName]", member.Member_Name);
                smsContent = smsContent.Replace("[OrderNumber]", ordernumber);
                smsContent = smsContent.Replace("[OrderAmount]", paymoney);
                smsContent = smsContent.Replace("[LeftAmount]", member.Member_Amount.ToString("0"));
                JisuAPI.SmsSend(member.Member_Mobile, smsContent);

                //向商户发送短信通知
                FCKRegisters registers = new FCKRegisters();
                var register = registers.GetModel(registerid);
                if (register.code == 100)
                {
                    smsContent = Utility.SMS_NewOrder;
                    smsContent = smsContent.Replace("[UserName]", register.datas.Register_Name);
                    smsContent = smsContent.Replace("[OrderNumber]", ordernumber);
                    smsContent = smsContent.Replace("[OrderAmount]", paymoney);
                    smsContent = smsContent.Replace("[LeftAmount]", "0");
                    JisuAPI.SmsSend(register.datas.Register_Mobile, smsContent);
                }
                result.id = 100;
                result.message = "SUCCESS";
            }
            catch (Exception ex)
            {
                FCKLog.AddLog(ex.Message, enumLogType.sms);
                result.id = 102;
                result.message = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 获取订单数据
        /// </summary>
        public OrdersJson GetPageList(int page, int pageSize, string OrderNumber = "", int MemberID = 0, string stime = "", string etime = "", int status = -1, string OrderIndex = "", int regid = 0)
        {
            OrdersJson result = new OrdersJson();
            var items = dbr.FCK_Orders.ToList();
            if (regid > 0 || RegisterID > 0)
            {
                items = items.Where(o => o.Register_ID == regid).ToList();
            }
            if (!string.IsNullOrEmpty(OrderNumber))
            {
                items = items.Where(o => o.Order_Number.Contains(OrderNumber) || o.Member_Telphone == OrderNumber || o.Member_Mobile == OrderNumber).ToList();
            }
            if (!string.IsNullOrEmpty(stime) && !string.IsNullOrEmpty(etime))
            {
                DateTime s = DateTime.Parse(stime);
                DateTime e = DateTime.Parse(etime);
                items = items.Where(o => o.Order_Time >= s && o.Order_Time <= e).ToList();
            }
            if (MemberID > 0)
            {
                items = items.Where(o => o.Member_ID == MemberID).ToList();
            }
            if (status > 0)
            {
                items = items.Where(o => o.Order_Status == status).ToList();
            }
            if (!string.IsNullOrEmpty(OrderIndex))
            {
                switch (OrderIndex.ToLower())
                {
                    case "amount_desc":
                        items = items.OrderByDescending(o => o.Order_Amount).ToList();
                        break;
                    case "amount":
                        items = items.OrderBy(o => o.Order_Amount).ToList();
                        break;
                    case "time_desc":
                        items = items.OrderByDescending(o => o.Order_Time).ToList();
                        break;
                    case "time":
                        items = items.OrderBy(o => o.Order_Time).ToList();
                        break;
                }

            }

            int total = items.Count;
            int pages = 1;
            if (pageSize > 0)
            {
                pages = (total + pageSize - 1) / pageSize;
                int startIndex = pageSize * (page - 1);
                items = items.Skip(startIndex).Take(pageSize).ToList();
            }

            List<OrdersData> lists = new List<OrdersData>();
            foreach (var item in items)
            {
                OrdersData obj = new OrdersData();
                obj.Member_Name = item.Member_Name;
                obj.Order_Amount = item.Order_Amount.ToString("0.00");
                obj.Order_ID = item.Order_ID;
                obj.Order_Number = item.Order_Number;
                obj.Order_Status = item.Order_Status;
                obj.Order_Time = item.Order_Time.ToString();
                obj.Member_ID = item.Member_ID;
                obj.Order_Payway = item.Order_Payway;
                obj.Order_Memo = item.Order_Memo;
                obj.Member_Telphone = item.Member_Telphone;
                obj.Member_Mobile = item.Member_Mobile;
                lists.Add(obj);
            }

            result.datas = lists;
            result.pages = pages;
            result.total = total;

            return result;
        }

        public List<OrderDetailData> GetDetails(string OrderNumber)
        {
            List<OrderDetailData> lists = new List<OrderDetailData>();
            var items = dbr.FCK_OrderDetail.Where(o => o.Order_Number == OrderNumber).ToList();
            var products = dbr.FCK_Products.ToList();
            foreach (var item in items)
            {
                OrderDetailData obj = new OrderDetailData();
                obj.Order_Number = item.Order_Number;
                obj.OrderDetail_ID = item.OrderDetail_ID;
                obj.Product_ID = item.Product_ID;
                obj.Product_Name = item.Product_Name;
                obj.Product_Number = item.Product_Number;
                obj.Product_Code = item.Product_Code;
                obj.Product_Grade = item.Product_Grade;
                if (products.Count > 0)
                {
                    var product = products.Where(o => o.Product_ID == item.Product_ID).FirstOrDefault();
                    obj.Product_Price = product.Product_Price.ToString("0.00");
                    obj.Product_Pic = product.Product_Pic;
                }
                lists.Add(obj);
            }
            return lists;
        }
        /// <summary>
        /// 领取服务
        /// </summary>
        /// <param name="OrderNumber"></param>
        /// <param name="OrderCode"></param>
        /// <returns></returns>
        public ErrorMsg TakeService(string OrderNumber, string OrderCode)
        {
            ErrorMsg result = new ErrorMsg();
            try
            {
                var item = dbr.FCK_Orders.Where(o => o.Order_Number == OrderNumber).FirstOrDefault();
                if (item != null)
                {
                    if (item.Order_Code != OrderCode)
                    {
                        result.code = 102;
                        result.message = "CODE_ERROR";
                    }
                    else
                    {
                        result = UpdateStatus(OrderNumber, 3);
                    }
                }
                else
                {
                    result.code = 101;
                    result.message = "OBJECT_NOT_EXIST";
                }
            }
            catch (Exception err)
            {
                result.code = 102;
                result.message = err.Message;
            }
            return result;
        }

        /// <summary>
        /// 检验订单状态更改
        /// </summary>
        /// <param name="status"></param>
        /// <param name="currentStatus"></param>
        /// <returns></returns>
        public bool IsAllow(int status, int currentStatus)
        {
            return status > currentStatus ? true : false;
        }

        public ReturnData<OrdersData> GetModel(int id)
        {
            ReturnData<OrdersData> result = new ReturnData<OrdersData>();
            try
            {
                FCK_Orders item = dbr.FCK_Orders.Where(o => o.Order_ID == id).FirstOrDefault();
                result.datas = Utility.MapTo<OrdersData>(item);
                result.code = 100;
                result.message = "SUCCESS";
            }
            catch (Exception e)
            {
                result.code = 102;
                result.message = e.Message;
            }
            return result;
        }

        public ReturnData<OrdersData> GetOrderByNumber(string OrderNumber)
        {
            ReturnData<OrdersData> result = new ReturnData<OrdersData>();
            try
            {
                var order = GetPageList(1, 0, OrderNumber).datas.FirstOrDefault();
                result.datas = order;
                result.code = 100;
                result.message = "SUCCESS";
            }
            catch (Exception e)
            {
                result.code = 102;
                result.message = e.Message;
            }
            return result;
        }

        /// <summary>
        /// 获取销售记录
        /// </summary>
        public PageDatas<V_SalesDto> GetVSales(int page, int pageSize, int status = -1, int memberid = 0, int regid = 0, string ordernumber = "", string stime = "", string etime = "")
        {
            PageDatas<V_SalesDto> result = new PageDatas<V_SalesDto>();
            var items = dbr.V_Sales.ToList();
            if (status >= 0)
            {
                items = items.Where(o => o.Order_Status == status).ToList();
            }
            if (memberid > 0)
            {
                items = items.Where(o => o.Member_ID == memberid).ToList();
            }
            if (regid >= 0)
            {
                items = items.Where(o => o.Register_ID == regid).ToList();
            }
            if (!string.IsNullOrEmpty(ordernumber))
            {
                items = items.Where(o => ordernumber.Contains(o.Order_Number)).ToList();
            }
            if (!string.IsNullOrEmpty(stime) && !string.IsNullOrEmpty(etime))
            {
                DateTime s = DateTime.Parse(stime);
                DateTime e = DateTime.Parse(etime);
                items = items.Where(o => o.Order_Time >= s && o.Order_Time <= e).ToList();
            }

            var lists = Utility.MapTo<List<V_SalesDto>>(items);

            int total = items.Count;
            int pages = 1;
            if (pageSize > 0)
            {
                pages = (total + pageSize - 1) / pageSize;
                int startIndex = pageSize * (page - 1);
                items = items.Skip(startIndex).Take(pageSize).ToList();
            }
            result.datas = lists;
            result.pages = pages;
            result.total = total;

            return result;
        }
    }


}

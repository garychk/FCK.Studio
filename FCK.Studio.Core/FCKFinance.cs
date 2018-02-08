using FCK.Common;
using FCK.Studio.Entity;
using FCK.Studio.Dto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FCK.Studio.Core
{
    public class FCKFinance : FCKBase
    {
        /// <summary>
        /// 添加财务信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ErrorMsg AddFinance(EditFinance model)
        {
            ErrorMsg result = new ErrorMsg();
            try
            {
                FCKMembers TJCM = new FCKMembers();
                var member = TJCM.GetModel(model.Member_ID);

                FCK_Finance obj = new FCK_Finance();
                obj.Finance_Amount = decimal.Parse(model.Finance_Amount);
                obj.Finance_Number = Utility.CreateRandNum("F");
                obj.Finance_Time = DateTime.Now;
                obj.Finance_Type = model.Finance_Type;
                obj.Member_ID = model.Member_ID;
                obj.Member_Name = member != null ? member.Member_Name : model.Member_Name;
                obj.Order_Number = model.Order_Number;
                obj.Register_ID = model.Register_ID;
                obj.Register_Name = model.Register_Name;
                obj.Finance_Memo = model.Finance_Memo;
                db.FCK_Finance.Add(obj);
                db.SaveChanges();
                result.code = 100;
                result.id = obj.Finance_ID;
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
        /// 获取财务数据
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="membername"></param>
        /// <param name="ordernumber"></param>
        /// <param name="stime"></param>
        /// <param name="etime"></param>
        /// <param name="orderindex"></param>
        /// <returns></returns>
        public FinanceJson GetFinances(int page, int pageSize, string membername = "", string ordernumber = "", string stime = "", string etime = "", string orderindex = "timedesc")
        {
            FinanceJson result = new FinanceJson();
            var lists = dbr.FCK_Finance.Where(o => o.Register_ID == RegisterID).ToList();
            if (!string.IsNullOrEmpty(membername))
            {
                lists = lists.Where(o => o.Member_Name.Contains(membername)).ToList();
            }
            if (!string.IsNullOrEmpty(ordernumber))
            {
                lists = lists.Where(o => o.Order_Number.Contains(ordernumber)).ToList();
            }
            if (!string.IsNullOrEmpty(stime) && !string.IsNullOrEmpty(etime))
            {
                DateTime s = DateTime.Parse(stime);
                DateTime e = DateTime.Parse(etime);
                lists = lists.Where(o => o.Finance_Time >= s && o.Finance_Time <= e).ToList();
            }

            if (orderindex == "time")
                lists = lists.OrderBy(o => o.Finance_Time).ToList();
            else if (orderindex == "amount")
                lists = lists.OrderBy(o => o.Finance_Amount).ToList();
            else if (orderindex == "amountdesc")
                lists = lists.OrderByDescending(o => o.Finance_Amount).ToList();
            else
                lists = lists.OrderByDescending(o => o.Finance_Time).ToList();

            int total = lists.Count;
            int pages = 0;
            if (pageSize > 0)
            {
                pages = (total + pageSize - 1) / pageSize;
                int startIndex = pageSize * (page - 1);
                lists = lists.Skip(startIndex).Take(pageSize).ToList();
            }
            List<FinanceData> items = new List<FinanceData>();
            foreach (var item in lists)
            {
                FinanceData obj = new FinanceData();
                obj.Finance_Amount = item.Finance_Amount.ToString("0.00");
                obj.Finance_ID = item.Finance_ID;
                obj.Finance_Time = item.Finance_Time.ToString();
                obj.Finance_Type = item.Finance_Type;
                obj.Member_Name = item.Member_Name;
                obj.Finance_Memo = item.Finance_Memo;
                items.Add(obj);
            }

            result.datas = items;
            result.pages = pages;
            result.total = total;

            return result;
        }

        public List<FinanceChartData> GetFinanceChartData(string stime = "", string etime = "", int regid = -1)
        {
            List<FinanceChartData> lists = new List<FinanceChartData>();
            FinanceChartData result = new FinanceChartData();
            List<string> datetime = new List<string>();
            List<int> data = new List<int>();
            DateTime s = DateTime.Now;
            DateTime e = DateTime.Now;

            if (string.IsNullOrEmpty(stime) || string.IsNullOrEmpty(etime))
            {
                s = DateTime.Now.AddMonths(-1);
                e = DateTime.Now;
            }
            else
            {
                s = DateTime.Parse(stime);
                e = DateTime.Parse(etime);
            }

            var items = dbr.V_FinanceChart.OrderBy(o => o.Date).ToList();
            if (regid >= 0)
            {
                items = items.Where(o => o.Register_ID == regid).ToList();
            }

            if (e <= DateTime.Now && s < e)
            {
                if (e > s.AddMonths(1))
                {
                    e = s.AddMonths(1);
                }

                List<V_FinanceChart> objs = new List<V_FinanceChart>();
                var datas = items.Where(o => DateTime.Parse(o.Date) >= s && DateTime.Parse(o.Date) <= e).ToList();

                if (datas.Count <= 0)
                {
                    objs = items;
                }
                foreach (var item in objs)
                {
                    try
                    {
                        data.Add(Convert.ToInt32(item.Amount));
                        datetime.Add(Convert.ToDateTime(item.Date).ToString("MM-dd"));
                    }
                    catch { }
                }

                result.name = s.Year.ToString();
                result.data = data;
                result.datetime = datetime;
                lists.Add(result);

            }
            return lists;
        }

        public FinanceJson GetPageList(int page, int pageSize, int memberid = 0, string ordernumber = "", string stime = "", string etime = "", string orderindex = "timedesc", int regid = -1)
        {
            FinanceJson result = new FinanceJson();
            var lists = dbr.FCK_Finance.ToList();
            if (regid >= 0)
            {
                lists = lists.Where(o => o.Register_ID == regid).ToList();
            }
            if (memberid > 0)
            {
                lists = lists.Where(o => o.Member_ID == memberid).ToList();
            }
            if (!string.IsNullOrEmpty(ordernumber))
            {
                lists = lists.Where(o => o.Order_Number.Contains(ordernumber)).ToList();
            }
            if (!string.IsNullOrEmpty(stime) && !string.IsNullOrEmpty(etime))
            {
                DateTime s = DateTime.Parse(stime);
                DateTime e = DateTime.Parse(etime);
                lists = lists.Where(o => o.Finance_Time >= s && o.Finance_Time <= e).ToList();
            }

            if (orderindex == "time")
                lists = lists.OrderBy(o => o.Finance_Time).ToList();
            else if (orderindex == "amount")
                lists = lists.OrderBy(o => o.Finance_Amount).ToList();
            else if (orderindex == "amountdesc")
                lists = lists.OrderByDescending(o => o.Finance_Amount).ToList();
            else
                lists = lists.OrderByDescending(o => o.Finance_Time).ToList();

            int total = lists.Count;
            int pages = 0;
            if (pageSize > 0)
            {
                pages = (total + pageSize - 1) / pageSize;
                int startIndex = pageSize * (page - 1);
                lists = lists.Skip(startIndex).Take(pageSize).ToList();
            }
            List<FinanceData> items = new List<FinanceData>();
            foreach (var item in lists)
            {
                FinanceData obj = new FinanceData();
                obj.Finance_Amount = item.Finance_Amount.ToString("0.00");
                obj.Finance_ID = item.Finance_ID;
                obj.Finance_Time = item.Finance_Time.ToString("yyyy/MM/dd hh:mm:ss");
                obj.Finance_Type = item.Finance_Type;
                obj.Member_Name = item.Member_Name;
                obj.Finance_Memo = item.Finance_Memo;
                items.Add(obj);
            }

            result.datas = items;
            result.pages = pages;
            result.total = total;

            return result;
        }
    }
}

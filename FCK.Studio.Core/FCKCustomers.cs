using FCK.Common;
using FCK.Studio.Dto;
using FCK.Studio.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FCK.Studio.Core
{
    public class FCKCustomers: FCKBase
    {
        public PageDatas<CustomerDto> GetPageList(int page, int pageSize, string keywords = "", int memberid = 0)
        {
            PageDatas<CustomerDto> result = new PageDatas<CustomerDto>();
            var lists = dbr.FCK_Customers.Where(o => o.Register_ID == RegisterID).ToList();
            if (!string.IsNullOrEmpty(keywords))
            {
                lists = lists.Where(o => o.Customer_Name.Contains(keywords) || o.Customer_Company.Contains(keywords)).ToList();
            }
            if (memberid > 0)
            {
                lists = lists.Where(o => o.Member_ID == memberid).ToList();
            }
            lists = lists.OrderBy(o => o.Customer_UpdateTime).ToList();

            int total = lists.Count;
            int pages = 0;
            if (pageSize > 0)
            {
                pages = (total + pageSize - 1) / pageSize;
                int startIndex = pageSize * (page - 1);
                lists = lists.Skip(startIndex).Take(pageSize).ToList();
            }

            result.datas = Utility.MapTo<List<CustomerDto>>(lists);
            result.pages = pages;
            result.total = total;

            return result;
        }

        public ErrorMsg CreateOrUpdate(CustomerDto model)
        {
            ErrorMsg result = new ErrorMsg();
            try
            {
                var obj = dbr.FCK_Customers.Where(o => o.Customer_ID == model.Customer_ID).FirstOrDefault();
                if (obj != null)
                {
                    obj = Utility.MapTo<FCK_Customers>(model);
                    obj.Register_ID = RegisterID;
                    db.Entry(obj).State = EntityState.Modified;
                    db.SaveChanges();
                    result.code = 100;
                    result.message = "SUCCESS";
                }
                else
                {
                    var lists = dbr.FCK_Customers.Where(o => o.Register_ID == RegisterID).ToList();
                    var item = lists.Where(o => o.Customer_Name == model.Customer_Name || o.Customer_Company == model.Customer_Company).FirstOrDefault();
                    if (item == null)
                    {
                        item = Utility.MapTo<FCK_Customers>(model);
                        item.Register_ID = RegisterID;
                        db.FCK_Customers.Add(item);
                        db.SaveChanges();
                        result.code = 100;
                        result.id = item.Customer_ID;
                        result.message = "SUCCESS";
                    }
                    else
                    {
                        result.code = 101;
                        result.message = "OBJECT_EXIST";
                    }
                }

            }
            catch (Exception err)
            {
                result.code = 102;
                result.message = err.Message;
                FCKLog.AddLog(err.Message, enumLogType.system);
            }
            return result;
        }

        public ErrorMsg Del(int ID)
        {
            ErrorMsg result = new ErrorMsg();
            try
            {
                var model = dbr.FCK_Customers.Where(o => o.Customer_ID == ID).FirstOrDefault();
                if (model != null)
                {
                    db.Entry(model).State = EntityState.Deleted;
                    db.SaveChanges();
                    result.code = 100;
                    result.message = "SUCCESS";
                }
                else
                {
                    result.code = 101;
                    result.message = "NOT_EXIST";
                }
            }
            catch (Exception err)
            {
                result.code = 102;
                result.message = err.Message;
            }
            return result;
        }

        public ReturnData<CustomerDto> GetModel(int ID)
        {
            ReturnData<CustomerDto> result = new ReturnData<CustomerDto>();
            try
            {
                var item = dbr.FCK_Customers.Where(o => o.Customer_ID == ID).FirstOrDefault();
                if (item != null)
                {
                    result.code = 100;
                    result.message = "SUCCESS";
                    result.datas = Utility.MapTo<CustomerDto>(item);
                }
                else
                    result.code = 101;
            }
            catch (Exception err)
            {
                result.code = 102;
                result.message = err.Message;
            }
            return result;
        }
    }
}

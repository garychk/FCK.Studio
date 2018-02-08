using FCK.Common;
using FCK.Studio.Dto;
using FCK.Studio.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace FCK.Studio.Core
{
    public class FCKShares : FCKBase
    {
        public PageDatas<ShareDto> GetPageList(int page, int pageSize, int memberid = 0,int registerid = -1, string keywords = "")
        {
            PageDatas<ShareDto> result = new PageDatas<ShareDto>();
            var lists = dbr.FCK_Shares.OrderByDescending(o => o.Share_Time).ToList();

            if (memberid > 0)
            {
                lists = lists.Where(o => o.Member_ID == memberid).ToList();
            }

            if(registerid >= 0)
            {
                lists = lists.Where(o => o.Register_ID == registerid).ToList();
            }

            if (!string.IsNullOrEmpty(keywords))
            {
                lists = lists.Where(o => o.Product_Name.Contains(keywords)).ToList();
            }

            int total = lists.Count;
            int pages = 0;
            if (pageSize > 0)
            {
                pages = (total + pageSize - 1) / pageSize;
                int startIndex = pageSize * (page - 1);
                lists = lists.Skip(startIndex).Take(pageSize).ToList();
            }
            result.pages = pages;
            result.total = total;
            result.datas = Utility.MapTo<List<ShareDto>>(lists);
            return result;
        }

        public ErrorMsg CreateOrUpdate(ShareDto input)
        {
            ErrorMsg result = new ErrorMsg();
            try
            {
                var model = Utility.MapTo<FCK_Shares>(input);
                if (input.Share_ID == 0)
                {
                    db.FCK_Shares.Add(model);
                    db.SaveChanges();
                    result.code = 100;
                    result.message = model.Share_Code;
                }
                else
                {
                    db.Entry(model).State = EntityState.Modified;
                    db.SaveChanges();
                    result.code = 100;
                    result.message = "SUCCESS";
                }

            }
            catch (Exception e)
            {
                result.code = 102;
                result.message = e.Message;
            }
            return result;
        }

        public ReturnData<ShareDto> GetModel(int id)
        {
            ReturnData<ShareDto> result = new ReturnData<ShareDto>();
            try
            {
                var model = dbr.FCK_Shares.Where(o => o.Share_ID == id).FirstOrDefault();
                if (model != null)
                {
                    result.code = 100;
                    result.message = "SUCCESS";
                    result.datas = Utility.MapTo<ShareDto>(model);
                }
                else
                {
                    result.code = 101;
                    result.message = "NO_EXIST";
                }
            }
            catch (Exception e)
            {
                result.code = 102;
                result.message = e.Message;
            }
            return result;
        }

        public ReturnData<ShareDto> GetModel(string code)
        {
            ReturnData<ShareDto> result = new ReturnData<ShareDto>();
            try
            {
                var model = dbr.FCK_Shares.Where(o => o.Share_Code == code).FirstOrDefault();
                if (model != null)
                {
                    result.code = 100;
                    result.message = "SUCCESS";
                    result.datas = Utility.MapTo<ShareDto>(model);
                }
                else
                {
                    result.code = 101;
                    result.message = "NO_EXIST";
                }
            }
            catch (Exception e)
            {
                result.code = 102;
                result.message = e.Message;
            }
            return result;
        }

        public ErrorMsg Del(int id)
        {
            ErrorMsg result = new ErrorMsg();
            try
            {
                var model = dbr.FCK_Shares.Where(o => o.Share_ID == id).FirstOrDefault();
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

        
    }
}

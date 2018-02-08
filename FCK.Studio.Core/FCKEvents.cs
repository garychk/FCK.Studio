using FCK.Common;
using FCK.Studio.Entity;
using FCK.Studio.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace FCK.Studio.Core
{
    public class FCKEvents : FCKBase
    {
        public PageDatas<EventDto> GetPageList(int page, int pageSize, int memberid = 0, string keywords = "", string status = "", string stime = "", string etime = "")
        {
            PageDatas<EventDto> result = new PageDatas<EventDto>();
            var lists = dbr.FCK_Events.Where(o => o.Register_ID == RegisterID).ToList();
            if (!string.IsNullOrEmpty(keywords))
            {
                lists = lists.Where(o => o.Event_Name.Contains(keywords) || o.Event_Code.Contains(keywords)).ToList();
            }
            if (memberid > 0)
            {
                lists = lists.Where(o => o.Event_Leader == memberid || o.Event_Resperson == memberid).ToList();
            }
            if (!string.IsNullOrEmpty(status))
            {
                lists = lists.Where(o => o.Event_Status == status).ToList();
            }
            if (!string.IsNullOrEmpty(stime) && !string.IsNullOrEmpty(etime))
            {
                DateTime s = Utility.cTime(stime);
                DateTime e = Utility.cTime(etime);
                lists = lists.Where(o => o.Event_StartTime >= s && o.Event_StartTime <= e).ToList();
            }
            lists = lists.OrderBy(o => o.Event_StartTime).ToList();

            int total = lists.Count;
            int pages = 0;
            if (pageSize > 0)
            {
                pages = (total + pageSize - 1) / pageSize;
                int startIndex = pageSize * (page - 1);
                lists = lists.Skip(startIndex).Take(pageSize).ToList();
            }

            result.datas = Utility.MapTo<List<EventDto>>(lists);
            result.pages = pages;
            result.total = total;

            return result;
        }

        public ErrorMsg CreateOrUpdate(EventDto model)
        {
            ErrorMsg result = new ErrorMsg();
            try
            {
                var obj = dbr.FCK_Events.Where(o => o.Event_ID == model.Event_ID).FirstOrDefault();
                if (obj != null)
                {
                    obj = Common.Utility.MapTo<FCK_Events>(model);
                    obj.Register_ID = RegisterID;
                    db.Entry(obj).State = EntityState.Modified;
                    db.SaveChanges();
                    result.code = 100;
                    result.message = "SUCCESS";
                }
                else
                {
                    var item = Utility.MapTo<FCK_Events>(model);
                    item.Register_ID = RegisterID;
                    db.FCK_Events.Add(item);
                    db.SaveChanges();
                    result.code = 100;
                    result.id = item.Event_ID;
                    result.message = "SUCCESS";
                }

            }
            catch (Exception err)
            {
                result.code = 102;
                result.message = err.Message;
            }
            return result;
        }

        public ErrorMsg Del(int ID)
        {
            ErrorMsg result = new ErrorMsg();
            try
            {
                var model = dbr.FCK_Events.Where(o => o.Event_ID == ID).FirstOrDefault();
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

        public ReturnData<EventDto> GetModel(int ID)
        {
            ReturnData<EventDto> result = new ReturnData<EventDto>();
            try
            {
                var item = dbr.FCK_Events.Where(o => o.Event_ID == ID).FirstOrDefault();
                if (item != null)
                {
                    result.code = 100;
                    result.message = "SUCCESS";
                    result.datas = Utility.MapTo<EventDto>(item);
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

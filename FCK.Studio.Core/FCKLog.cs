using FCK.Studio.Dal;
using FCK.Studio.Entity;
using FCK.Studio.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace FCK.Studio.Core
{
    public class FCKLog : FCKBase
    {
        public static ErrorMsg AddLog(string content, enumLogType type, int regid = 0, int memberid = 0)
        {
            ErrorMsg result = new ErrorMsg();
            try
            {
                DataBaseContent db = new DataBaseContent();
                FCK_Log nobj = new FCK_Log();
                nobj.Log_Content = content;
                nobj.Log_Time = DateTime.Now;
                nobj.Log_Type = type.ToString();
                nobj.Member_ID = memberid;
                nobj.Register_ID = regid;
                db.FCK_Log.Add(nobj);
                db.SaveChanges();
                result.code = 100;
                result.id = nobj.Log_ID;
                result.message = "SUCCESS";
            }
            catch (Exception err)
            {
                result.code = 102;
                result.message = err.Message;
            }
            return result;
        }

        public LogJson GetLogs(int page, int pageSize, string stime = "", string etime = "", int status = -1, int regid = -1, int memberid = 0)
        {
            LogJson result = new LogJson();
            var lists = dbr.FCK_Log.OrderByDescending(o => o.Log_Time).ToList();
            if (status >= 0)
            {
                lists = lists.Where(o => o.Log_Status == status).ToList();
            }
            if (regid >= 0)
            {
                lists = lists.Where(o => o.Register_ID == regid).ToList();
            }
            if (memberid > 0)
            {
                lists = lists.Where(o => o.Member_ID == memberid).ToList();
            }
            if (!string.IsNullOrEmpty(stime) && !string.IsNullOrEmpty(etime))
            {
                DateTime s = DateTime.Parse(stime);
                DateTime e = DateTime.Parse(etime);
                lists = lists.Where(o => o.Log_Time >= s && o.Log_Time <= e).ToList();
            }
            int total = lists.Count;
            int pages = 0;
            if (pageSize > 0)
            {
                pages = (total + pageSize - 1) / pageSize;
                int startIndex = pageSize * (page - 1);
                lists = lists.Skip(startIndex).Take(pageSize).ToList();
            }
            List<LogData> items = new List<LogData>();
            foreach (var item in lists)
            {
                LogData obj = new LogData();
                obj.Log_Content = item.Log_Content;
                obj.Log_ID = item.Log_ID;
                obj.Log_Time = item.Log_Time.ToString();
                obj.Log_Type = item.Log_Type;
                items.Add(obj);
            }

            result.datas = items;
            result.pages = pages;
            result.total = total;

            return result;
        }

        public LogData GetDetail(int logid)
        {
            LogData result = new LogData();
            var item = dbr.FCK_Log.Where(o => o.Log_ID == logid).FirstOrDefault();
            if (item != null)
            {
                result.Log_Content = item.Log_Content;
                result.Log_ID = item.Log_ID;
                result.Log_Time = item.Log_Time.ToString();
                result.Log_Type = item.Log_Type;
                if (item.Log_Status == 0)
                {
                    item.Log_Status = 1;
                    db.Entry(item).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            return result;
        }
    }
}

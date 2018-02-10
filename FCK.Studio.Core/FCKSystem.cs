using FCK.Common;
using FCK.Studio.Dal;
using FCK.Studio.Entity;
using FCK.Studio.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace FCK.Studio.Core
{
    public class FCKSystem : FCKBase
    {
        public FCKSystem()
        {
            try
            {
                string SMSAccount = GetVal("SMSAccount");
                string SMSPassword = GetVal("SMSPassword");
                if (!string.IsNullOrEmpty(SMSAccount) && !string.IsNullOrEmpty(SMSPassword))
                {
                    SMS.ACCOUNT = SMSAccount;
                    SMS.AUTHKEY = SMSPassword;
                }
                GetSmsRemain();
            }
            catch (Exception err)
            {
                FCKLog.AddLog(err.Message, enumLogType.system);
            }
        }

        public CodeJson GetCodes(int page, int pageSize, string codename = "", string codetype = "")
        {
            CodeJson result = new CodeJson();
            var lists = dbr.FCK_Code.ToList();
            if (!string.IsNullOrEmpty(codename))
            {
                lists = lists.Where(o => o.Code_Name.Contains(codename)).ToList();
            }
            if (!string.IsNullOrEmpty(codetype))
            {
                lists = lists.Where(o => o.Code_Type == codetype).ToList();
            }
            int total = lists.Count;
            int pages = 0;
            if (pageSize > 0)
            {
                pages = (total + pageSize - 1) / pageSize;
                int startIndex = pageSize * (page - 1);
                lists = lists.Skip(startIndex).Take(pageSize).ToList();
            }
            List<CodeData> items = new List<CodeData>();
            foreach (var item in lists)
            {
                CodeData obj = new CodeData();
                obj.Code_ID = item.Code_ID;
                obj.Code_Name = item.Code_Name;
                obj.Code_Type = item.Code_Type;
                obj.Code_Value = item.Code_Value;
                items.Add(obj);
            }

            result.datas = items;
            result.pages = pages;
            result.total = total;

            return result;
        }

        public ErrorMsg EditCode(CodeData model)
        {
            ErrorMsg result = new ErrorMsg();
            try
            {
                var obj = dbr.FCK_Code.Where(o => o.Code_ID == model.Code_ID).FirstOrDefault();
                if (obj != null)
                {
                    obj.Code_Name = model.Code_Name;
                    obj.Code_Type = model.Code_Type;
                    obj.Code_Value = model.Code_Value;
                    db.Entry(obj).State = EntityState.Modified;
                    db.SaveChanges();
                    result.code = 100;
                    result.id = obj.Code_ID;
                    result.message = "SUCCESS";
                }
                else
                {
                    var item = dbr.FCK_Code.Where(o => o.Code_Name == model.Code_Name).FirstOrDefault();
                    if (item == null)
                    {
                        FCK_Code nobj = new FCK_Code();
                        nobj.Code_Name = model.Code_Name;
                        nobj.Code_Type = model.Code_Type;
                        nobj.Code_Value = model.Code_Value;
                        db.FCK_Code.Add(nobj);
                        db.SaveChanges();
                        result.code = 100;
                        result.id = nobj.Code_ID;
                        result.message = "SUCCESS";
                    }
                    else
                    {
                        result.code = 101;
                        result.message = "EXIST";
                    }                    
                }                
            }
            catch (Exception err)
            {
                result.code = 102;
                result.message = err.Message;
            }
            return result;
        }

        public ErrorMsg DelCode(int codeid)
        {
            ErrorMsg result = new ErrorMsg();
            try
            {
                var model = dbr.FCK_Code.Where(o => o.Code_ID == codeid).FirstOrDefault();
                if (model != null)
                {
                    db.Entry(model).State = EntityState.Deleted;
                    db.SaveChanges();
                    result.code = 100;
                    result.message = "OK";
                }
                else
                {
                    result.code = 102;
                    result.message = "NO_EXIST";
                }
            }
            catch (Exception err)
            {
                result.code = 102;
                result.message = err.Message;
            }
            return result;
        }

        public List<string> GetCodeTypes()
        {
            List<string> lists = new List<string>();
            var codes = GetCodes(1, 0);
            if (codes.total > 0)
            {
                foreach (var item in codes.datas)
                {
                    if (!lists.Contains(item.Code_Type))
                    {
                        lists.Add(item.Code_Type);
                    }
                }
            }
            return lists;
        }
        /// <summary>
        /// 获取系统参数
        /// </summary>
        /// <param name="codename"></param>
        /// <returns></returns>
        public static string GetVal(string codename)
        {
            string result = "";
            try
            {
                DataBaseReadContent dbr = new DataBaseReadContent();
                FCK_Code item = dbr.FCK_Code.Where(o => o.Code_Name == codename).FirstOrDefault();
                if (item != null)
                {
                    result = item.Code_Value;
                }
            }
            catch { };
            return result;
        }

        public CodeData GetModel(int ID)
        {
            CodeData model = new CodeData();
            FCK_Code item = dbr.FCK_Code.Where(o => o.Code_ID == ID).FirstOrDefault();
            if (item != null)
            {
                model.Code_ID = item.Code_ID;
                model.Code_Name = item.Code_Name;
                model.Code_Type = item.Code_Type;
                model.Code_Value = item.Code_Value;
            }
            return model;
        }

        public void GetSmsRemain()
        {
            try
            {
                if (DateTime.Now.Hour == 10)
                {
                    FCK.Common.SMS.BalanceResult result = SMS.getBalance();
                    string logcontent = "当前系统短信平台余额：￥" + result.fRemain.ToString("0.00");
                    FCKLog.AddLog(logcontent, enumLogType.system);
                }
            }
            catch { }
            
        }
    }
}

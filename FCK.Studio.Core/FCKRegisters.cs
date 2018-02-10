using FCK.Common;
using FCK.Studio.Entity;
using FCK.Studio.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace FCK.Studio.Core
{
    public class FCKRegisters : FCKBase
    {
        public PageDatas<RegisterDto> GetPageList(int page, int pageSize, string keywords = "")
        {
            PageDatas<RegisterDto> result = new PageDatas<RegisterDto>();
            var lists = dbr.FCK_Registers.ToList();
            if (!string.IsNullOrEmpty(keywords))
            {
                lists = lists.Where(o => o.Register_Name.Contains(keywords)).ToList();
            }
            lists = lists.OrderByDescending(o => o.Register_AddTime).ToList();

            int total = lists.Count;
            int pages = 0;
            if (pageSize > 0)
            {
                pages = (total + pageSize - 1) / pageSize;
                int startIndex = pageSize * (page - 1);
                lists = lists.Skip(startIndex).Take(pageSize).ToList();
            }

            result.datas = Common.Utility.MapTo<List<RegisterDto>>(lists);
            result.pages = pages;
            result.total = total;

            return result;
        }

        public ErrorMsg CreateOrUpdate(RegisterDto model)
        {
            ErrorMsg result = new ErrorMsg();
            try
            {
                var obj = dbr.FCK_Registers.Where(o => o.Register_ID == model.Register_ID).FirstOrDefault();
                if (obj != null)
                {
                    obj = Common.Utility.MapTo<FCK_Registers>(model);
                    db.Entry(obj).State = EntityState.Modified;
                    db.SaveChanges();
                    result.code = 100;
                    result.message = "SUCCESS";
                }
                else
                {
                    var item = dbr.FCK_Registers.Where(o => o.Register_Name == model.Register_Name || o.Register_Mobile == model.Register_Mobile).FirstOrDefault();
                    if (item == null)
                    {
                        model.Register_AddTime = DateTime.Now;
                        model.Register_Number = Utility.CreateRandNum("R");
                        item = Utility.MapTo<FCK_Registers>(model);
                        db.FCK_Registers.Add(item);
                        db.SaveChanges();
                        result.code = 100;
                        result.id = item.Register_ID;
                        result.message = "SUCCESS";
                    }
                    else
                    {
                        result.code = 101;
                        result.message = "OBJECT_IS_EXIST";
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

        public ErrorMsg UpdateStatus(int id, int status)
        {
            ErrorMsg result = new ErrorMsg();
            try
            {
                var item = dbr.FCK_Registers.Where(o => o.Register_ID == id).FirstOrDefault();
                if (item != null)
                {
                    item.Register_Status = status;
                    db.Entry(item).State = EntityState.Modified;
                    db.SaveChanges();
                    result.code = 100;
                    result.message = "UPDATE_SUCCESS";
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

        public ReturnData<RegisterDto> GetModel(int id)
        {
            ReturnData<RegisterDto> result = new ReturnData<RegisterDto>();
            try
            {
                var item = dbr.FCK_Registers.Where(o => o.Register_ID == id).FirstOrDefault();
                if (item != null)
                {
                    result.code = 100;
                    result.message = "SUCCESS";
                    result.datas = Common.Utility.MapTo<RegisterDto>(item);
                }else
                    result.code = 101;
            }
            catch (Exception err)
            {
                result.code = 102;
                result.message = err.Message;
            }
            return result;
        }

        public ReturnData<RegisterDto> GetModelByNumber(string number)
        {
            ReturnData<RegisterDto> result = new ReturnData<RegisterDto>();
            try
            {
                var item = dbr.FCK_Registers.Where(o => o.Register_Number == number).FirstOrDefault();
                if (item != null)
                {
                    Utility.SetSession("RegisterId", item.Register_ID);
                    result.code = 100;
                    result.message = "SUCCESS";
                    result.datas = Utility.MapTo<RegisterDto>(item);
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

        /// <summary>
        /// 修改租户API权限
        /// </summary>
        /// <param name="memberid"></param>
        /// <param name="powers"></param>
        /// <returns></returns>
        public ErrorMsg UpdatePower(int id, string powers)
        {
            ErrorMsg result = new ErrorMsg();
            try
            {
                var item = dbr.FCK_Registers.Where(o => o.Register_ID == id).FirstOrDefault();
                if (item != null)
                {
                    item.Register_ApiPower = powers;
                    db.Entry(item).State = EntityState.Modified;
                    db.SaveChanges();
                    result.code = 100;
                    result.message = "UPDATE_SUCCESS";
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
        /// 修改租户截止日期
        /// </summary>
        /// <param name="id"></param>
        /// <param name="days"></param>
        /// <returns></returns>
        public ErrorMsg UpdateDeadLine(int id, int days)
        {
            ErrorMsg result = new ErrorMsg();
            try
            {
                var item = dbr.FCK_Registers.Where(o => o.Register_ID == id).FirstOrDefault();
                if (item != null)
                {
                    item.Register_DeadLine = item.Register_DeadLine.AddDays(days);
                    db.Entry(item).State = EntityState.Modified;
                    db.SaveChanges();
                    result.code = 100;
                    result.message = "UPDATE_SUCCESS";
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
    }
}

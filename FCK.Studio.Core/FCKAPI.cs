using FCK.Common;
using FCK.Studio.Entity;
using FCK.Studio.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace FCK.Studio.Core
{
    public class FCKAPI : FCKBase
    {
        public PageDatas<FCKAPIDto> GetPageList(int page, int pageSize, string keywords = "")
        {
            PageDatas<FCKAPIDto> result = new PageDatas<FCKAPIDto>();
            var lists = dbr.FCK_API.OrderBy(o => o.API).ToList();

            if (!string.IsNullOrEmpty(keywords))
            {
                lists = lists.Where(o => o.API.Contains(keywords)).ToList();
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
            result.datas = Utility.MapTo<List<FCKAPIDto>>(lists);
            return result;
        }

        public ErrorMsg CreateOrUpdate(FCKAPIDto input)
        {
            ErrorMsg result = new ErrorMsg();
            try
            {
                var model = Utility.MapTo<FCK_API>(input);
                if (input.ID == 0)
                {
                    db.FCK_API.Add(model);
                    db.SaveChanges();
                    result.code = 100;
                    result.message = "SUCCESS";
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

        public ErrorMsg Del(int ID)
        {
            ErrorMsg result = new ErrorMsg();
            try
            {
                var model = dbr.FCK_API.Where(o => o.ID == ID).FirstOrDefault();
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

        public ReturnData<FCKAPIDto> GetModel(int id)
        {
            ReturnData<FCKAPIDto> result = new ReturnData<FCKAPIDto>();
            try
            {
                var model = dbr.FCK_API.Where(o => o.ID == id).FirstOrDefault();
                if (model != null)
                {
                    result.code = 100;
                    result.message = "SUCCESS";
                    result.datas = Utility.MapTo<FCKAPIDto>(model);
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

        public ErrorMsg UpdateStatus(int id, int status)
        {
            ErrorMsg result = new ErrorMsg();
            try
            {
                var item = dbr.FCK_API.Where(o => o.ID == id).FirstOrDefault();
                if (item != null)
                {
                    item.Status = status;
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

        public PageDatas<FCKAPIParaDto> GetParas(int page, int pageSize, string keywords = "", int apid = 0)
        {
            PageDatas<FCKAPIParaDto> result = new PageDatas<FCKAPIParaDto>();
            var lists = dbr.FCK_APIPara.OrderBy(o => o.ID).ToList();

            if (!string.IsNullOrEmpty(keywords))
            {
                lists = lists.Where(o => o.Para_Name.Contains(keywords)).ToList();
            }
            if (apid > 0)
            {
                lists = lists.Where(o => o.API_ID == apid).ToList();
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
            result.datas = Utility.MapTo<List<FCKAPIParaDto>>(lists);
            return result;
        }

        public ErrorMsg EditPara(FCKAPIParaDto input)
        {
            ErrorMsg result = new ErrorMsg();
            try
            {
                var model = Utility.MapTo<FCK_APIPara>(input);
                if (input.ID == 0)
                {
                    db.FCK_APIPara.Add(model);
                    db.SaveChanges();
                    result.code = 100;
                    result.message = "SUCCESS";
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

        public ReturnData<FCKAPIParaDto> GetPara(int ID)
        {
            ReturnData<FCKAPIParaDto> result = new ReturnData<FCKAPIParaDto>();
            try
            {
                var item = dbr.FCK_APIPara.Where(o => o.ID == ID).FirstOrDefault();
                if (item != null)
                {
                    result.code = 100;
                    result.datas = Utility.MapTo<FCKAPIParaDto>(item);
                    result.message = "SUCCESS";
                }
                else {
                    result.code = 101;
                    result.message = "OBJECT_NO_EXIST";
                }
            }
            catch (Exception e)
            {
                result.code = 102;
                result.message = e.Message;
            }
            return result;
        }

        public ErrorMsg DelPara(int ID)
        {
            ErrorMsg result = new ErrorMsg();
            try
            {
                var model = dbr.FCK_APIPara.Where(o => o.ID == ID).FirstOrDefault();
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

        public ErrorMsg UpdateUseTimes(int ID)
        {
            ErrorMsg result = new ErrorMsg();
            try
            {
                var model = dbr.FCK_API.Where(o => o.ID == ID).FirstOrDefault();
                if (model != null)
                {
                    model.UseTimes += 1;
                    db.Entry(model).State = EntityState.Modified;
                    db.SaveChanges();
                    result.code = 100;
                    result.message = "SUCCESS";
                }
                else
                {
                    result.code = 101;
                    result.message = "OBJECT_NO_EXIST";
                }
            }
            catch (Exception e)
            {
                result.code = 102;
                result.message = e.Message;
            }
            return result;
        }
    }
}

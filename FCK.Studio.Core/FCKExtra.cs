using FCK.Common;
using FCK.Studio.Entity;
using FCK.Studio.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace FCK.Studio.Core
{
    public class FCKExtra : FCKBase
    {
        public PageDatas<ExtraDto> GetPageList(int page, int pageSize, string keywords = "", int outid = 0)
        {
            PageDatas<ExtraDto> result = new PageDatas<ExtraDto>();
            var lists = dbr.FCK_Extra.Where(o => o.Register_ID == RegisterID).ToList();
            if (!string.IsNullOrEmpty(keywords))
            {
                lists = lists.Where(o => o.ExtraName.Contains(keywords)).ToList();
            }
            if (outid > 0)
            {
                lists = lists.Where(o => o.ExtraOutID == outid).ToList();
            }
            lists = lists.OrderBy(o => o.ExtraOrder).ToList();

            int total = lists.Count;
            int pages = 0;
            if (pageSize > 0)
            {
                pages = (total + pageSize - 1) / pageSize;
                int startIndex = pageSize * (page - 1);
                lists = lists.Skip(startIndex).Take(pageSize).ToList();
            }

            result.datas = Utility.MapTo<List<ExtraDto>>(lists);
            result.pages = pages;
            result.total = total;

            return result;
        }

        public ErrorMsg CreateOrUpdate(ExtraDto model)
        {
            ErrorMsg result = new ErrorMsg();
            try
            {
                var obj = dbr.FCK_Extra.Where(o => o.ID == model.ID).FirstOrDefault();
                if (obj != null)
                {
                    obj = Common.Utility.MapTo<FCK_Extra>(model);
                    obj.Register_ID = RegisterID;
                    db.Entry(obj).State = EntityState.Modified;
                    db.SaveChanges();
                    result.code = 100;
                    result.message = "SUCCESS";
                }
                else
                {
                    var item = dbr.FCK_Extra.Where(o => o.ExtraName == model.ExtraName).FirstOrDefault();
                    if (item == null)
                    {
                        item = Utility.MapTo<FCK_Extra>(model);
                        item.Register_ID = RegisterID;
                        db.FCK_Extra.Add(item);
                        db.SaveChanges();
                        result.code = 100;
                        result.id = item.ID;
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
            }
            return result;
        }

        public ErrorMsg Del(int ID)
        {
            ErrorMsg result = new ErrorMsg();
            try
            {
                var model = dbr.FCK_Extra.Where(o => o.ID == ID).FirstOrDefault();
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

        public ReturnData<ExtraDto> GetModel(int ID)
        {
            ReturnData<ExtraDto> result = new ReturnData<ExtraDto>();
            try
            {
                var item = dbr.FCK_Extra.Where(o => o.ID == ID).FirstOrDefault();
                if (item != null)
                {
                    result.code = 100;
                    result.message = "SUCCESS";
                    result.datas = Utility.MapTo<ExtraDto>(item);
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

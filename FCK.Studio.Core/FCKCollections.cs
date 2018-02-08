using FCK.Common;
using FCK.Studio.Entity;
using FCK.Studio.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace FCK.Studio.Core
{
    public class FCKCollections : FCKBase
    {

        public CollectionsJson GetByPageList(int page, int pageSize, int memberid = 0, string keywords = "", int isprd = 0)
        {
            CollectionsJson result = new CollectionsJson();
            var lists = dbr.FCK_Collections.Where(o => o.Register_ID == RegisterID).ToList();

            if (memberid > 0)
            {
                lists = lists.Where(o => o.Member_ID == memberid).ToList();
            }

            if (isprd > 0)
            {
                lists = lists.Where(o => o.Product_ID > 0 && o.Article_ID == 0).ToList();
            }

            if (!string.IsNullOrEmpty(keywords))
            {
                lists = lists.Where(o => o.Article_Title.Contains(keywords)).ToList();
            }

            int total = lists.Count;
            int pages = 0;
            if (pageSize > 0)
            {
                pages = (total + pageSize - 1) / pageSize;
                int startIndex = pageSize * (page - 1);
                lists = lists.Skip(startIndex).Take(pageSize).ToList();
            }

            var datas = Utility.MapTo<List<CollectionsDto>>(lists);

            result.total = total;
            result.pages = pages;
            result.datas = datas;
            return result;
        }

        public ErrorMsg Add(CollectionsDto data)
        {
            ErrorMsg result = new ErrorMsg();
            try
            {
                var items = dbr.FCK_Collections.Where(o => o.Member_ID == data.Member_ID && o.Article_ID == data.Article_ID).ToList();
                if (items.Count > 0)
                {
                    result.code = 101;
                    result.message = "EXIST";
                }
                else
                {
                    var model = Utility.MapTo<FCK_Collections>(data);
                    model.Collection_Time = DateTime.Now;
                    model.Register_ID = RegisterID;
                    db.FCK_Collections.Add(model);
                    db.SaveChanges();
                    result.code = 100;

                    FCKArticles article = new FCKArticles();
                    article.AddCollections(data.Article_ID, 1);
                }
            }
            catch (Exception e)
            {
                result.code = 102;
                result.message = e.Message;
            }
            return result;
        }

        public ErrorMsg AddProd(CollectionsDto data)
        {
            ErrorMsg result = new ErrorMsg();
            try
            {
                var items = dbr.FCK_Collections.Where(o => o.Member_ID == data.Member_ID && o.Product_ID == data.Product_ID).ToList();
                if (items.Count > 0)
                {
                    result.code = 101;
                    result.message = "EXIST";
                }
                else
                {
                    var model = Utility.MapTo<FCK_Collections>(data);
                    model.Collection_Time = DateTime.Now;
                    model.Register_ID = RegisterID;
                    db.FCK_Collections.Add(model);
                    db.SaveChanges();
                    result.code = 100;
                }
            }
            catch (Exception e)
            {
                result.code = 102;
                result.message = e.Message;
            }
            return result;
        }

        public ErrorMsg Del(string ids)
        {
            ErrorMsg result = new ErrorMsg();
            try
            {
                FCKArticles article = new FCKArticles();
                string[] idArr = ids.Split(',');
                foreach (string item in idArr)
                {
                    int ID = Utility.cInt(item);
                    var model = dbr.FCK_Collections.Where(o => o.Collection_ID == ID).FirstOrDefault();
                    if (model != null)
                    {
                        db.Entry(model).State = EntityState.Deleted;
                        db.SaveChanges();

                        article.AddCollections(model.Article_ID, -1);
                    }
                }
                result.code = 100;
                result.message = "SUCCESS";
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

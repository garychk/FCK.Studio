using FCK.Common;
using FCK.Studio.Entity;
using FCK.Studio.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace FCK.Studio.Core
{
    public class FCKArticles : FCKBase
    {

        public PageDatas<ArticleDto> GetPageList(int page, int pageSize, int cateid = 0, string catename = "", string keywords = "", string tag = "", string orderindex = "", int isrec = 0, int memberid = 0)
        {
            PageDatas<ArticleDto> result = new PageDatas<ArticleDto>();
            FCKCategory fckcate = new FCKCategory();
            var lists = dbr.FCK_Articles.Where(o => o.Register_ID == RegisterID).ToList();
            if (!string.IsNullOrEmpty(keywords))
            {
                lists = lists.Where(o => o.Article_Title != null).ToList();
                lists = lists.Where(o => o.Article_Title.Contains(keywords)).ToList();
            }
            if (!string.IsNullOrEmpty(tag))
            {
                lists = lists.Where(o => !string.IsNullOrEmpty(o.Article_Tag)).ToList();
                lists = lists.Where(o => o.Article_Tag.ToLower().Contains(tag.ToLower())).ToList();
            }
            if (isrec > 0) 
            {
                lists = lists.Where(o => o.Article_IsRec == isrec).ToList();
            }
            if (cateid > 0)
            {
                List<int> cates = new List<int>();
                cates.Add(cateid);
                var cateitems = fckcate.GetList(cateid);
                foreach (var item in cateitems)
                {
                    cates.Add(item.Category_ID);
                }
                lists = lists.Where(o => cates.Contains(o.Article_CateID)).ToList();
            }
            if (!string.IsNullOrEmpty(catename))
            {
                cateid = fckcate.GetCateId(catename);
                List<int> cates = new List<int>();
                cates.Add(cateid);
                var cateitems = fckcate.GetList(cateid);
                foreach (var item in cateitems)
                {
                    cates.Add(item.Category_ID);
                }
                lists = lists.Where(o => cates.Contains(o.Article_CateID)).ToList();
            }

            if (memberid > 0)
            {
                lists = lists.Where(o => o.Article_MemberID == memberid).ToList();
            }

            if (orderindex == "hits")
                lists = lists.OrderBy(o => o.Article_Hits).ToList();
            else if (orderindex == "hitsdesc")
                lists = lists.OrderByDescending(o => o.Article_Hits).ToList();
            else if (orderindex == "orderid")
                lists = lists.OrderBy(o => o.Article_OrderID).ToList();
            else if (orderindex == "orderidesc")
                lists = lists.OrderByDescending(o => o.Article_OrderID).ToList();
            else if (orderindex == "time")
                lists = lists.OrderBy(o => o.Article_UpdateTime).ToList();
            else
                lists = lists.OrderByDescending(o => o.Article_UpdateTime).ToList();

            int total = lists.Count;
            int pages = 0;
            if (pageSize > 0)
            {
                pages = (total + pageSize - 1) / pageSize;
                int startIndex = pageSize * (page - 1);
                lists = lists.Skip(startIndex).Take(pageSize).ToList();
            }
            
            List<ArticleDto> items = new List<ArticleDto>();
            foreach (var item in lists)
            {
                ArticleDto obj = Utility.MapTo<ArticleDto>(item);
                item.Article_Pic = string.IsNullOrEmpty(item.Article_Pic) ? "/Content/images/blank.png" : item.Article_Pic;
                obj.Article_PicSmall = Utility.GetSmallPic(item.Article_Pic);
                obj.Article_CateName = fckcate.GetCateName(item.Article_CateID);
                items.Add(obj);
            }

            result.datas = items;
            result.pages = pages;
            result.total = total;

            return result;
        }

        public ErrorMsg CreateOrUpdate(ArticleDto model)
        {
            ErrorMsg result = new ErrorMsg();
            try
            {
                var obj = dbr.FCK_Articles.Where(o => o.Article_ID == model.Article_ID).FirstOrDefault();
                if (obj != null)
                {
                    obj = Utility.MapTo<FCK_Articles>(model);
                    obj.Register_ID = RegisterID;
                    db.Entry(obj).State = EntityState.Modified;
                    db.SaveChanges();
                    result.code = 100;
                    result.id = obj.Article_ID;
                    result.message = "SUCCESS";
                }
                else
                {
                    FCK_Articles nobj = new FCK_Articles();
                    var item = dbr.FCK_Articles.Where(o => o.Article_Title == model.Article_Title).FirstOrDefault();
                    if (item == null)
                    {
                        nobj = Utility.MapTo<FCK_Articles>(model);
                        nobj.Register_ID = RegisterID;
                        db.FCK_Articles.Add(nobj);
                        db.SaveChanges();
                        result.code = 100;
                        result.id = nobj.Article_ID;
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

        public ArticleDto GetModel(int ID)
        {
            ArticleDto model = new ArticleDto();
            FCK_Articles item = dbr.FCK_Articles.Where(o => o.Article_ID == ID).FirstOrDefault();
            if (item != null)
            {
                FCKCategory fckcate = new FCKCategory();
                model = Utility.MapTo<ArticleDto>(item);
                model.Article_CateName = fckcate.GetCateName(item.Article_CateID);
            }
            return model;
        }

        public ErrorMsg Del(string ids)
        {
            ErrorMsg result = new ErrorMsg();
            try
            {
                string[] idArr = ids.Split(',');
                foreach (string item in idArr)
                {
                    int ID = Utility.cInt(item);
                    var model = dbr.FCK_Articles.Where(o => o.Article_ID == ID).FirstOrDefault();
                    if (model != null)
                    {
                        db.Entry(model).State = EntityState.Deleted;
                        db.SaveChanges();                        
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

        public ErrorMsg RecArticle(string ids)
        {
            ErrorMsg result = new ErrorMsg();
            try
            {
                string[] idArr = ids.Split(',');
                foreach (string id in idArr)
                {
                    int ID = Utility.cInt(id);
                    var model = dbr.FCK_Articles.Where(o => o.Article_ID == ID).FirstOrDefault();
                    if (model != null)
                    {
                        model.Article_IsRec = model.Article_IsRec == 0 ? 1 : 0;
                        db.Entry(model).State = EntityState.Modified;
                        db.SaveChanges();
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

        public List<string> GetTags(int cateid = 0, int top = 0)
        {
            List<string> result = new List<string>();
            var lists = GetPageList(1, 0, cateid, "", "", "", "hitsdesc");
            var items = lists.datas;
            if (top > 0 && top < items.Count)
                items = items.Take(top).ToList();
            var tags = items.Where(o => !string.IsNullOrEmpty(o.Article_Tag)).Select(o => o.Article_Tag).ToList();
            foreach (var o in tags)
            {
                var str = o.Replace("，", ",").Replace("|", ",").Replace(";", ",").Replace("；", ",");
                string[] arr = str.Split(',');
                foreach (var i in arr)
                {
                    if (!result.Contains(i.ToLower()))
                    {
                        result.Add(i.ToLower());
                    }
                }
            }
            result = result.Distinct().ToList();
            return result;
        }

        public ErrorMsg AddHits(int ID)
        {
            ErrorMsg result = new ErrorMsg();
            try
            {
                var obj = dbr.FCK_Articles.Where(o=>o.Article_ID==ID).FirstOrDefault();
                obj.Article_Hits += 1;
                db.Entry(obj).State = EntityState.Modified;
                db.SaveChanges();
                result.code = 100;
                result.id = obj.Article_ID;
                result.message = "SUCCESS";
            }
            catch (Exception e)
            {
                result.code = 102;
                result.message = e.Message;
            }
            return result;
        }

        public ErrorMsg AddComments(int ID, int val)
        {
            ErrorMsg result = new ErrorMsg();
            try
            {
                var obj = dbr.FCK_Articles.Where(o => o.Article_ID == ID).FirstOrDefault();
                obj.Article_Comments += val;
                db.Entry(obj).State = EntityState.Modified;
                db.SaveChanges();
                result.code = 100;
                result.id = obj.Article_ID;
                result.message = "SUCCESS";
            }
            catch (Exception e)
            {
                result.code = 102;
                result.message = e.Message;
            }
            return result;
        }

        public ErrorMsg AddCollections(int ID, int val)
        {
            ErrorMsg result = new ErrorMsg();
            try
            {
                var obj = dbr.FCK_Articles.Where(o => o.Article_ID == ID).FirstOrDefault();
                obj.Article_Collections += val;
                db.Entry(obj).State = EntityState.Modified;
                db.SaveChanges();
                result.code = 100;
                result.id = obj.Article_ID;
                result.message = "SUCCESS";
            }
            catch (Exception e)
            {
                result.code = 102;
                result.message = e.Message;
            }
            return result;
        }


        public List<List<object>> GetArticleTj()
        {

            List<List<object>> result = new List<List<object>>();
            FCKCategory fckcate = new FCKCategory();
            var cates = fckcate.GetList(0);
            foreach (var cate in cates)
            {
                List<object> obj = new List<object>();
                var arts = GetPageList(1, 0, cate.Category_ID);
                int num = 0;
                foreach (var item in arts.datas)
                {
                    num += item.Article_Hits;
                }
                if (num > 10)
                {
                    obj.Add(cate.Category_Name);
                    obj.Add(num);
                    result.Add(obj);
                }
            }

            return result;
        }
    }
}

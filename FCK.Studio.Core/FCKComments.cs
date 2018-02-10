using FCK.Common;
using FCK.Studio.Entity;
using FCK.Studio.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace FCK.Studio.Core
{
    public class FCKComments : FCKBase
    {

        public CommentsJson GetByPageList(int page, int pageSize, bool? isdel, bool? isactive, int memberid = 0, string keywords = "", int articleid = 0)
        {
            CommentsJson result = new CommentsJson();
            var lists = dbr.FCK_Comments.Where(o => o.Register_ID == RegisterID).ToList();

            if (isdel.Value)
            {
                lists = lists.Where(o => o.IsDel == isdel).ToList();
            }

            if (memberid > 0)
            {
                lists = lists.Where(o => o.Member_ID == memberid).ToList();
            }

            if (articleid > 0)
            {
                lists = lists.Where(o => o.Article_ID == articleid).ToList();
            }

            if (!string.IsNullOrEmpty(keywords))
            {
                lists = lists.Where(o => o.Member_NickName.Contains(keywords)).ToList();
            }
            
            int total = lists.Count;
            int pages = 0;
            if (pageSize > 0)
            {
                pages = (total + pageSize - 1) / pageSize;
                int startIndex = pageSize * (page - 1);
                lists = lists.Skip(startIndex).Take(pageSize).ToList();
            }
            
            var datas = Utility.MapTo<List<CommentsData>>(lists);

            result.total = total;
            result.pages = pages;
            result.datas = datas;
            return result;
        }

        public ErrorMsg Add(CommentsData data)
        {
            ErrorMsg result = new ErrorMsg();
            try
            {
                var model = Utility.MapTo<FCK_Comments>(data);
                model.Comment_Times = DateTime.Now;
                model.Register_ID = RegisterID;
                db.FCK_Comments.Add(model);
                db.SaveChanges();
                result.code = 100;

                FCKArticles article = new FCKArticles();
                article.AddComments(data.Article_ID, 1);
            }
            catch (Exception e) {
                result.code = 102;
                result.message = e.Message;
            }
            return result;
        }

        public ErrorMsg DelComment(string ids)
        {
            ErrorMsg result = new ErrorMsg();
            try
            {
                FCKArticles article = new FCKArticles();
                string[] idArr = ids.Split(',');
                foreach (string item in idArr)
                {
                    int ID = Utility.cInt(item);
                    var model = dbr.FCK_Comments.Where(o => o.Comment_ID == ID).FirstOrDefault();
                    if (model != null)
                    {
                        db.Entry(model).State = EntityState.Deleted;
                        db.SaveChanges();

                        article.AddComments(model.Article_ID, 1);
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

        /// <summary>
        /// 点赞or踩
        /// </summary>
        /// <param name="id"></param>
        /// <param name="step"></param>
        /// <returns></returns>
        public ErrorMsg Ok(int id, int step = 1)
        {
            ErrorMsg result = new ErrorMsg();
            try
            {
                var model = dbr.FCK_Comments.Where(o => o.Comment_ID == id).FirstOrDefault();
                if (step > 0)
                    model.Comment_Good += 1;
                else
                    model.Comment_Bad += 1;

                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                result.code = 100;
                result.message = "SUCCESS";
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

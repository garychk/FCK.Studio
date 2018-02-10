using FCK.Studio.Core;
using FCK.Studio.Dto;
using FCK.Studio.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FCK.Studio.Web.Controllers
{
    [FilterCheckLogin]
    public class MembersController : BaseController
    {
        FCKMembers members = new FCKMembers();
        FCKComments comments = new FCKComments();
        FCKArticles articles = new FCKArticles();
        FCKCategory category = new FCKCategory();
        FCKCollections collections = new FCKCollections();
        FCKOrders orders = new FCKOrders();

        public ActionResult Index()
        {
            return View(member);
        }

        public ActionResult MemberInfo()
        {
            return View(member);
        }

        public ActionResult MemberEdit()
        {
            return View(member);
        }

        public ActionResult ResetPsw()
        {
            return View(member);
        }

        public JsonResult ResetPassword(string newPassword)
        {
            var result = members.ResetPassword(member.Member_ID, newPassword);
            return Json(result);
        }

        public ActionResult Comments()
        {
            return View(member);
        }
        
        public ActionResult Articles()
        {
            ViewData["CateId"] = category.GetCateId("博文");
            return View(member);
        }

        public ActionResult Resources()
        {
            ViewData["CateId"] = category.GetCateId("资源");
            return View(member);
        }

        public ActionResult ResourceEdit(int para = 0)
        {
            var model = articles.GetModel(para);
            if (model.Article_ID == 0)
            {
                model.Article_UpdateTime = DateTime.Now;
            }
            model.Article_MemberID = member.Member_ID;
            return View(model);
        }

        public ActionResult Collection()
        {
            return View(member);
        }

        public ActionResult Orders()
        {
            return View(member);
        }
        
        public JsonResult GetComments(int page = 1, int pageSize = 10, string keywords = "")
        {

            var result = comments.GetByPageList(page, pageSize, false, true, member.Member_ID, keywords);
            return Json(result);
        }

        public ActionResult ArticleEdit(int para = 0)
        {
            ArticleDto model = new ArticleDto();
            model = articles.GetModel(para);
            model.Article_MemberID = member.Member_ID;
            model.Article_UpdateTime = DateTime.Now;

            return View(model);
        }

        public JsonResult EditArticle(ArticleDto model)
        {
            model.Article_Contents = HttpUtility.UrlDecode(model.Article_Contents);
            model.Article_Author = member.Member_NickName;
            ErrorMsg result = articles.CreateOrUpdate(model);
            return Json(result);
        }

        public JsonResult DelArticle(string ids)
        {
            ErrorMsg result = articles.Del(ids);
            return Json(result);
        }

        public JsonResult DelComments(string ids)
        {
            ErrorMsg result = comments.DelComment(ids);
            return Json(result);
        }

        public JsonResult GetOrders(int page = 1, int pageSize = 10, string keywords = "", string stime = "", string etime = "", int status = -1, string OrderIndex = "")
        {
            OrdersJson result = orders.GetPageList(page, pageSize, keywords, member.Member_ID, stime, etime, status, OrderIndex);
            return Json(result);
        }

        public JsonResult GetCollections(int page = 1, int pageSize = 10, string keywords = "", int articleid = 0)
        {
            CollectionsJson result = collections.GetByPageList(page, pageSize, member.Member_ID, keywords, articleid);
            return Json(result);
        }

        public JsonResult DelCollection(string ids)
        {
            ErrorMsg result = collections.Del(ids);
            return Json(result);
        }

        public JsonResult AddCollect(CollectionsDto model)
        {
            ErrorMsg result = collections.Add(model);
            return Json(result);
        }

        public JsonResult EditMember(MemberDto model)
        {
            return Json(members.CreateOrUpdate(model));
        }

        public JsonResult CheckExist(string val)
        {
            bool result = false;
            var item = members.GetPageList(1, 0);
            var list = item.datas.Where(o => o.Member_ID != member.Member_ID).ToList();
            list = list.Where(o => o.Member_NickName == val || o.Member_Mobile == val || o.Member_QQ == val || o.Member_Email == val).ToList();
            result = list.Count == 0;
            return Json(result);
        }

        public JsonResult UpdateUserHeader(int id)
        {
            string headerurl = "/Content/header/" + new Random().Next(0, 48).ToString() + ".png";
            var result = members.UpdateHeader(id, headerurl);
            return Json(result);
        }

    }
}

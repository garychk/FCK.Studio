using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FCK.Studio.Core;
using FCK.Studio.Web.Models;
using FCK.Studio.Dto;

namespace FCK.Studio.Web.Controllers
{
    public class ArticleController : BaseController
    {
        FCKArticles core = new FCKArticles();
        FCKCategory category = new FCKCategory();
        FCKComments comments = new FCKComments();
        FCKMembers members = new FCKMembers();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Detail(int para = 0)
        {
            ArticleDto model = core.GetModel(para);
            core.AddHits(para);
            ViewData["UserHeader"] = member.Member_Header;
            return View(model);
        }

        public ActionResult PartialArticles(ParaArticle para)
        {
            PageDatas<ArticleDto> model = core.GetPageList(para.page, para.pageSize, para.cateid, para.catename, para.keywords, para.tag, para.orderindex, para.isrec, para.memberid);
            if (para.ispage == 0)
            {
                model.pages = 1;
            }
            return PartialView(model);
        }

        public JsonResult GetArticles(ParaArticle para)
        {
            PageDatas<ArticleDto> model = core.GetPageList(para.page, para.pageSize, para.cateid, para.catename, para.keywords, para.tag, para.orderindex, para.isrec, para.memberid);
            return Json(model);
        }

        public ActionResult PartialHotArticles(int top = 10, int cateid = 0, string url = "")
        {
            ViewData["listurl"] = url;
            PageDatas<ArticleDto> model = core.GetPageList(1, top, cateid, "", "", "", "hitsdesc", 0);
            return View(model);
        }

        public ActionResult PartialCategory(string catename, string ctype = "", string url = "")
        {
            ViewData["listurl"] = url;
            CategoryList model = new CategoryList();
            int cateid = category.GetCateId(catename);
            List<CategoryDto> lists = category.GetList(cateid, ctype);
            model.datas = lists;
            return View(model);
        }

        public ActionResult PartialTags(string catename, int top = 0, string url = "")
        {
            ViewData["listurl"] = url;
            int cateid = category.GetCateId(catename);
            List<string> tags = core.GetTags(cateid, top);
            StringList lists = new StringList();
            lists.datas = tags;
            return View(lists);
        }

        public ActionResult _PartialComments(int articleid = 0, int page = 1, int pagesize = 0, int memberid = 0, string style = "detail")
        {
            var result = comments.GetByPageList(page, pagesize, false, false, memberid, "", articleid);
            var items = result.datas.OrderByDescending(o => o.Comment_Times).ToList();
            result.datas = items;
            ViewData["style"] = style;
            return View(result);
        }

        public JsonResult AddComment(CommentsData data)
        {
            var result = comments.Add(data);
            if (result.code == 100)
            {
                int points = Common.Utility.cInt(FCKSystem.GetVal("CommentPoints"));
                FCKMembers members = new FCKMembers();
                members.UpdatePoints(member.Member_ID, points);
            }
            return Json(result);
        }        

        public JsonResult GetCateTree(int parentid = 0, string ctype = "")
        {
            List<TreeNode> result = category.GetCateTree(parentid, ctype);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public ActionResult _PartialMemberInfo(int memberid)
        {
            var member = members.GetModel(memberid);
            return View(member);
        }

        public JsonResult Ok(int id, int step = 1)
        {
            return Json(comments.Ok(id, step));
        }

        public ActionResult Advertise(string advtag, int width, int height, string target = "_blank", int isdiy = 0)
        {
            PageDatas<ArticleDto> model = core.GetPageList(1, 100, 0, "广告位");
            AdvDto adv = new AdvDto();
            adv.width = width;
            adv.height = height;
            adv.advtag = advtag;
            if (model.total > 0)
            {
                var item = model.datas.Where(o => o.Article_Tag == advtag).FirstOrDefault();
                if (item != null)
                {
                    if (isdiy > 0)
                    {
                        adv.advcon = item.Article_Contents;
                    }
                    else
                    {
                        adv.advname = item.Article_Title;
                        adv.advpic = item.Article_Brief;
                        adv.advurl = item.Article_NavUrl;
                        adv.target = target;
                    }
                }
            }
            return PartialView(adv);
        }
    }
}

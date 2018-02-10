using FCK.Studio.Admin.Filters;
using FCK.Studio.Core;
using FCK.Studio.Dto;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace FCK.Studio.Admin.Controllers
{
    [CheckLogin]
    public class ArticlesController : Controller
    {
        FCKArticles core = new FCKArticles();

        public ActionResult Index(int id = 0)
        {
            ViewData["CateId"] = id;
            return View();
        }

        public ActionResult Edit(int id = 0)
        {
            ArticleDto model = core.GetModel(id);
            if (id == 0)
            {
                model.Article_UpdateTime = DateTime.Now;
            }
            return View(model);
        }

        public JsonResult GetArticles(int page, int pageSize, int cateid = 0, string catename = "", string keywords = "", string tag = "", string orderindex = "", int isrec = 0)
        {
            PageDatas<ArticleDto> result = core.GetPageList(page, pageSize, cateid, catename, keywords, tag, orderindex, isrec);
            return Json(result);
        }

        public JsonResult EditArticle(ArticleDto model)
        {
            model.Article_Contents = HttpUtility.UrlDecode(model.Article_Contents);
            ErrorMsg result = core.CreateOrUpdate(model);
            return Json(result);
        }

        public JsonResult DelArticle(string ids)
        {
            ErrorMsg result = core.Del(ids);
            return Json(result);
        }

        public JsonResult RecArticle(string ids)
        {
            ErrorMsg result = core.RecArticle(ids);
            return Json(result);
        }

        public JsonResult GetTags(int cateid = 0, int top = 0)
        {
            List<string> result = core.GetTags(cateid, top);
            return Json(result);
        }

        public JsonResult GetArticleTj()
        {
            var result = core.GetArticleTj();
            return Json(result);
        }
    }
}

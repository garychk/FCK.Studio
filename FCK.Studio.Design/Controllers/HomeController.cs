using FCK.Studio.Core;
using FCK.Studio.Design.Models;
using FCK.Studio.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FCK.Studio.Design.Controllers
{
    public class HomeController : Controller
    {
        FCKArticles core = new FCKArticles();

        public ActionResult Index()
        {
            ViewData["page"] = "home";
            PageDatas<ArticleDto> item_focus = core.GetPageList(1, 0, 0, "首页焦点", "", "", "orderid");
            PageDatas<ArticleDto> item_service = core.GetPageList(1, 0, 0, "产品服务", "", "", "orderid");
            PageDatas<ArticleDto> item_partner = core.GetPageList(1, 0, 0, "合作伙伴", "", "", "orderid");

            HomeModel model = new HomeModel();
            model.Item_Banner = item_focus.datas;
            model.Item_Partner = item_partner.datas;
            model.Item_Service = item_service.datas;
            return View(model);
        }

        public ActionResult About()
        {
            ViewData["page"] = "about";
            PageDatas<ArticleDto> model = core.GetPageList(1, 10, 0, "常见问题", "", "", "orderid");
            return View(model);
        }

        public ActionResult Contact()
        {
            ViewData["page"] = "contact";
            return View();
        }

        public ActionResult Works()
        {
            ViewData["page"] = "works";
            return View();
        }

        public ActionResult _PartialArticles(int page, int pageSize, string catename, string orderindex, int isrec, int ispage)
        {
            PageDatas<ArticleDto> model = core.GetPageList(page, pageSize, 0, catename, "", "", orderindex, isrec);
            if (ispage == 0)
            {
                model.pages = 1;
            }
            ViewData["catename"] = catename;
            return PartialView(model);
        }

        public ActionResult _PartialSide(int page, int pageSize, string catename, string orderindex, int isrec, int ispage)
        {
            PageDatas<ArticleDto> model = core.GetPageList(page, pageSize, 0, catename, "", "", orderindex, isrec);
            if (ispage == 0)
            {
                model.pages = 1;
            }            
            return PartialView(model);
        }

        public ActionResult Detail(int id)
        {
            ViewData["page"] = "about";
            ArticleDto model = core.GetModel(id);
            core.AddHits(id);
            return View(model);
        }

        public ActionResult WMore(int id)
        {
            ViewData["page"] = "works";
            ArticleDto model = core.GetModel(id);
            core.AddHits(id);
            return View(model);
        }


    }
}

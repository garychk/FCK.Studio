using FCK.Studio.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FCK.Studio.Web.Controllers
{
    public class ResourcesController : BaseController
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
            var model = core.GetModel(para);
            core.AddHits(para);
            ViewData["UserHeader"] = member.Member_Header;
            ViewData["UserPoint"] = Common.Utility.cInt(member.Member_Points);
            return View(model);
        }

        public ActionResult _PartialResources(int page = 1, int pageSize = 10, int ispage = 1, int cateid = 0, string catename = "", string keywords = "", string tag = "",int memberid = 0)
        {
            if (cateid == 0)
                cateid = category.GetCateId("资源");
            var result = core.GetPageList(page, pageSize, cateid, catename, keywords, tag, "", 0, memberid);
            if (ispage == 0)
            {
                result.pages = 1;
            }
            return View(result);
        }

        public ActionResult _PartialSearch()
        {
            int cateid = category.GetCateId("资源");
            var result = category.GetList(cateid);
            return View(result);
        }

        public ActionResult Search(int page = 1, int pageSize = 20, int cateid = 0, string keywords = "")
        {
            var result = core.GetPageList(page, pageSize, cateid, "", keywords);
            return PartialView("_PartialResources", result);
        }

        public FileResult GetResource(int id)
        {            
            var result = core.GetModel(id);
            if (member.Member_Points >= result.Article_Point)
            {
                var path = Server.MapPath(result.Article_NavUrl);
                var name = System.IO.Path.GetFileName(path);

                //扣下载者积分
                members.UpdatePoints(member.Member_ID, -result.Article_Point);
                //增加资源提供者积分
                members.UpdatePoints(result.Article_MemberID, result.Article_Point);

                return File(path, "application/x-zip-compressed", name);
            }
            else
                return null;
        }

    }
}

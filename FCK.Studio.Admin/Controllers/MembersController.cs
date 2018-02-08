using FCK.Studio.Admin.Filters;
using FCK.Studio.Core;
using FCK.Studio.Dto;
using System.Web.Mvc;

namespace FCK.Studio.Admin.Controllers
{
    [CheckLogin]
    public class MembersController : Controller
    {
        FCKMembers core = new FCKMembers();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Add()
        {
            return View();
        }
        public ActionResult SimpleAdd()
        {
            return View();
        }
        public ActionResult Edit(int id=0)
        {
            MemberDto model = core.GetModel(id);
            return View(model);
        }
        public ActionResult Groups()
        {
            return View();
        }
        public ActionResult Recharge(int id = 0)
        {
            ViewData["MemberID"] = id;
            return View();
        }

        public JsonResult GetMembers(int page, int pageSize, string keywords = "")
        {
            PageDatas<MemberData> result = core.GetPageList(page, pageSize, keywords);
            return Json(result);
        }

        public JsonResult GetModel(int ID)
        {
            MemberDto result = core.GetModel(ID);
            return Json(result);
        }

        public JsonResult EditMember(MemberDto model)
        {
            ErrorMsg result = core.CreateOrUpdate(model);
            return Json(result);
        }

        public JsonResult UpdateStatus(int memberid, int status)
        {
            ErrorMsg result = core.UpdateStatus(memberid, status);
            return Json(result);
        }

        public JsonResult DoRecharge(int memberid, int money)
        {
            ErrorMsg result = core.UpdateMoney(memberid, money);
            if (result.code == 100) {
                core.RechargeAfter(memberid, money);
            }
            return Json(result);
        }

        public JsonResult DelMember(int memberid)
        {
            ErrorMsg result = core.Del(memberid);
            return Json(result);
        }

        public JsonResult ResetPassword(int memberid, string newPassword)
        {
            ErrorMsg result = core.ResetPassword(memberid, newPassword);
            return Json(result);
        }

        public JsonResult GetGroups(int page, int pageSize, string groupname = "")
        {
            GroupJson result = core.GetGroups(page, pageSize, groupname);
            return Json(result);
        }

        public JsonResult EditGroup(GroupData model)
        {
            ErrorMsg result = core.EditGroup(model);
            return Json(result);
        }

        public JsonResult DelGroup(int groupid)
        {
            ErrorMsg result = core.DelGroup(groupid);
            return Json(result);
        }

        public JsonResult GetModelGroup(int groupid)
        {
            GroupData result = core.GetModelGroup(groupid);
            return Json(result);
        }

        public JsonResult UpdatePower(int memberid, string powers)
        {
            ErrorMsg result = core.UpdatePower(memberid, powers);
            return Json(result);
        }
    }
}

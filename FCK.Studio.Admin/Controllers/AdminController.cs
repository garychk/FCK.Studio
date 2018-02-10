using FCK.Studio.Admin.Filters;
using FCK.Studio.Core;
using FCK.Studio.Dto;
using System.Web.Mvc;

namespace FCK.Studio.Admin.Controllers
{
    public class AdminController : Controller
    {
        private FCKAdmin admin = new FCKAdmin();

        [CheckLogin]
        public ActionResult Index()
        {            
            return View();
        }

        [CheckLogin]
        public ActionResult Edit(int id = 0)
        {
            EditAdminM result = new EditAdminM();
            try
            {
                result = admin.GetAdmin(id);
            }
            catch { }
            return View(result);
        }

        [CheckLogin]
        public ActionResult ResetPassword(int id = 0)
        {
            EditAdminM result = new EditAdminM();
            try
            {
                result = admin.GetAdmin(id);
            }
            catch { }
            return View(result);
        }

        [CheckLogin]
        public ActionResult Add()
        {
            return View();
        }

        public JsonResult DoLogin(string username, string password, string IP = "")
        {
            LoginModel model = new LoginModel();
            model.UserName = username;
            model.Password = password;
            model.IP = IP;
            ErrorMsg obj = admin.Login(model);
            return Json(obj);
        }

        public JsonResult InitAdmin(string username, string password)
        {
            EditAdminM model = new EditAdminM();
            model.Admin_Name = username;
            model.Admin_Password = password;
            model.Admin_TrueName = "管理员";
            ErrorMsg obj = admin.EditAdmin(model);
            return Json(obj);
        }

        public JsonResult DoLogout()
        {
            ErrorMsg obj = admin.Logout();
            return Json(obj);
        }
        [CheckLogin]
        public JsonResult GetAdmins(int page, int pageSize, string adminname = "")
        {
            AdminsJson result = admin.GetAdmins(page, pageSize, adminname);
            return Json(result);
        }

        [CheckLogin]
        public JsonResult EditAdmin(EditAdminM model)
        {
            ErrorMsg result = admin.EditAdmin(model);
            return Json(result);
        }

        [CheckLogin]
        public JsonResult DelAdmin(int adminid)
        {
            ErrorMsg result = admin.DelAdmin(adminid);
            return Json(result);
        }

        [CheckLogin]
        public JsonResult UpdateAdminStatus(int adminid, int status)
        {
            ErrorMsg result = admin.UpdateAdminStatus(adminid, status);
            return Json(result);
        }

        [CheckLogin]
        public JsonResult UpdateAdminPsw(int adminid, string newpassword)
        {
            ErrorMsg result = admin.UpdateAdminPsw(adminid, newpassword);
            return Json(result);
        }

    }
}

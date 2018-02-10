using FCK.Studio.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FCK.Studio.Web.Controllers
{
    public class UsersController : BaseController
    {
        FCKMembers members = new FCKMembers();

        public ActionResult Index(int para = 0)
        {
            var model = members.GetModel(para);
            return View(model);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FCK.Studio.Merchant.Filters;
using FCK.Studio.Dto;
using FCK.Studio.Core;

namespace FCK.Studio.Merchant.Controllers
{
    [FilterCheckLogin]    
    public class RegisterController : Controller
    {
        FCKRegisters core = new FCKRegisters();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Message()
        {
            return View();
        }

        public ActionResult Products()
        {            
            return View();
        }

        public ActionResult Orders()
        {
            return View();
        }

        public ActionResult Finance()
        {
            return View();
        }

        public ActionResult Setting(int id = 0)
        {
            var result = core.GetModel(id);
            RegisterDto model = new RegisterDto();
            if (result.code == 100)
                model = result.datas;
            return View(model);
        }
    }
}
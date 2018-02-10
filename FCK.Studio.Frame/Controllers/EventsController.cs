using FCK.Studio.Frame.Core;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FCK.Studio.Frame.Controllers
{
    public class EventsController : BaseController
    {
        public JsonResult GetPageList()
        {
            return Get<PageDatas<EventDto>>("Events/GetPageList");
        }

        public JsonResult CreateOrUpdate()
        {
            return Post<ErrorMsg>("Events/CreateOrUpdate");
        }

    }
}

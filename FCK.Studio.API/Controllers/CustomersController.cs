using FCK.Studio.API.Filter;
using FCK.Studio.Core;
using FCK.Studio.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FCK.Studio.API.Controllers
{
    [BasicAuthentication]
    public class CustomersController : ApiController
    {
        FCKCustomers core = new FCKCustomers();

        [HttpGet]
        public PageDatas<CustomerDto> GetPageList(int page, int pageSize, string keywords = "", int memberid = 0)
        {
            return core.GetPageList(page, pageSize, keywords, memberid);
        }

        [HttpPost]
        public ErrorMsg CreateOrUpdate(CustomerDto model)
        {
            return core.CreateOrUpdate(model);
        }

        [HttpGet]
        public ReturnData<CustomerDto> GetModel(int ID)
        {
            return core.GetModel(ID);
        }

        [HttpGet]
        public ErrorMsg Del(int ID)
        {
            return core.Del(ID);
        }
    }
}

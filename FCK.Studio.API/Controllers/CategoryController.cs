using FCK.Studio.API.Filter;
using FCK.Studio.Core;
using FCK.Studio.Dto;
using System.Collections.Generic;
using System.Web.Http;

namespace FCK.Studio.API.Controllers
{
    [BasicAuthentication]
    public class CategoryController : ApiController
    {
        FCKCategory core = new FCKCategory();

        [HttpGet]
        public List<CategoryDto> GetList(int parentid, string ctype = "")
        {
            return core.GetList(parentid, ctype);
        }

        [HttpPost]
        public ErrorMsg CreateOrUpdate(CategoryDto model)
        {
            return core.CreateOrUpdate(model);
        }

        public ErrorMsg Del(int id)
        {
            return core.Del(id);
        }
    }
}

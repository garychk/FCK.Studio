using FCK.Studio.API.Filter;
using FCK.Studio.Core;
using FCK.Studio.Dto;
using System.Web.Http;

namespace FCK.Studio.API.Controllers
{
    [BasicAuthentication]
    public class ProductController : ApiController
    {
        FCKProducts core = new FCKProducts();
        FCKExtra extra = new FCKExtra();

        [HttpGet]
        public PageDatas<ProductDto> GetPageList(int page, int pageSize, int cateid = 0, string keywords = "", string pricerange = "", string orderindex = "pricedesc", int isrec = 0)
        {
            return core.GetPageList(page, pageSize, cateid, keywords, pricerange, orderindex, isrec);
        }

        [HttpPost]
        public ErrorMsg CreateOrUpdate(ProductDto model)
        {
            return core.CreateOrUpdate(model);
        }

        [HttpGet]
        public ProductDto GetModel(int ID)
        {
            return core.GetModel(ID);
        }

        public ErrorMsg Del(int ID)
        {
            return core.Del(ID);
        }

        public ErrorMsg Recommend(string ids)
        {
            return core.Recommend(ids);
        }

        [HttpGet]
        public PageDatas<ExtraDto> GetExtraPageList(int page, int pageSize, string keywords = "", int outid = 0)
        {            
            PageDatas<ExtraDto> result = new PageDatas<ExtraDto>();
            result = extra.GetPageList(page, pageSize, keywords, outid);
            return result;
        }

        [HttpPost]
        public ErrorMsg EditExtra(ExtraDto model)
        {
            return extra.CreateOrUpdate(model);
        }

        public ReturnData<ExtraDto> GetExtraModel(int ID)
        {
            return extra.GetModel(ID);
        }
        
        public ErrorMsg DelExtra(int ID)
        {
            return extra.Del(ID);
        }
    }
}

using FCK.Studio.API.Filter;
using FCK.Studio.Core;
using FCK.Studio.Dto;
using System.Web.Http;

namespace FCK.Studio.API.Controllers
{
    [BasicAuthentication]
    public class ArticleController : ApiController
    {
        FCKArticles core = new FCKArticles();

        [HttpGet]
        public PageDatas<ArticleDto> GetPageList(int page, int pageSize, int cateid = 0, string catename = "", string keywords = "", string tag = "", string orderindex = "", int isrec = 0, int memberid = 0)
        {
            return core.GetPageList(page, pageSize, cateid, catename, keywords, tag, orderindex, isrec, memberid);
        }

        [HttpPost]
        public ErrorMsg CreateOrUpdate(ArticleDto model)
        {
            return core.CreateOrUpdate(model);
        }
        [HttpGet]
        public ErrorMsg AddCollections(int ID, int val)
        {
            return core.AddCollections(ID, val);
        }
        [HttpGet]
        public ErrorMsg AddComments(int ID, int val)
        {
            return core.AddComments(ID, val);
        }

        [HttpGet]
        public ErrorMsg AddHits(int ID)
        {
            return core.AddHits(ID);
        }

        [HttpPost]
        public ErrorMsg Del(string ids)
        {
            return core.Del(ids);
        }

        [HttpGet]
        public ArticleDto GetModel(int ID)
        {
            return core.GetModel(ID);
        }

    }
}

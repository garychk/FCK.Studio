using FCK.Studio.API.Filter;
using FCK.Studio.Core;
using FCK.Studio.Dto;
using System.Web.Http;

namespace FCK.Studio.API.Controllers
{
    [BasicAuthentication]
    public class EventsController : ApiController
    {
        FCKEvents core = new FCKEvents();

        public PageDatas<EventDto> GetPageList(int page, int pageSize, int memberid = 0, string keywords = "", string status = "", string stime = "", string etime = "")
        {
            return core.GetPageList(page, pageSize, memberid, keywords, status, stime, etime);
        }

        public ErrorMsg CreateOrUpdate(EventDto model)
        {
            return core.CreateOrUpdate(model);
        }

        public ErrorMsg Del(int ID)
        {
            return core.Del(ID);
        }

        public ReturnData<EventDto> GetModel(int ID)
        {
            return core.GetModel(ID);
        }
    }
}

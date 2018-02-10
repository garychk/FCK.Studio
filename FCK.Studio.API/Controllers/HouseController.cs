using FCK.Studio.API.Filter;
using FCK.Studio.Core;
using FCK.Studio.Dto;
using System.Collections.Generic;
using System.Web.Http;

namespace FCK.Studio.API.Controllers
{
    [BasicAuthentication]
    public class HouseController : ApiController
    {
        FCKHouses core = new FCKHouses();
        public PageDatas<HouseDto> GetPageList(int page, int pageSize, int grade = 0, string keywords = "", string itype = "", int maxP = 0, int minP = 0, string orderindex = "grade_desc")
        {
            return core.GetPageList(page, pageSize, grade, keywords, itype, maxP, minP, orderindex);
        }

        [HttpPost]
        public ErrorMsg CreateOrUpdate(HouseDto model)
        {
            return core.CreateOrUpdate(model);
        }

        public ErrorMsg UpdateStatus(int id, int status)
        {
            return core.UpdateStatus(id, status);
        }

        public ReturnData<HouseDto> GetModel(int id)
        {
            return core.GetModel(id);
        }

        public ReturnData<HouseDto> GetModelByCode(string code)
        {
            return core.GetModelByCode(code);
        }

        public ReturnData<List<HouseImageDto>> GetImageByCode(string code)
        {
            return core.GetImageByCode(code);
        }
    }
}

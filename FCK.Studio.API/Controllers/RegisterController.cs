using FCK.Studio.Core;
using FCK.Studio.Dto;
using System.Web.Http;

namespace FCK.Studio.API.Controllers
{
    public class RegisterController : ApiController
    {
        FCKRegisters core = new FCKRegisters();

        public ReturnData<RegisterDto> GetModelByNumber(string number)
        {
            return core.GetModelByNumber(number);
        }

    }
}

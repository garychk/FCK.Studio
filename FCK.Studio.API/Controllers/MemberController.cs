using FCK.Studio.API.Filter;
using FCK.Studio.Core;
using FCK.Studio.Dto;
using System.Web.Http;

namespace FCK.Studio.API.Controllers
{
    [BasicAuthentication]
    public class MemberController : ApiController
    {
        FCKMembers core = new FCKMembers();

        public PageDatas<MemberData> GetPageList(int page, int pageSize, string keywords = "")
        {
            return core.GetPageList(page, pageSize, keywords);
        }

        public MemberDto GetModel(int ID)
        {
            return core.GetModel(ID);
        }

        [HttpPost]
        public ErrorMsg CreateOrUpdate(MemberDto model)
        {
            return core.CreateOrUpdate(model);
        }

        [HttpPost]
        public ErrorMsg Login(LoginModel model)
        {
            return core.Login(model);
        }

        [HttpPost]
        public ErrorMsg UpdateHeader(int memberid, string headerurl)
        {
            return core.UpdateHeader(memberid, headerurl);
        }

        [HttpPost]
        public ErrorMsg ResetPassword(int memberid, string newPassword)
        {
            return core.ResetPassword(memberid, newPassword);
        }

        [HttpPost]
        public ErrorMsg UpdatePoints(int memberid, int points)
        {
            return core.UpdatePoints(memberid, points);
        }

        [HttpPost]
        public ErrorMsg EditGroup(GroupData model)
        {
            return core.EditGroup(model);
        }

        public ErrorMsg DelGroup(int groupid)
        {
            return core.DelGroup(groupid);
        }

        public GroupJson GetGroups(int page, int pageSize, string groupname = "")
        {
            return core.GetGroups(page, pageSize, groupname);
        }
    }
}

using FCK.Studio.Dto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FCK.Studio.Controls
{
    public class Member : Base
    {
        public MemberDto GetModel(int ID)
        {
            Dictionary<string, string> para = new Dictionary<string, string>();
            para.Add("partner", api_partner);
            para.Add("ID", ID.ToString());
            var request = BuildRequest(para);
            string JsonResponse = HttpWebGet(api_url + "/Member/GetModel?" + request);
            var result = JsonConvert.DeserializeObject<MemberDto>(JsonResponse);

            return result;
        }

        public PageDatas<MemberData> GetPageList(int page, int pageSize, string keywords = "")
        {
            Dictionary<string, string> para = new Dictionary<string, string>();
            para.Add("partner", api_partner);
            para.Add("page", page.ToString());
            para.Add("pageSize", pageSize.ToString());
            para.Add("keywords", keywords);
            var request = BuildRequest(para);
            string JsonResponse = HttpWebGet(api_url + "/Member/GetPageList?" + request);
            var result = JsonConvert.DeserializeObject<PageDatas<MemberData>>(JsonResponse);

            return result;
        }
    }
}

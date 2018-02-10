using FCK.Studio.Dto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FCK.Studio.Controls
{
    public class Article : Base
    {
        public ArticleDto GetModel(int ID)
        {
            Dictionary<string, string> para = new Dictionary<string, string>();
            para.Add("partner", api_partner);
            para.Add("ID", ID.ToString());
            var request = BuildRequest(para);
            string JsonResponse = HttpWebGet(api_url + "/Article/GetModel?" + request);
            var result = JsonConvert.DeserializeObject<ArticleDto>(JsonResponse);

            return result;
        }

        public PageDatas<ArticleDto> GetPageList(int page, int pageSize, int cateid = 0, string catename = "", string keywords = "", string tag = "", string orderindex = "", int isrec = 0, int memberid = 0)
        {
            PageDatas<ArticleDto> result = new PageDatas<ArticleDto>();
            Dictionary<string, string> para = new Dictionary<string, string>();
            para.Add("partner", api_partner);
            para.Add("page", page.ToString());
            para.Add("pageSize", pageSize.ToString());
            para.Add("cateid", cateid.ToString());
            para.Add("catename", catename);
            para.Add("keywords", keywords);
            para.Add("tag", tag);
            para.Add("orderindex", orderindex);
            para.Add("isrec", isrec.ToString());
            para.Add("memberid", memberid.ToString());

            var request = BuildRequest(para);
            string JsonResponse = HttpWebGet(api_url + "/Article/GetPageList?" + request);
            result = JsonConvert.DeserializeObject<PageDatas<ArticleDto>>(JsonResponse);

            return result;
        }
        
    }
}

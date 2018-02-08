using FCK.Studio.Dto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FCK.Studio.Controls
{
    public class Product : Base
    {
        public PageDatas<ProductDto> GetPageList(int page, int pageSize, int cateid = 0, string keywords = "", string pricerange = "", string orderindex = "pricedesc", int isrec = 0)
        {
            Dictionary<string, string> para = new Dictionary<string, string>();
            para.Add("partner", api_partner);
            para.Add("page", page.ToString());
            para.Add("pageSize", pageSize.ToString());
            para.Add("cateid", cateid.ToString());
            para.Add("keywords", keywords);
            para.Add("pricerange", pricerange);
            para.Add("orderindex", orderindex);
            para.Add("isrec", isrec.ToString());
            var request = BuildRequest(para);
            string JsonResponse = HttpWebGet(api_url + "/Product/GetPageList?" + request);
            var result = JsonConvert.DeserializeObject<PageDatas<ProductDto>>(JsonResponse);

            return result;
        }

        public ProductDto GetModel(int ID)
        {
            Dictionary<string, string> para = new Dictionary<string, string>();
            para.Add("partner", api_partner);
            para.Add("ID", ID.ToString());
            var request = BuildRequest(para);
            string JsonResponse = HttpWebGet(api_url + "/Product/GetModel?" + request);
            var result = JsonConvert.DeserializeObject<ProductDto>(JsonResponse);

            return result;
        }

        public PageDatas<ExtraDto> GetExtraPageList(int page, int pageSize, string keywords = "", int outid = 0)
        {
            Dictionary<string, string> para = new Dictionary<string, string>();
            para.Add("partner", api_partner);
            para.Add("page", page.ToString());
            para.Add("pageSize", pageSize.ToString());
            para.Add("keywords", keywords);
            para.Add("outid", outid.ToString());
            var request = BuildRequest(para);
            string JsonResponse = HttpWebGet(api_url + "/Product/GetExtraPageList?" + request);
            var result = JsonConvert.DeserializeObject<PageDatas<ExtraDto>>(JsonResponse);

            return result;
        }
    }
}

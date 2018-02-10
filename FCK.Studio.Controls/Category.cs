using FCK.Studio.Dto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FCK.Studio.Controls
{
    public class Category : Base
    {
        public List<CategoryDto> GetList(int parentid, string ctype = "")
        {
            Dictionary<string, string> para = new Dictionary<string, string>();
            para.Add("partner", api_partner);
            para.Add("parentid", parentid.ToString());
            para.Add("ctype", ctype);
            var request = BuildRequest(para);
            string JsonResponse = HttpWebGet(api_url + "/Category/GetList?" + request);
            var result = JsonConvert.DeserializeObject<List<CategoryDto>>(JsonResponse);

            return result;
        }
    }
}

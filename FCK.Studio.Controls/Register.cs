using FCK.Studio.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FCK.Studio.Controls
{
    public class Register : Base
    {
        /// <summary>
        /// 创建租户连接，原则上第一个执行
        /// </summary>
        /// <returns></returns>
        public static RegisterDto GetRegister()
        {
            RegisterDto returnStr = new RegisterDto();
            try
            {
                Dictionary<string, string> para = new Dictionary<string, string>();
                para.Add("number", api_partner);
                var request = BuildRequest(para);
                string JsonResponse = HttpWebGet(api_url + "/Register/GetModelByNumber?" + request);
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ReturnData<RegisterDto>>(JsonResponse);
                if (result != null)
                {
                    if (result.code == 100)
                    {
                        returnStr = result.datas;
                    }
                }

            }
            catch { }
            return returnStr;
        }
    }
}

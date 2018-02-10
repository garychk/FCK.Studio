using System;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;

namespace FCK.Common
{
    public class SMS
    {
        //static string username = ConfigurationManager.AppSettings["SMS_UserName"];
        //static string password = ConfigurationManager.AppSettings["SMS_Password"];
        /// <summary>
        /// 接口地址
        /// </summary>
        private static readonly string POST_URL = "http://180.97.163.89:8012/OpenPlatform/OpenApi";
        /// <summary>
        /// 接口帐号, 请换成你的帐号, 格式为 1001@+8位登录帐号+0001
        /// </summary>
        public static string ACCOUNT = "1001@800006980001";
        /// <summary>
        /// 接口密钥, 请换成你的帐号对应的接口密钥
        /// </summary>
        public static string AUTHKEY = "FE97DEACBFB2F07A57F842603B4607CD";
        /// <summary>
        /// 通道组编号, 请换成你的帐号可以使用的通道组编号
        /// </summary>
        private static readonly uint CGID = 6;

        // 余额信息
        public struct BalanceResult
        {
            public int nResult;
            public long dwCorpId;
            public int nStaffId;
            public float fRemain;
        }

        private static BalanceResult parseBalanceResult(string sResult)
        {
            BalanceResult ret = new BalanceResult();
            int nRet = parseResult(sResult);
            ret.nResult = nRet;
            if (nRet < 0) return ret;

            try
            {
                // 对返回值分析
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(sResult);
                XmlElement root = xml.DocumentElement;
                if (nRet > 0)
                {
                    XmlNode item = xml.SelectSingleNode("/xml/Item");
                    if (item != null)
                    {
                        ret.dwCorpId = Convert.ToInt64(item.Attributes["cid"].Value);
                        ret.nStaffId = Convert.ToInt32(item.Attributes["sid"].Value);
                        ret.fRemain = (float)Convert.ToDouble(item.Attributes["remain"].Value);
                    }
                }
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return ret;
        }

        /// <summary>
        /// 获取余额信息
        /// </summary>
        /// <returns></returns>
        public static BalanceResult getBalance()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("action=getBalance&ac=");
            sb.Append(ACCOUNT);
            sb.Append("&authkey=");
            sb.Append(AUTHKEY);

            string sResult = sendQuery(POST_URL, sb.ToString());
            return parseBalanceResult(sResult);
        }

        /// <summary>
        /// 群发接口
        /// </summary>
        /// <param name="mobile">手机号码, 如有多个使用逗号分隔, 支持1~3000个号码</param>
        /// <param name="content">内容, 500字以内</param>
        /// <param name="uCgid"></param>
        /// <param name="uCsid"></param>
        /// <returns></returns>
        public static int sendOnce(string mobile, string content, uint uCgid = 0, uint uCsid = 0)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("action=sendOnce&ac=");
            sb.Append(ACCOUNT);
            sb.Append("&authkey=");
            sb.Append(AUTHKEY);
            sb.Append("&cgid=");
            sb.Append(uCgid > 0 ? uCgid : CGID);
            if (uCsid > 0)
            {
                sb.Append("&csid=");
                sb.Append(uCsid);
            }
            sb.Append("&m=");
            sb.Append(mobile);
            sb.Append("&c=");
            sb.Append(UrlEncode(content, Encoding.UTF8));

            string sResult = sendQuery(POST_URL, sb.ToString());
            return parseResult(sResult);
        }

        private static int parseResult(string sResult)
        {
            if (sResult != null)
            {
                try
                {
                    // 对返回值分析
                    XmlDocument xml = new XmlDocument();
                    xml.LoadXml(sResult);
                    XmlElement root = xml.DocumentElement;
                    string sRet = root.GetAttribute("result");
                    return Convert.ToInt32(sRet);
                }
                catch (System.Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return -100;
        }

        private static string UrlEncode(string text, Encoding encoding)
        {
            StringBuilder sb = new StringBuilder();
            byte[] byData = encoding.GetBytes(text);
            for (int i = 0; i < byData.Length; i++)
            {
                sb.Append(@"%" + Convert.ToString(byData[i], 16));
            }
            return sb.ToString();
        }

        private static string sendQuery(string url, string param)
        {
            // 准备要POST的数据
            byte[] byData = Encoding.UTF8.GetBytes(param);

            // 设置发送的参数
            HttpWebRequest req = WebRequest.Create(url) as HttpWebRequest;
            req.Method = "POST";
            req.Timeout = 5000;
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = byData.Length;

            // 提交数据
            Stream rs = req.GetRequestStream();
            rs.Write(byData, 0, byData.Length);
            rs.Close();

            // 取响应结果
            HttpWebResponse resp = req.GetResponse() as HttpWebResponse;
            StreamReader sr = new StreamReader(resp.GetResponseStream(), Encoding.UTF8);

            try
            {
                return sr.ReadToEnd();
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
            }

            sr.Close();
            return null;
        }

        public static string GetReturn(int code)
        {
            switch (code)
            {
                case 0:
                    return "帐户格式不正确(正确的格式为:员工编号@企业编号)";
                case -1:
                    return "服务器拒绝(速度过快、限时或绑定IP不对等)如遇速度过快可延时再发";
                case -2:
                    return "密钥不正确";
                case -3:
                    return "密钥已锁定";
                case -4:
                    return "参数不正确(内容和号码不能为空，手机号码数过多，发送时间错误等)";
                case -5:
                    return "无此帐户";
                case -6:
                    return "帐户已锁定或已过期";
                case -7:
                    return "帐户未开启接口发送";
                case -8:
                    return "不可使用该通道组";
                case -9:
                    return "帐户余额不足";
                case -10:
                    return "内部错误";
                case -11:
                    return "扣费失败";
                default:
                    return code.ToString();
            }
        }
    }
}

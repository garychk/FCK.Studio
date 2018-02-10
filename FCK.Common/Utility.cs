using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.SessionState;

namespace FCK.Common
{
    public class Utility
    {
        private static Encoding myEncoding = Encoding.GetEncoding("utf-8");
        /// <summary>
        /// 新订单短信
        /// </summary>
        public static string SMS_NewOrder = ConfigurationManager.AppSettings["SMS_NewOrder"];
        /// <summary>
        /// 图片服务器
        /// </summary>
        public static string ImgServer = ConfigurationManager.AppSettings["ImgServer"];
        /// <summary>
        /// 缩略图尺寸
        /// </summary>
        public static string SmallPicSize = ConfigurationManager.AppSettings["SmallPicSize"];
        /// <summary>
        /// 邮箱服务器
        /// </summary>
        public static string MailServer = ConfigurationManager.AppSettings["MailServer"];
        /// <summary>
        /// 邮箱用户
        /// </summary>
        public static string MailAcount = ConfigurationManager.AppSettings["MailAcount"];
        /// <summary>
        /// 邮箱密码
        /// </summary>
        public static string MailPassword = ConfigurationManager.AppSettings["MailPassword"];
        /// <summary>
        /// Action权限开关
        /// </summary>
        public static string WebApiAuthFlag = ConfigurationManager.AppSettings["WebApiAuthFlag"];

        /// <summary>
        /// 初始化Session
        /// </summary>
        private static HttpSessionState _session = HttpContext.Current.Session;

        /// <summary>
        /// 设置Session
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetSession(string key, object value)
        {
            _session[key] = value;
        }
        /// <summary>
        /// 获取Session值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetSessionValue(string key)
        {
            try
            {
                return _session[key].ToString();
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 获取缩略图
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetSmallPic(string fileName)
        {
            try
            {
                string fileExt = System.IO.Path.GetExtension(fileName).ToLower();
                return fileName + "_" + SmallPicSize + fileExt;
            }
            catch { return fileName; }
        }
        /// <summary>
        /// 生成随机编号
        /// </summary>
        /// <param name="pre"></param>
        /// <returns></returns>
        public static string CreateRandNum(string pre)
        {
            return pre + DateTime.Now.ToString("yyMMddhhmmss") + new Random().Next(100, 999).ToString();
        }
        /// <summary>
        /// 转化状态
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public static string OrderStatus(int status)
        {
            switch (status)
            {
                case 0:
                    return "未确认";
                case 1:
                    return "等待支付";
                case 2:
                    return "支付完成";
                case 3:
                    return "已取衣";
                case 4:
                    return "完成";
                default:
                    return "";
            }
        }

        public static string appendParam(string returnStr, string paramId, string paramValue)
        {
            if (returnStr != "")
            {
                if (paramValue != "")
                {
                    returnStr += "&" + paramId + "=" + HttpUtility.UrlEncode(paramValue, myEncoding);
                }
            }
            else
            {
                if (paramValue != "")
                {
                    returnStr = paramId + "=" + HttpUtility.UrlEncode(paramValue, myEncoding);
                }
            }
            return returnStr;
        }

        public static int GetUrlError(string curl)
        {
            int num = 0;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(curl));
            ServicePointManager.Expect100Continue = false;
            try
            {
                ((HttpWebResponse)request.GetResponse()).Close();
            }
            catch (WebException exception)
            {
                if (exception.Status != WebExceptionStatus.ProtocolError)
                {
                    num = 200;
                }
                if (exception.Message.IndexOf("500 ") > 0)
                {
                    num = 500;
                }
                if (exception.Message.IndexOf("401 ") > 0)
                {
                    num = 401;
                }
                if (exception.Message.IndexOf("404") > 0)
                {
                    num = 404;
                }
            }
            return num;
        }
        /// <summary>
        /// 判断本地文件是否存在
        /// </summary>
        /// <param name="fileurl"></param>
        /// <returns></returns>
        public static bool GetFileExist(string fileurl)
        {
            if (!string.IsNullOrEmpty(fileurl))
            {
                string filepath = HttpContext.Current.Server.MapPath(fileurl);
                if (File.Exists(filepath))
                    return true;
                else
                {
                    string remotefile = ImgServer + fileurl;
                    return IsRemoteExist(remotefile);
                }
            }
            else
                return false;
        }

        public static bool IsRemoteExist(string fileurl)
        {
            HttpWebRequest req = null;
            HttpWebResponse res = null;
            try
            {
                req = (HttpWebRequest)WebRequest.Create(fileurl);
                req.Method = "HEAD";
                req.Timeout = 100;
                res = (HttpWebResponse)req.GetResponse();
                return (res.StatusCode == HttpStatusCode.OK);
            }
            catch
            {
                return false;
            }
            finally
            {
                if (res != null)
                {
                    res.Close();
                    res = null;
                }
                if (req != null)
                {
                    req.Abort();
                    req = null;
                }
            }
        } 

        public static int cInt(object val)
        {
            try
            {
                return Convert.ToInt32(val);
            }
            catch {
                return 0;
            }
        }

        public static bool cBool(object val)
        {
            bool result = false;
            try
            {
                result = Convert.ToBoolean(val);
            }
            catch
            { }
            return result;
        }

        public static DateTime cTime(object val)
        {
            try
            {
                return Convert.ToDateTime(val);
            }
            catch
            {
                return DateTime.Now;
            }
        }

        public static Decimal cDecimal(object val)
        {
            try
            {
                return Convert.ToDecimal(val);
            }
            catch
            {
                return 0;
            }
        }

        public static string CreateRandomCode(int n)
        {
            string charSet = "2,3,4,5,6,8,9,A,B,C,D,E,F,G,H,J,K,M,N,P,R,S,U,W,X,Y";
            string[] CharArray = charSet.Split(',');
            string randomCode = "";
            int temp = -1;
            Random rand = new Random();
            for (int i = 0; i < n; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(i * temp * ((int)DateTime.Now.Ticks));
                } int t = rand.Next(CharArray.Length - 1);
                if (temp == t)
                {
                    return CreateRandomCode(n);
                } temp = t;
                randomCode += CharArray[t];
            }
            return randomCode;
        }

        public static byte[] CreateImage(string checkCode)
        {
            int iwidth = (int)(checkCode.Length * 13);
            System.Drawing.Bitmap image = new System.Drawing.Bitmap(iwidth, 23);
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(image);
            System.Drawing.Font f = new System.Drawing.Font("Arial", 12, (System.Drawing.FontStyle.Italic | System.Drawing.FontStyle.Bold));        // 前景色        
            System.Drawing.Brush b = new System.Drawing.SolidBrush(System.Drawing.Color.Black);        // 背景色       
            g.Clear(System.Drawing.Color.White);        // 填充文字        
            g.DrawString(checkCode, f, b, 0, 1);        // 随机线条        
            System.Drawing.Pen linePen = new System.Drawing.Pen(System.Drawing.Color.Gray, 0); Random rand = new Random();
            for (int i = 0; i < 5; i++)
            {
                int x1 = rand.Next(image.Width);
                int y1 = rand.Next(image.Height);
                int x2 = rand.Next(image.Width);
                int y2 = rand.Next(image.Height);
                g.DrawLine(linePen, x1, y1, x2, y2);
            }        // 随机点        
            for (int i = 0; i < 30; i++)
            {
                int x = rand.Next(image.Width);
                int y = rand.Next(image.Height);
                image.SetPixel(x, y, System.Drawing.Color.Gray);
            }        // 边框       
            g.DrawRectangle(new System.Drawing.Pen(System.Drawing.Color.White), 0, 0, image.Width - 1, image.Height - 1);        // 输出图片  
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] result = ms.ToArray();
            g.Dispose();
            image.Dispose();
            return result;
        }

        public static string SendEmail(string inputMailsTo, string inputMailSubject, string textBoxMailBody, string mailServer = "", string mailAcount = "", string mailPassword = "")
        {
            try
            {
                bool flag = true;
                string result = "";
                mailServer = string.IsNullOrEmpty(mailServer) ? MailServer : mailServer;
                mailAcount = string.IsNullOrEmpty(mailAcount) ? MailAcount : mailAcount;
                mailPassword = string.IsNullOrEmpty(mailPassword) ? MailPassword : mailPassword;

                if (string.IsNullOrEmpty(mailServer))
                {
                    flag = false;
                    result = "mailServer can not null";
                }
                if (string.IsNullOrEmpty(mailAcount))
                {
                    flag = false;
                    result = "mailAcount can not null";
                }
                if (string.IsNullOrEmpty(mailPassword))
                {
                    flag = false;
                    result = "mailPassword can not null";
                }
                if (flag)
                {
                    SmtpClient client = new SmtpClient(mailServer);
                    client.UseDefaultCredentials = true;
                    client.Credentials = new System.Net.NetworkCredential(mailAcount, mailPassword);
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress(mailAcount);
                    mail.To.Add(inputMailsTo);
                    mail.Subject = inputMailSubject;
                    mail.BodyEncoding = System.Text.Encoding.Default;
                    mail.Body = textBoxMailBody;
                    mail.IsBodyHtml = true;
                    client.Send(mail);
                    result = "true";
                }
                return result;
            }
            catch (Exception err)
            {
                return err.Message;
            }
        }

        /// <summary>
        /// 结构转换
        /// </summary>
        /// <typeparam name="T">转换后的类型</typeparam>
        /// <param name="type">转换前的类型</param>
        /// <returns></returns>
        public static T MapTo<T>(object type)
        {
            string jsonText = JsonConvert.SerializeObject(type);
            T obj = JsonConvert.DeserializeObject<T>(jsonText);
            return obj;
        }

        /// <summary>
        /// 获取公网IP
        /// </summary>
        /// <returns></returns>
        public static string GetIP()
        {
            string tempip = "";
            string err = "";
            try
            {
                string htmlinfo = HttpHelper.Get("http://ip.qq.com/");//读取网站的数据
                Regex r = new Regex("((25[0-5]|2[0-4]\\d|1\\d\\d|[1-9]\\d|\\d)\\.){3}(25[0-5]|2[0-4]\\d|1\\d\\d|[1-9]\\d|[1-9])", RegexOptions.None);
                Match mc = r.Match(htmlinfo);
                //获取匹配到的IP
                tempip = mc.Groups[0].Value;
            }
            catch (Exception e)
            {
                err = e.Message;
            }
            return tempip;
        }

        /// <summary>
        /// 获取当前城市
        /// </summary>
        /// <param name="dataType"></param>
        /// <returns></returns>
        public static string GetCity(string dataType = "jsonp")
        {
            string result = "";
            try
            {
                string IP = GetIP();
                string ip138API = "http://api.ip138.com/query/";
                string response = HttpHelper.Get(ip138API + "?ip=" + IP + "&datatype=" + dataType + "&token=3c75da1deaa1d5cc23eb746e233a718d");
                Ip138Data ipdata = JsonConvert.DeserializeObject<Ip138Data>(response);
                List<string> lists = ipdata.data;
                if (lists.Count > 2)
                    result = lists[2];
            }
            catch {}
            return result;
        }
    }

    public class Ip138Data
    {
        public string ret { get; set; }
        public string ip { get; set; }
        public List<string> data { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;

namespace FCK.Studio.Frame.Core
{
    public class HttpHelper
    {
        private static Encoding code = Encoding.GetEncoding("utf-8");
        /// <summary>
        /// HttpClient Post方法
        /// </summary>
        /// <param name="url"></param>
        /// <param name="requestJson"></param>
        /// <returns></returns>
        public static string Post(string url, string requestJson)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                HttpContent httpContent = new StringContent(requestJson);
                httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/x-www-form-urlencoded");
                HttpResponseMessage response = httpClient.PostAsync(url, httpContent).Result;
                String result = response.Content.ReadAsStringAsync().Result;
                return result;
            }
            catch (Exception err)
            {
                return err.Message;
            }
        }
        /// <summary>
        /// HttpClient Get方法
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string Get(string url)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                HttpResponseMessage response = httpClient.GetAsync(url).Result;
                String result = response.Content.ReadAsStringAsync().Result;
                return result;
            }
            catch (Exception err)
            {
                return err.Message;
            }
        }

        public static string HttpWebPost(string apiurl, Dictionary<string, string> sParaTemp)
        {
            //待请求参数数组字符串
            string strRequestData = BuildRequest(sParaTemp, code);

            //把数组转换成流中所需字节数组类型
            byte[] bytesRequestData = code.GetBytes(strRequestData);

            //构造请求地址
            string strUrl = apiurl;

            //请求远程HTTP
            string strResult = "";
            try
            {
                //设置HttpWebRequest基本信息
                HttpWebRequest myReq = (HttpWebRequest)HttpWebRequest.Create(strUrl);
                myReq.Method = "post";
                myReq.ContentType = "application/x-www-form-urlencoded";

                //填充POST数据
                myReq.ContentLength = bytesRequestData.Length;
                Stream requestStream = myReq.GetRequestStream();
                requestStream.Write(bytesRequestData, 0, bytesRequestData.Length);
                requestStream.Close();

                //发送POST数据请求服务器
                HttpWebResponse HttpWResp = (HttpWebResponse)myReq.GetResponse();
                Stream myStream = HttpWResp.GetResponseStream();

                //获取服务器返回信息
                StreamReader reader = new StreamReader(myStream, code);
                StringBuilder responseData = new StringBuilder();
                String line;
                while ((line = reader.ReadLine()) != null)
                {
                    responseData.Append(line);
                }

                //释放
                myStream.Close();

                strResult = responseData.ToString();
            }
            catch (Exception exp)
            {
                strResult = exp.Message;
            }

            return strResult;
        }

        public static string HttpWebGet(string apiurl)
        {
            StringBuilder rStr = new StringBuilder();
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            StreamReader streamReader = null;
            try
            {
                request = (HttpWebRequest)HttpWebRequest.Create(apiurl);
                request.Method = "GET";
                request.Timeout = 500;
                response = (HttpWebResponse)request.GetResponse();
                streamReader = new StreamReader(response.GetResponseStream());
                rStr.Append(streamReader.ReadToEnd());
                streamReader.Close();
            }
            catch { }
            finally
            {
                if (streamReader != null)
                {
                    streamReader.Close();
                }
                if (response != null)
                {
                    response.Close();
                }
                if (request != null)
                {
                    request.Abort();
                }
            }
            return rStr.ToString();
        }

        public static string BuildRequest(Dictionary<string, string> dicArray, Encoding code)
        {
            StringBuilder prestr = new StringBuilder();
            foreach (KeyValuePair<string, string> temp in dicArray)
            {
                prestr.Append(temp.Key + "=" + HttpUtility.UrlEncode(temp.Value, code) + "&");
            }

            int nLen = prestr.Length;
            prestr.Remove(nLen - 1, 1);

            return prestr.ToString();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FCK.Studio.Frame.Core
{
    public class CookieHelper
    {
        /// <summary>
        /// Cookies赋值
        /// </summary>
        /// <param name="strName">主键</param>
        /// <param name="strValue">键值</param>
        /// <param name="days">有效期</param>
        /// <returns></returns>
        public static bool setCookie(string strName, string strValue, int days)
        {
            try
            {
                HttpCookie Cookie = new HttpCookie(strName);
                Cookie.Expires = DateTime.Now.AddDays(days);
                Cookie.Value = strValue;
                System.Web.HttpContext.Current.Response.Cookies.Add(Cookie);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 读取Cookies
        /// </summary>
        /// <param name="strName">主键</param>
        /// <returns></returns>

        public static HttpCookie getCookie(string strName)
        {
            HttpCookie Cookie = System.Web.HttpContext.Current.Request.Cookies[strName];
            if (Cookie != null)
            {
                return Cookie;
            }
            else
            {
                return null;
            }
        }

        public static string CookieVal(string strName)
        {
            try
            {
                HttpCookie Cookie = System.Web.HttpContext.Current.Request.Cookies[strName];
                if (Cookie != null)
                {
                    return Cookie.Value;
                }
                else
                {
                    return "";
                }
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 删除Cookies
        /// </summary>
        /// <param name="strName">主键</param>
        /// <returns></returns>
        public static bool delCookie(string strName, string domain)
        {
            try
            {
                HttpCookie Cookie = new HttpCookie(strName);
                Cookie.Values.Clear();
                Cookie.Domain = domain;
                Cookie.Expires = DateTime.Now.AddDays(-10);
                System.Web.HttpContext.Current.Response.Cookies.Add(Cookie);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 删除cookie中指定值
        /// </summary>
        /// <param name="delval">要删除的值</param>
        /// <param name="name">cookie名称</param>
        /// <returns></returns>
        public static string delCookieByValue(string delval, string name)
        {
            try
            {
                string CartValue = CookieHelper.getCookie(name).Value;
                string[] CartArr = CartValue.Split(',');
                List<string> NewCartArr = new List<string> { };
                foreach (string i in CartArr)
                {
                    if (i != delval)
                    {
                        NewCartArr.Add(i);
                    }
                }
                CartValue = "";
                foreach (string item in NewCartArr)
                {
                    if (CartValue.Length <= 0)
                    {
                        CartValue += item;
                    }
                    else
                    {
                        CartValue += "," + item;
                    }
                }
                if (!string.IsNullOrEmpty(CartValue))
                {
                    CookieHelper.setCookie(name, CartValue, 1440);
                }
                else
                {
                    CookieHelper.delCookie(name, "");
                }
                return "true";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
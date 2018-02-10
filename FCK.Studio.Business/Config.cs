using System.Configuration;

namespace FCK
{
    public class Config
    {
        public static string ImgServer = ConfigurationManager.AppSettings["ImgServer"];
        public static string cStatus(int num)
        {
            switch (num)
            {
                case 0:
                    return "待确认";
                case 1:
                    return "待支付";
                case 2:
                    return "支付成功";
                case 3:
                    return "已取货";
                case 4:
                    return "完成";
                default:
                    return "";
            }
        }
    }
}
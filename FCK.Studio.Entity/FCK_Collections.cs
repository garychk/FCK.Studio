using System;
using System.ComponentModel.DataAnnotations;

namespace FCK.Studio.Entity
{
    public class FCK_Collections : FCK_Model
    {
        [Key]
        public int Collection_ID { set; get; }
        /// <summary>
        /// 文章ID
        /// </summary>
        public int Article_ID { set; get; }
        /// <summary>
        /// 图片
        /// </summary>
        public string Article_Pic { set; get; }
        /// <summary>
        /// 文章标题
        /// </summary>
        public string Article_Title { set; get; }

        public int Product_ID { set; get; }
        public string Product_Name { set; get; }
        public string Product_Pic { set; get; }
        public string Product_Price { set; get; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int Member_ID { set; get; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string Member_NickName { set; get; }
        /// <summary>
        /// 收藏时间
        /// </summary>
        public DateTime Collection_Time { set; get; }
    }
}

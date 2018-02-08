using System;
using System.ComponentModel.DataAnnotations;

namespace FCK.Studio.Entity
{
    public class FCK_Shares: FCK_Model
    {
        [Key]
        public int Share_ID { set; get; }
        /// <summary>
        /// 分享码
        /// </summary>
        public string Share_Code { set; get; }
        /// <summary>
        /// 产品编号
        /// </summary>
        public int Product_ID { set; get; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string Product_Name { set; get; }
        /// <summary>
        /// 产品图片
        /// </summary>
        public string Product_Pic { set; get; }
        /// <summary>
        /// 产品价格
        /// </summary>
        public decimal Product_Price { set; get; }
        /// <summary>
        /// 会员编号
        /// </summary>
        public int Member_ID { set; get; }
        /// <summary>
        /// 分享佣金
        /// </summary>
        public decimal Share_Money { set; get; }
        /// <summary>
        /// 分享时间
        /// </summary>
        public DateTime Share_Time { set; get; }
    }
}

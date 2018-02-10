using System.ComponentModel.DataAnnotations;

namespace FCK.Studio.Entity
{
    public class FCK_Products : FCK_Model
    {
        [Key]
        public int Product_ID { set; get; }
        /// <summary>
        /// 产品分类
        /// </summary>
        public int Product_CateID { set; get; }
        /// <summary>
        /// 编号
        /// </summary>
        public string Product_Code { set; get; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Product_Name { set; get; }
        /// <summary>
        /// 颜色
        /// </summary>
        public string Product_Color { set; get; }
        /// <summary>
        /// 档次
        /// </summary>
        public string Product_Grade { set; get; }
        /// <summary>
        /// 概述
        /// </summary>
        public string Product_Intro { set; get; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Product_Order { set; get; }
        /// <summary>
        /// 销量
        /// </summary>
        public int Product_Sales { set; get; }
        /// <summary>
        /// 价格
        /// </summary>
        public decimal Product_Price { set; get; }
        /// <summary>
        /// 图片
        /// </summary>
        public string Product_Pic { set; get; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Product_Content { set; get; }
        /// <summary>
        /// 优化关键词
        /// </summary>
        public string Product_Keywords { set; get; }
        /// <summary>
        /// 优化描述
        /// </summary>
        public string Product_Description { set; get; }
        /// <summary>
        /// 推荐
        /// </summary>
        public int IsRec { set; get; }
        /// <summary>
        /// 上架
        /// </summary>
        public int IsOnSale { set; get; }
        /// <summary>
        /// 分享佣金
        /// </summary>
        public decimal Share_Money { set; get; }

    }
}

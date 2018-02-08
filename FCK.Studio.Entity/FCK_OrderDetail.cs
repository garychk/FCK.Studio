using System.ComponentModel.DataAnnotations;

namespace FCK.Studio.Entity
{
    public class FCK_OrderDetail : FCK_Model
    {
        [Key]
        public int OrderDetail_ID { set; get; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string Order_Number { set; get; }
        /// <summary>
        /// 编号
        /// </summary>
        public int Product_ID { set; get; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Product_Name { set; get; }
        /// <summary>
        /// 价格
        /// </summary>
        public decimal Product_Price { set; get; }
        /// <summary>
        /// 有效期
        /// </summary>
        public string Product_Code { set; get; }
        /// <summary>
        /// 权限
        /// </summary>
        public string Product_Grade { set; get; }
        public string Product_Pic { set; get; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Product_Number { set; get; }
    }
}

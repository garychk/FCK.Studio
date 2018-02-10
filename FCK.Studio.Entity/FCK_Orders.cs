using System;
using System.ComponentModel.DataAnnotations;

namespace FCK.Studio.Entity
{
    public class FCK_Orders : FCK_Model
    {
        [Key]
        public int Order_ID { set; get; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string Order_Number { set; get; }
        /// <summary>
        /// 会员编号
        /// </summary>
        public int Member_ID { set; get; }
        /// <summary>
        /// 会员名
        /// </summary>
        public string Member_Name { set; get; }
        /// <summary>
        /// 会员手机号
        /// </summary>
        public string Member_Mobile { set; get; }
        /// <summary>
        /// 会员联系电话
        /// </summary>
        public string Member_Telphone { set; get; }
        /// <summary>
        /// 订单总金额
        /// </summary>
        public decimal Order_Amount { set; get; }
        /// <summary>
        /// 订单时间
        /// </summary>
        public DateTime Order_Time { set; get; }
        /// <summary>
        /// 折扣
        /// </summary>
        public double Order_Discount { set; get; }
        /// <summary>
        /// 订单状态，0未确认、1等待支付、2支付完成、3已取货、4完成
        /// </summary>
        public int Order_Status { set; get; }
        /// <summary>
        /// 支付金额
        /// </summary>
        public string Order_PayMoney { set; get; }
        /// <summary>
        /// 支付方式，余额支付local，支付宝alipay，微信wepay，网银Bank_Id，其它other
        /// </summary>
        public string Order_Payway { set; get; }
        /// <summary>
        /// 订单备注
        /// </summary>
        public string Order_Memo { set; get; }
        /// <summary>
        /// 取件码
        /// </summary>
        public string Order_Code { set; get; }
        /// <summary>
        /// 分享码
        /// </summary>
        public string Share_Code { set; get; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace FCK.Studio.Entity
{
    public class FCK_Finance : FCK_Model
    {
        [Key]
        public int Finance_ID { set; get; }
        /// <summary>
        /// 交易编号
        /// </summary>
        public string Finance_Number { set; get; }
        /// <summary>
        /// 交易金额
        /// </summary>
        public decimal Finance_Amount { set; get; }
        /// <summary>
        /// 交易类型，comein收入、payout支出、refund退款、cash体现
        /// </summary>
        public string Finance_Type { set; get; }
        /// <summary>
        /// 交易时间
        /// </summary>
        public DateTime Finance_Time { set; get; }
        public string Finance_Memo { set; get; }
        /// <summary>
        /// 会员编号
        /// </summary>
        public int Member_ID { set; get; }
        /// <summary>
        /// 会员姓名
        /// </summary>
        public string Member_Name { set; get; }
        /// <summary>
        /// 注册用户名称
        /// </summary>
        public string Register_Name { set; get; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string Order_Number { set; get; }
    }
}

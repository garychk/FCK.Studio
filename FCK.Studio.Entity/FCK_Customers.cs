using System;
using System.ComponentModel.DataAnnotations;

namespace FCK.Studio.Entity
{
    public partial class FCK_Customers
    {
        public FCK_Customers()
        { }
        #region Model
        private int _customer_id;
        private string _customer_name;
        private string _customer_company;
        private string _customer_tel;
        private string _customer_mobile;
        private string _customer_email;
        private string _customer_qq;
        private string _customer_weixin;
        private string _customer_address;
        private string _customer_type;
        private int _customer_grade;
        private string _customer_longitude;
        private string _customer_latitude;
        private string _customer_memo;
        private DateTime _customer_updatetime;
        private int _member_id = 0;
        private int _register_id;

        [Key]
        public int Customer_ID
        {
            set { _customer_id = value; }
            get { return _customer_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Customer_Name
        {
            set { _customer_name = value; }
            get { return _customer_name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Customer_Company
        {
            set { _customer_company = value; }
            get { return _customer_company; }
        }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Customer_Tel
        {
            set { _customer_tel = value; }
            get { return _customer_tel; }
        }
        /// <summary>
        /// 手机号
        /// </summary>
        public string Customer_Mobile
        {
            set { _customer_mobile = value; }
            get { return _customer_mobile; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Customer_Email
        {
            set { _customer_email = value; }
            get { return _customer_email; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Customer_QQ
        {
            set { _customer_qq = value; }
            get { return _customer_qq; }
        }
        /// <summary>
        /// 微信
        /// </summary>
        public string Customer_Weixin
        {
            set { _customer_weixin = value; }
            get { return _customer_weixin; }
        }
        /// <summary>
        /// 联系地址
        /// </summary>
        public string Customer_Address
        {
            set { _customer_address = value; }
            get { return _customer_address; }
        }
        /// <summary>
        /// 类型
        /// </summary>
        public string Customer_Type
        {
            set { _customer_type = value; }
            get { return _customer_type; }
        }
        /// <summary>
        /// 星级
        /// </summary>
        public int Customer_Grade
        {
            set { _customer_grade = value; }
            get { return _customer_grade; }
        }
        /// <summary>
        /// 经度
        /// </summary>
        public string Customer_Longitude
        {
            set { _customer_longitude = value; }
            get { return _customer_longitude; }
        }
        /// <summary>
        /// 纬度
        /// </summary>
        public string Customer_Latitude
        {
            set { _customer_latitude = value; }
            get { return _customer_latitude; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Customer_Memo
        {
            set { _customer_memo = value; }
            get { return _customer_memo; }
        }
        //[DisplayFormat(DataFormatString = "{0:s}")]
        public DateTime Customer_UpdateTime
        {
            set { _customer_updatetime = value; }
            get { return _customer_updatetime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Member_ID
        {
            set { _member_id = value; }
            get { return _member_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Register_ID
        {
            set { _register_id = value; }
            get { return _register_id; }
        }
        #endregion Model

    }
}


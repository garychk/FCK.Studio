using System;
using System.ComponentModel.DataAnnotations;

namespace FCK.Studio.Entity
{
    public class FCK_Houses
    {
        #region Model
        private int _id;
        private string _house_code;
        private string _house_title;
        private string _house_image;
        private string _house_type;
        private int _house_beds = 1;
        private int _house_tenants = 1;
        private int _house_toilets = 1;
        private string _house_city;
        private string _house_country;
        private string _house_zone;
        private string _house_province;
        private string _house_address;
        private decimal _house_longitude = 0M;
        private decimal _house_latitude = 0M;
        private string _house_postcode;
        private string _house_facilities;
        private string _house_securityfacilities;
        private string _house_avaarea;
        private string _house_intro;
        private string _house_scene;
        private bool _house_ischildren = false;
        private bool _house_ispets = false;
        private bool _house_isbaby = false;
        private bool _house_issmoking = false;
        private bool _house_isparty = false;
        private int _house_arrivenoticedays = 3;
        private int _house_orderdaymax = 30;
        private int _house_advancedays = 30;
        private decimal _house_price = 0M;
        private decimal _house_discountweek = 1M;
        private decimal _house_discountmonth = 1M;
        private int _house_status = 0;
        private int _house_grade = 0;
        private decimal _house_evatrue = 0M;
        private decimal _house_evatraffic = 0M;
        private decimal _house_evahygiene = 0M;
        private decimal _house_evaattitude = 0M;
        private decimal _house_evacost = 0M;
        private decimal _house_evaprocess = 0M;
        private int _house_orders;
        private string _house_checkintime;
        private string _house_checkouttime;
        private DateTime _house_updatetimes;
        private int _member_id;
        private int _register_id = 0;

        [Key]
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 标题
        /// </summary>
        public string House_Title
        {
            set { _house_title = value; }
            get { return _house_title; }
        }
        /// <summary>
        /// 房源编码
        /// </summary>
        public string House_Code
        {
            set { _house_code = value; }
            get { return _house_code; }
        }
        /// <summary>
        /// 房源图片
        /// </summary>
        public string House_Image
        {
            set { _house_image = value; }
            get { return _house_image; }
        }
        /// <summary>
        /// 房源类型
        /// </summary>
        public string House_Type
        {
            set { _house_type = value; }
            get { return _house_type; }
        }
        /// <summary>
        /// 床位数
        /// </summary>
        public int House_Beds
        {
            set { _house_beds = value; }
            get { return _house_beds; }
        }
        /// <summary>
        /// 可住多少房客
        /// </summary>
        public int House_Tenants
        {
            set { _house_tenants = value; }
            get { return _house_tenants; }
        }
        /// <summary>
        /// 卫生间数量
        /// </summary>
        public int House_Toilets
        {
            set { _house_toilets = value; }
            get { return _house_toilets; }
        }
        /// <summary>
        /// 城市
        /// </summary>
        public string House_City
        {
            set { _house_city = value; }
            get { return _house_city; }
        }
        /// <summary>
        /// 国家
        /// </summary>
        public string House_Country
        {
            set { _house_country = value; }
            get { return _house_country; }
        }
        /// <summary>
        /// 区域
        /// </summary>
        public string House_Zone
        {
            set { _house_zone = value; }
            get { return _house_zone; }
        }
        /// <summary>
        /// 省份
        /// </summary>
        public string House_Province
        {
            set { _house_province = value; }
            get { return _house_province; }
        }
        /// <summary>
        /// 详细地址
        /// </summary>
        public string House_Address
        {
            set { _house_address = value; }
            get { return _house_address; }
        }
        /// <summary>
        /// 经度
        /// </summary>
        public decimal House_Longitude
        {
            set { _house_longitude = value; }
            get { return _house_longitude; }
        }
        /// <summary>
        /// 纬度
        /// </summary>
        public decimal House_Latitude
        {
            set { _house_latitude = value; }
            get { return _house_latitude; }
        }
        /// <summary>
        /// 邮政编码
        /// </summary>
        public string House_Postcode
        {
            set { _house_postcode = value; }
            get { return _house_postcode; }
        }
        /// <summary>
        /// 房屋便利设施
        /// </summary>
        public string House_Facilities
        {
            set { _house_facilities = value; }
            get { return _house_facilities; }
        }
        /// <summary>
        /// 房屋安全设施
        /// </summary>
        public string House_SecurityFacilities
        {
            set { _house_securityfacilities = value; }
            get { return _house_securityfacilities; }
        }
        /// <summary>
        /// 房客可使用区域
        /// </summary>
        public string House_AvaArea
        {
            set { _house_avaarea = value; }
            get { return _house_avaarea; }
        }
        /// <summary>
        /// 简介
        /// </summary>
        public string House_Intro
        {
            set { _house_intro = value; }
            get { return _house_intro; }
        }
        /// <summary>
        /// 房屋适用场景
        /// </summary>
        public string House_Scene
        {
            set { _house_scene = value; }
            get { return _house_scene; }
        }
        /// <summary>
        /// 2~12岁儿童是否可以入住
        /// </summary>
        public bool House_IsChildren
        {
            set { _house_ischildren = value; }
            get { return _house_ischildren; }
        }
        /// <summary>
        /// 宠物是否可以入住
        /// </summary>
        public bool House_IsPets
        {
            set { _house_ispets = value; }
            get { return _house_ispets; }
        }
        /// <summary>
        /// 婴儿是否可以入住
        /// </summary>
        public bool House_IsBaby
        {
            set { _house_isbaby = value; }
            get { return _house_isbaby; }
        }
        /// <summary>
        /// 是否允许吸烟
        /// </summary>
        public bool House_IsSmoking
        {
            set { _house_issmoking = value; }
            get { return _house_issmoking; }
        }
        /// <summary>
        /// 是否允许开Party
        /// </summary>
        public bool House_IsParty
        {
            set { _house_isparty = value; }
            get { return _house_isparty; }
        }
        /// <summary>
        /// 房客抵达前多少天通知
        /// </summary>
        public int House_ArriveNoticeDays
        {
            set { _house_arrivenoticedays = value; }
            get { return _house_arrivenoticedays; }
        }
        /// <summary>
        /// 最长租多少天
        /// </summary>
        public int House_OrderDayMax
        {
            set { _house_orderdaymax = value; }
            get { return _house_orderdaymax; }
        }
        /// <summary>
        /// 可以提前多少天预订
        /// </summary>
        public int House_AdvanceDays
        {
            set { _house_advancedays = value; }
            get { return _house_advancedays; }
        }
        /// <summary>
        /// 每晚入住价格
        /// </summary>
        public decimal House_Price
        {
            set { _house_price = value; }
            get { return _house_price; }
        }
        /// <summary>
        /// 周折扣
        /// </summary>
        public decimal House_DiscountWeek
        {
            set { _house_discountweek = value; }
            get { return _house_discountweek; }
        }
        /// <summary>
        /// 月折扣
        /// </summary>
        public decimal House_DiscountMonth
        {
            set { _house_discountmonth = value; }
            get { return _house_discountmonth; }
        }
        /// <summary>
        /// 发布状态
        /// </summary>
        public int House_Status
        {
            set { _house_status = value; }
            get { return _house_status; }
        }
        /// <summary>
        /// 星级
        /// </summary>
        public int House_Grade
        {
            set { _house_grade = value; }
            get { return _house_grade; }
        }
        /// <summary>
        /// 如实描述指数
        /// </summary>
        public decimal House_EvaTrue
        {
            set { _house_evatrue = value; }
            get { return _house_evatrue; }
        }
        /// <summary>
        /// 交通便利指数
        /// </summary>
        public decimal House_EvaTraffic
        {
            set { _house_evatraffic = value; }
            get { return _house_evatraffic; }
        }
        /// <summary>
        /// 卫生指数
        /// </summary>
        public decimal House_EvaHygiene
        {
            set { _house_evahygiene = value; }
            get { return _house_evahygiene; }
        }
        /// <summary>
        /// 服务态度指数
        /// </summary>
        public decimal House_EvaAttitude
        {
            set { _house_evaattitude = value; }
            get { return _house_evaattitude; }
        }
        /// <summary>
        /// 性价比指数
        /// </summary>
        public decimal House_EvaCost
        {
            set { _house_evacost = value; }
            get { return _house_evacost; }
        }
        /// <summary>
        /// 入住手续方便指数
        /// </summary>
        public decimal House_EvaProcess
        {
            set { _house_evaprocess = value; }
            get { return _house_evaprocess; }
        }
        /// <summary>
        /// 预订次数
        /// </summary>
        public int House_Orders
        {
            set { _house_orders = value; }
            get { return _house_orders; }
        }
        /// <summary>
        /// 入住时间
        /// </summary>
        public string House_CheckinTime
        {
            set { _house_checkintime = value; }
            get { return _house_checkintime; }
        }
        /// <summary>
        /// 退房时间
        /// </summary>
        public string House_CheckoutTime
        {
            set { _house_checkouttime = value; }
            get { return _house_checkouttime; }
        }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime House_UpdateTimes
        {
            set { _house_updatetimes = value; }
            get { return _house_updatetimes; }
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

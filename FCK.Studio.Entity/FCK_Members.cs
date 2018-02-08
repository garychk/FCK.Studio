using System;
using System.ComponentModel.DataAnnotations;

namespace FCK.Studio.Entity
{
    public class FCK_Members : FCK_Model
    {
        [Key]
        public int Member_ID { set; get; }
        /// <summary>
        /// 会员名
        /// </summary>
        public string Member_UserName { set; get; }
        /// <summary>
        /// 会员密码
        /// </summary>
        public string Member_Password { set; get; }
        /// <summary>
        /// 头像
        /// </summary>
        public string Member_Header { set; get; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string Member_NickName { set; get; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Member_Name { set; get; }
        /// <summary>
        /// 手机
        /// </summary>
        public string Member_Mobile { set; get; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Member_Telphone { set; get; }
        /// <summary>
        /// 微信号
        /// </summary>
        public string Member_Weixin { set; get; }
        /// <summary>
        /// 电子邮箱
        /// </summary>
        public string Member_Email { set; get; }
        /// <summary>
        /// 性别，1男，0女
        /// </summary>
        public int Member_Sex { set; get; }
        /// <summary>
        /// QQ
        /// </summary>
        public string Member_QQ { set; get; }
        /// <summary>
        /// 联系地址
        /// </summary>
        public string Member_Address { set; get; }
        /// <summary>
        /// 积分
        /// </summary>
        public int Member_Points { set; get; }
        /// <summary>
        /// 余额
        /// </summary>
        public decimal Member_Amount { set; get; }
        /// <summary>
        /// 分组编号
        /// </summary>
        public int Group_ID { set; get; }
        /// <summary>
        /// 状态
        /// </summary>
        public int Member_Status { set; get; }
        /// <summary>
        /// 加入时间
        /// </summary>
        public DateTime Member_JoinTime { set; get; }
        /// <summary>
        /// QQ开放平台ID
        /// </summary>
        public string Member_QQ_OpenID { set; get; }
        /// <summary>
        /// 微信开放平台ID
        /// </summary>
        public string Member_WX_OpenID { set; get; }
        /// <summary>
        /// 会员API权限
        /// </summary>
        public string Member_ApiPower { set; get; }
    }
}

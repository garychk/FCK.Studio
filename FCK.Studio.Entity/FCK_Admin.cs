using System;
using System.ComponentModel.DataAnnotations;

namespace FCK.Studio.Entity
{
    public class FCK_Admin
    {
        [Key]
        public int Admin_ID { set; get; }       
        /// <summary>
        /// 管理员登录名
        /// </summary>
        public string Admin_Name { set; get; }
        /// <summary>
        /// 登录密码
        /// </summary>
        public string Admin_Password { set; get; }
        /// <summary>
        /// 管理员代码
        /// </summary>
        public string Admin_Code { set; get; }
        /// <summary>
        /// 管理员权限
        /// </summary>
        public string Admin_Power { set; get; }
        /// <summary>
        /// 授权注册用户ID
        /// </summary>
        public int Register_ID { set; get; }
        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime Admin_RegTime { set; get; }
        /// <summary>
        /// 状态
        /// </summary>
        public int Admin_Status { set; get; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Admin_TrueName { set; get; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Admin_Telphone { set; get; }
    }
}

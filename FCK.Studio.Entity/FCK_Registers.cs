using System;
using System.ComponentModel.DataAnnotations;

namespace FCK.Studio.Entity
{
    public class FCK_Registers
    {
        [Key]
        public int Register_ID { set; get; }
        /// <summary>
        /// 租户编号
        /// </summary>
        public string Register_Number { set; get; }
        /// <summary>
        /// 租户用户名
        /// </summary>
        public string Register_Name { set; get; }
        /// <summary>
        /// 注册用户类型
        /// </summary>
        public string Register_Type { set; get; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string Register_Contactor { set; get; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Register_Telphone { set; get; }
        /// <summary>
        /// 手机
        /// </summary>
        public string Register_Mobile { set; get; }
        /// <summary>
        /// 电子邮箱
        /// </summary>
        public string Register_Email { set; get; }
        /// <summary>
        /// 省份
        /// </summary>
        public string Register_Province { set; get; }
        /// <summary>
        /// 城市
        /// </summary>
        public string Register_City { set; get; }
        /// <summary>
        /// 区域
        /// </summary>
        public string Register_Zone { set; get; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Register_Address { set; get; }
        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime Register_AddTime { set; get; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Register_Memo { set; get; }
        /// <summary>
        /// 等级
        /// </summary>
        public int Register_Grade { set; get; }
        /// <summary>
        /// 状态
        /// </summary>
        public int Register_Status { set; get; }
        /// <summary>
        /// 身份证号码
        /// </summary>
        public string Register_IDNumber { set; get; }
        /// <summary>
        /// 营业执照
        /// </summary>
        public string Register_BusinessLicense { set; get; }
        /// <summary>
        /// 组织机构代码证
        /// </summary>
        public string Register_OrgCodeCert { set; get; }
        /// <summary>
        /// API权限
        /// </summary>
        public string Register_ApiPower { set; get; }
        /// <summary>
        /// 截止日期
        /// </summary>
        public DateTime Register_DeadLine { set; get; }
        /// <summary>
        /// 余额
        /// </summary>
        public decimal Register_Money { set; get; }
    }
}

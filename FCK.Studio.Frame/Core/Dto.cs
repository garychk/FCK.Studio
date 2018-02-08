using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FCK.Studio.Frame.Core
{
    public class ErrorMsg
    {
        public int code { get; set; }
        public int id { get; set; }
        public string message { get; set; }
    }

    public class ReturnData<T>
    {
        public int code { get; set; }
        public int id { get; set; }
        public string message { get; set; }
        public T datas { get; set; }
    }

    public class RegisterDto
    {
        public int Register_ID { set; get; }
        /// <summary>
        /// 注册用户编号
        /// </summary>
        //public string Register_Number { set; get; }
        /// <summary>
        /// 注册用户名
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
        //public string Register_IDNumber { set; get; }
        /// <summary>
        /// 营业执照
        /// </summary>
        //public string Register_BusinessLicense { set; get; }
        /// <summary>
        /// 组织机构代码证
        /// </summary>
        //public string Register_OrgCodeCert { set; get; }
        /// <summary>
        /// API权限
        /// </summary>
        //public string Register_ApiPower { set; get; }
    }

    public class EventDto
    {
        public int Event_ID { set; get; }
        /// <summary>
        /// 事务代号
        /// </summary>
        public string Event_Code { set; get; }
        /// <summary>
        /// 事务名称
        /// </summary>
        public string Event_Name { set; get; }
        /// <summary>
        /// 事务内容
        /// </summary>
        public string Event_Content { set; get; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime Event_StartTime { set; get; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime Event_EndTime { set; get; }
        /// <summary>
        /// 事务等级，1最高，以此类推
        /// </summary>
        public int Event_Grade { set; get; }
        /// <summary>
        /// 事务总监
        /// </summary>
        public int Event_Leader { set; get; }
        /// <summary>
        /// 事务负责人
        /// </summary>
        public int Event_Resperson { set; get; }
        /// <summary>
        /// 子事务
        /// </summary>
        public string Event_Subs { set; get; }
        /// <summary>
        /// 事务状态
        /// </summary>
        public string Event_Status { set; get; }
        /// <summary>
        /// 事务父级
        /// </summary>
        public int Event_ParentId { set; get; }
        /// <summary>
        /// 事务文档地址
        /// </summary>
        public string Event_Document { set; get; }
        /// <summary>
        /// 事务接手时间
        /// </summary>
        public DateTime Event_TakeTime { set; get; }
        /// <summary>
        /// 事务完成时间
        /// </summary>
        public DateTime Event_OverTime { set; get; }
        /// <summary>
        /// 事务完成报告
        /// </summary>
        public string Event_OverReport { set; get; }
        /// <summary>
        /// 事务绩效点
        /// </summary>
        public int Event_Points { set; get; }
    }

    public class PageDatas<T>
    {
        public int pages { get; set; }
        public int total { get; set; }
        public List<T> datas { get; set; }
    }

    public class ArticleDto
    {
        public int Article_ID { set; get; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Article_Title { set; get; }
        /// <summary>
        /// 简介
        /// </summary>
        public string Article_Brief { set; get; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Article_Contents { set; get; }
        /// <summary>
        /// 图片
        /// </summary>
        public string Article_Pic { set; get; }
        /// <summary>
        /// 缩略图
        /// </summary>
        public string Article_PicSmall { set; get; }
        /// <summary>
        /// 分类编号
        /// </summary>
        public int Article_CateID { set; get; }
        /// <summary>
        /// 分类名称
        /// </summary>
        public string Article_CateName { set; get; }
        /// <summary>
        /// 作者
        /// </summary>
        public string Article_Author { set; get; }
        /// <summary>
        /// 外部链接
        /// </summary>
        public string Article_NavUrl { set; get; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Article_OrderID { set; get; }
        /// <summary>
        /// 标签
        /// </summary>
        public string Article_Tag { set; get; }
        /// <summary>
        /// 用户编号
        /// </summary>
        public int Article_MemberID { set; get; }
        /// <summary>
        /// 推荐标记
        /// </summary>
        public int Article_IsRec { set; get; }
        /// <summary>
        /// 点击量
        /// </summary>
        public int Article_Hits { set; get; }
        /// <summary>
        /// 资源分
        /// </summary>
        public int Article_Point { set; get; }
        /// <summary>
        /// 评论数
        /// </summary>
        public int Article_Comments { set; get; }
        /// <summary>
        /// 收藏数
        /// </summary>
        public int Article_Collections { set; get; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime Article_UpdateTime { set; get; }
    }
}
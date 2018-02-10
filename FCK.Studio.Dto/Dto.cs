using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FCK.Studio.Dto
{
    public class ErrorMsg
    {
        public int code { get; set; }
        public int id { get; set; }
        public string message { get; set; }
    }

    public class OrderDetailData
    {
        public int OrderDetail_ID { set; get; }
        public int Product_ID { set; get; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string Order_Number { set; get; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Product_Name { set; get; }
        /// <summary>
        /// 编号
        /// </summary>
        public string Product_Code { set; get; }
        /// <summary>
        /// 等级
        /// </summary>
        public string Product_Grade { set; get; }
        /// <summary>
        /// 价格
        /// </summary>
        public string Product_Price { set; get; }        
        public string Product_Pic { set; get; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Product_Number { set; get; }
    }

    public class OrdersData
    {
        public int Order_ID { set; get; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string Order_Number { set; get; }
        public int Member_ID { set; get; }
        /// <summary>
        /// 会员名
        /// </summary>
        public string Member_Name { set; get; }
        public string Member_Telphone { set; get; }
        public string Member_Mobile { set; get; }
        /// <summary>
        /// 订单总金额
        /// </summary>
        public string Order_Amount { set; get; }
        /// <summary>
        /// 订单时间
        /// </summary>
        public string Order_Time { set; get; }
        /// <summary>
        /// 订单状态,0未确认、1等待支付、2支付完成、3已取货、4完成
        /// </summary>
        public int Order_Status { set; get; }
        /// <summary>
        /// 支付方式
        /// </summary>
        public string Order_Payway { set; get; }
        public string Order_Memo { set; get; }
    }

    public class OrdersJson
    {
        public int pages { get; set; }
        public int total { get; set; }
        public List<OrdersData> datas { get; set; }
    }

    public class OrderEdit
    {
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
        /// 商户编号
        /// </summary>
        public int Register_ID { set; get; }
        /// <summary>
        /// 会员联系电话
        /// </summary>
        public string Member_Telphone { set; get; }
        /// <summary>
        /// 订单总金额
        /// </summary>
        public decimal Order_Amount { set; get; }
        /// <summary>
        /// 订单折扣
        /// </summary>
        public double Order_Discount { set; get; }
        /// <summary>
        /// 订单备注
        /// </summary>
        public string Order_Memo { set; get; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public int Order_Status { set; get; }
        /// <summary>
        /// 订单详情
        /// </summary>
        public string OrderDetail { set; get; }
        /// <summary>
        /// 支付金额
        /// </summary>
        public string Order_PayMoney { set; get; }
        /// <summary>
        /// 支付方式，余额支付local，支付宝alipay，微信wepay，网银Bank_Id，其它other
        /// </summary>
        public string Order_Payway { set; get; }
        /// <summary>
        /// 分享码
        /// </summary>
        public string Share_Code { set; get; }
    }

    public class OrderDetailEdit
    {
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
        public string Product_Pic { set; get; }
        /// <summary>
        /// 价格
        /// </summary>
        public decimal Product_Price { set; get; }
        public string Product_Code { set; get; }
        public string Product_Grade { set; get; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Product_Number { set; get; }
    }

    public class LoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string IP { get; set; }
    }

    public class EditAdminM
    {
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
        /// 姓名
        /// </summary>
        public string Admin_TrueName { set; get; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Admin_Telphone { set; get; }
    }

    public class AdminsJson
    {
        public int pages { get; set; }
        public int total { get; set; }
        public List<AdminData> datas { get; set; }
    }

    public class AdminData
    {
        public int Admin_ID { set; get; }
        /// <summary>
        /// 管理员登录名
        /// </summary>
        public string Admin_Name { set; get; }
        /// <summary>
        /// 管理员代码
        /// </summary>
        public string Admin_Code { set; get; }
        /// <summary>
        /// 授权注册用户ID
        /// </summary>
        public int Register_ID { set; get; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Admin_TrueName { set; get; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Admin_Telphone { set; get; }

        public int Admin_Status { set; get; }

        public string Admin_RegTime { set; get; }
    }

    public class EditFinance
    {
        public int Finance_ID { set; get; }
        /// <summary>
        /// 交易金额
        /// </summary>
        public string Finance_Amount { set; get; }
        /// <summary>
        /// 交易类型，comein收入、payout支出、refund退款、cash体现
        /// </summary>
        public string Finance_Type { set; get; }
        /// <summary>
        /// 会员编号
        /// </summary>
        public int Member_ID { set; get; }
        /// <summary>
        /// 会员姓名
        /// </summary>
        public string Member_Name { set; get; }
        /// <summary>
        /// 注册用户编号
        /// </summary>
        public int Register_ID { set; get; }
        /// <summary>
        /// 注册用户名称
        /// </summary>
        public string Register_Name { set; get; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string Order_Number { set; get; }

        public DateTime Finance_Time { set; get; }
        public string Finance_Memo { set; get; }
    }

    public class FinanceJson
    {
        public int pages { get; set; }
        public int total { get; set; }
        public List<FinanceData> datas { get; set; }
    }

    public class FinanceData
    {
        public int Finance_ID { set; get; }
        /// <summary>
        /// 交易金额
        /// </summary>
        public string Finance_Amount { set; get; }
        /// <summary>
        /// 交易类型，comein收入、payout支出、refund退款、cash体现
        /// </summary>
        public string Finance_Type { set; get; }
        /// <summary>
        /// 交易时间
        /// </summary>
        public string Finance_Time { set; get; }
        /// <summary>
        /// 会员姓名
        /// </summary>
        public string Member_Name { set; get; }
        public string Finance_Memo { set; get; }
    }

    public class ProductsJson
    {
        public int pages { get; set; }
        public int total { get; set; }
        public List<ProductDto> datas { get; set; }
    }

    public class ProductDto
    {
        public int Product_ID { set; get; }
        /// <summary>
        /// 产品分类
        /// </summary>
        public int Product_CateID { set; get; }
        /// <summary>
        /// 分类名称
        /// </summary>
        public string Product_CateName { set; get; }
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
        /// 优化关键词
        /// </summary>
        public string Product_Keywords { set; get; }
        /// <summary>
        /// 优化描述
        /// </summary>
        public string Product_Description { set; get; }

        public int IsRec { set; get; }

        public int IsOnSale { set; get; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Product_Content { set; get; }

        public int Register_ID { set; get; }
        /// <summary>
        /// 分享佣金
        /// </summary>
        public decimal Share_Money { set; get; }
    }

    public class ArticleJson
    {
        public int pages { get; set; }
        public int total { get; set; }
        public List<ArticleDto> datas { get; set; }
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
        /// 优化关键词
        /// </summary>
        public string Article_Keywords { set; get; }
        /// <summary>
        /// 优化描述
        /// </summary>
        public string Article_Description { set; get; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime Article_UpdateTime { set; get; }
    }

    public class MemberJson
    {
        public int pages { get; set; }
        public int total { get; set; }
        public List<MemberData> datas { get; set; }
    }

    public class MemberData
    {
        public int Member_ID { set; get; }
        /// <summary>
        /// 会员名
        /// </summary>
        public string Member_UserName { set; get; }
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
        /// QQ
        /// </summary>
        public string Member_QQ { set; get; }
        /// <summary>
        /// 电子邮箱
        /// </summary>
        public string Member_Email { set; get; }
        /// <summary>
        /// 状态
        /// </summary>
        public int Member_Status { set; get; }
        /// <summary>
        /// 分组名称
        /// </summary>
        public string Group_Name { set; get; }
        /// <summary>
        /// 折扣
        /// </summary>
        public string Group_Discount { set; get; }
        /// <summary>
        /// 余额
        /// </summary>
        public string Member_Amount { set; get; }
    }

    public class MemberDto
    {
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
        /// 折扣
        /// </summary>
        public double Group_Discount { set; get; }
        /// <summary>
        /// 会员API权限
        /// </summary>
        public string Member_ApiPower { set; get; }
        /// <summary>
        /// 加入时间
        /// </summary>
        public DateTime Member_JoinTime { set; get; }
    }

    public class GroupJson
    {
        public int pages { get; set; }
        public int total { get; set; }
        public List<GroupData> datas { get; set; }
    }

    public class GroupData
    {
        public int Group_ID { set; get; }
        /// <summary>
        /// 分组名称
        /// </summary>
        public string Group_Name { set; get; }
        /// <summary>
        /// 折扣
        /// </summary>
        public double Group_Discount { set; get; }
        /// <summary>
        /// 积分
        /// </summary>
        public int Group_Points { set; get; }

        public int Group_ParentId { set; get; }
    }

    public class CodeJson
    {
        public int pages { get; set; }
        public int total { get; set; }
        public List<CodeData> datas { get; set; }
    }

    public class CodeData
    {
        public int Code_ID { set; get; }
        /// <summary>
        /// 索引名称
        /// </summary>
        public string Code_Name { set; get; }
        /// <summary>
        /// 索引值
        /// </summary>
        public string Code_Value { set; get; }
        /// <summary>
        /// 索引类型
        /// </summary>
        public string Code_Type { set; get; }
    }

    public class LogJson
    {
        public int pages { get; set; }
        public int total { get; set; }
        public List<LogData> datas { get; set; }
    }

    public class LogData
    {
        public int Log_ID { set; get; }
        /// <summary>
        /// 日志内容
        /// </summary>
        public string Log_Content { set; get; }
        /// <summary>
        /// 日志类型
        /// </summary>
        public string Log_Type { set; get; }
        /// <summary>
        /// 日志时间
        /// </summary>
        public string Log_Time { set; get; }
    }

    public class FinanceChartData
    {
        public string name { get; set; }
        public List<int> data { get; set; }
        public List<string> datetime { get; set; }
    }

    public class TreeNode
    {
        public int id { get; set; }
        public string text { get; set; }
        public List<TreeNode> nodes { get; set; }
    }

    public class CategoryDto
    {
        public int Category_ID { set; get; }
        /// <summary>
        /// 分类名称
        /// </summary>
        public string Category_Name { set; get; }
        /// <summary>
        /// 分类索引
        /// </summary>
        public string Category_Index { set; get; }

        public int Category_ParentID { set; get; }
        public int Category_OrderID { set; get; }
        public int Category_Depth { set; get; }
        /// <summary>
        /// 类型
        /// </summary>
        public string Category_Type { set; get; }
        /// <summary>
        /// 子集数量
        /// </summary>
        public int Children_Num { set; get; }
    }

    public class CategoryList
    {
        public List<CategoryDto> datas { get; set; }
    }

    public class StringList
    {
        public List<string> datas { get; set; }
    }

    public class CommentsData
    {
        public int Comment_ID { set; get; }
        /// <summary>
        /// 评论内容
        /// </summary>
        public string Comment_Contents { set; get; }
        /// <summary>
        /// 评论时间
        /// </summary>
        public DateTime? Comment_Times { set; get; }
        /// <summary>
        /// 赞
        /// </summary>
        public int Comment_Good { set; get; }
        /// <summary>
        /// 踩
        /// </summary>
        public int Comment_Bad { set; get; }
        /// <summary>
        /// 文章ID
        /// </summary>
        public int Article_ID { set; get; }
        /// <summary>
        /// 文章标题
        /// </summary>
        public string Article_Title { set; get; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int Member_ID { set; get; }
        /// <summary>
        /// 用户头像
        /// </summary>
        public string Member_Header { set; get; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string Member_NickName { set; get; }

    }

    public class CommentsJson
    {
        public int pages { get; set; }
        public int total { get; set; }
        public List<CommentsData> datas { get; set; }
    }

    public class CollectionsJson
    {
        public int pages { get; set; }
        public int total { get; set; }
        public List<CollectionsDto> datas { get; set; }
    }

    public class CollectionsDto
    {
        public int Collection_ID { set; get; }
        /// <summary>
        /// 文章ID
        /// </summary>
        public int Article_ID { set; get; }
        /// <summary>
        /// 文章标题
        /// </summary>
        public string Article_Title { set; get; }
        /// <summary>
        /// 图片
        /// </summary>
        public string Article_Pic { set; get; }
        public int Product_ID { set; get; }
        public string Product_Name { set; get; }
        public string Product_Pic { set; get; }
        public string Product_Price { set; get; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int Member_ID { set; get; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string Member_NickName { set; get; }
        /// <summary>
        /// 收藏时间
        /// </summary>
        public DateTime Collection_Time { set; get; }
    }

    public class AdvDto
    {
        public string advtag { get; set; }
        public string advname { get; set; }
        public string advurl { get; set; }
        public string advpic { get; set; }
        public string target { get; set; }
        public string advcon { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public int isdiy { get; set; }
    }

    /// <summary>
    /// 泛型分页数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageDatas<T>
    {
        public int pages { get; set; }
        public int total { get; set; }
        public List<T> datas { get; set; }
    }

    /// <summary>
    /// 接口授权基本信息
    /// </summary>
    public class ApiAuth
    {
        /// <summary>
        /// 填写分配给接口合作商的授权code
        /// </summary>
        public string partner { get; set; }
        /// <summary>
        /// 接口合作商密码
        /// </summary>
        public string key { get; set; }
    }

    public class FCKAPIDto
    {
        public int ID { get; set; }
        /// <summary>
        /// API标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        public int CateId { get; set; }
        /// <summary>
        /// API方法名
        /// </summary>
        public string API { get; set; }
        /// <summary>
        /// 调用地址
        /// </summary>
        public string URL { get; set; }
        /// <summary>
        /// 调用方式：POST或GET
        /// </summary>
        public string Methord { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 调用次数
        /// </summary>
        public int UseTimes { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; }
    }

    public class FCKAPIParaDto
    {
        public int ID { get; set; }
        /// <summary>
        /// API编号
        /// </summary>
        public int API_ID { get; set; }
        /// <summary>
        /// 参数名
        /// </summary>
        public string Para_Name { get; set; }
        /// <summary>
        /// 参数类型
        /// </summary>
        public string Para_Type { get; set; }
        /// <summary>
        /// 参数长度
        /// </summary>
        public int Para_Length { get; set; }
        /// <summary>
        /// 是否必填
        /// </summary>
        public int Para_Required { get; set; }
        /// <summary>
        /// 参数介绍
        /// </summary>
        public string Para_Intro { get; set; }
    }

    public class RegisterDto
    {
        public int Register_ID { set; get; }
        /// <summary>
        /// 注册用户编号
        /// </summary>
        public string Register_Number { set; get; }
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

    public class ExtraDto
    {
        public int ID { set; get; }
        /// <summary>
        /// 属性名
        /// </summary>
        public string ExtraName { set; get; }
        /// <summary>s
        /// 属性值
        /// </summary>
        public string ExtraValue { set; get; }
        /// <summary>
        /// 属性类型
        /// </summary>
        public string ExtraType { set; get; }
        /// <summary>
        /// 属性介绍
        /// </summary>
        public string ExtraIntro { set; get; }
        /// <summary>
        /// 排序
        /// </summary>
        public int ExtraOrder { set; get; }
        /// <summary>
        /// 外链ID
        /// </summary>
        public int ExtraOutID { set; get; }
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

    public class HouseDto
    {
        public int ID { set; get; }
        /// <summary>
        /// 房源编码
        /// </summary>
        public string House_Code { set; get; }
        /// <summary>
        /// 标题
        /// </summary>
        public string House_Title { set; get; }
        /// <summary>
        /// 图片
        /// </summary>
        public string House_Image { set; get; }
        /// <summary>
        /// 房源类型
        /// </summary>
        public string House_Type { set; get; }
        /// <summary>
        /// 床位数
        /// </summary>
        public int House_Beds { set; get; }
        /// <summary>
        /// 可住多少房客
        /// </summary>
        public int House_Tenants { set; get; }
        /// <summary>
        /// 卫生间数量
        /// </summary>
        public int House_Toilets { set; get; }
        /// <summary>
        /// 城市
        /// </summary>
        public string House_City { set; get; }
        /// <summary>
        /// 国家
        /// </summary>
        public string House_Country { set; get; }
        /// <summary>
        /// 区域
        /// </summary>
        public string House_Zone { set; get; }
        /// <summary>
        /// 省份
        /// </summary>
        public string House_Province { set; get; }
        /// <summary>
        /// 地址
        /// </summary>
        public string House_Address { set; get; }
        /// <summary>
        /// 经度
        /// </summary>
        public decimal House_Longitude { set; get; }
        /// <summary>
        /// 纬度
        /// </summary>
        public decimal House_Latitude { set; get; }
        /// <summary>
        /// 邮政编码
        /// </summary>
        public string House_Postcode { set; get; }
        /// <summary>
        /// 房屋便利设施
        /// </summary>
        public string House_Facilities { set; get; }
        /// <summary>
        /// 房屋安全设施
        /// </summary>
        public string House_SecurityFacilities { set; get; }
        /// <summary>
        /// 房客可使用区域
        /// </summary>
        public string House_AvaArea { set; get; }
        /// <summary>
        /// 简介
        /// </summary>
        public string House_Intro { set; get; }
        /// <summary>
        /// 房屋适用场景
        /// </summary>
        public string House_Scene { set; get; }
        /// <summary>
        /// 2~12岁儿童是否可以入住
        /// </summary>
        public bool House_IsChildren { set; get; }
        /// <summary>
        /// 宠物是否可以入住
        /// </summary>
        public bool House_IsPets { set; get; }
        /// <summary>
        /// 婴儿是否可以入住
        /// </summary>
        public bool House_IsBaby { set; get; }
        /// <summary>
        /// 是否允许吸烟
        /// </summary>
        public bool House_IsSmoking { set; get; }
        /// <summary>
        /// 是否允许开Party
        /// </summary>
        public bool House_IsParty { set; get; }
        /// <summary>
        /// 房客抵达前多少天通知
        /// </summary>
        public int House_ArriveNoticeDays { set; get; }
        /// <summary>
        /// 最长租多少天
        /// </summary>
        public int House_OrderDayMax { set; get; }
        /// <summary>
        /// 可以提前多少天预订
        /// </summary>
        public int House_AdvanceDays { set; get; }
        /// <summary>
        /// 每晚入住价格
        /// </summary>
        public decimal House_Price { set; get; }
        /// <summary>
        /// 周折扣
        /// </summary>
        public decimal House_DiscountWeek { set; get; }
        /// <summary>
        /// 月折扣
        /// </summary>
        public decimal House_DiscountMonth { set; get; }
        /// <summary>
        /// 发布状态
        /// </summary>
        public int House_Status { set; get; }
        /// <summary>
        /// 星级
        /// </summary>
        public int House_Grade { set; get; }
        /// <summary>
        /// 如实描述指数
        /// </summary>
        public decimal House_EvaTrue { set; get; }
        /// <summary>
        /// 交通便利指数
        /// </summary>
        public decimal House_EvaTraffic { set; get; }
        /// <summary>
        /// 卫生指数
        /// </summary>
        public decimal House_EvaHygiene { set; get; }
        /// <summary>
        /// 服务态度指数
        /// </summary>
        public decimal House_EvaAttitude { set; get; }
        /// <summary>
        /// 性价比指数
        /// </summary>
        public decimal House_EvaCost { set; get; }
        /// <summary>
        /// 入住手续方便指数
        /// </summary>
        public decimal House_EvaProcess { set; get; }
        /// <summary>
        /// 预订次数
        /// </summary>
        public int House_Orders { set; get; }
        /// <summary>
        /// 入住时间
        /// </summary>
        public string House_CheckinTime { set; get; }
        /// <summary>
        /// 退房时间
        /// </summary>
        public string House_CheckoutTime { set; get; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime House_UpdateTimes { set; get; }
    }

    public class HouseImageDto
    {
        public int ID { set; get; }
        /// <summary>
        /// 照片地址
        /// </summary>
        public string Url { set; get; }
        /// <summary>
        /// 照片类型
        /// </summary>
        public string iType { set; get; }
        /// <summary>
        /// 照片标题
        /// </summary>
        public string Title { set; get; }
    }

    public class CustomerDto
    {
        public int Customer_ID { set; get; }
        public string Customer_Name { set; get; }
        public string Customer_Company { set; get; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Customer_Tel { set; get; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string Customer_Mobile { set; get; }
        public string Customer_Email { set; get; }
        public string Customer_QQ { set; get; }
        /// <summary>
        /// 微信
        /// </summary>
        public string Customer_Weixin { set; get; }
        /// <summary>
        /// 联系地址
        /// </summary>
        public string Customer_Address { set; get; }
        /// <summary>
        /// 类型
        /// </summary>
        public string Customer_Type { set; get; }
        /// <summary>
        /// 星级
        /// </summary>
        public int Customer_Grade { set; get; }
        /// <summary>
        /// 经度
        /// </summary>
        public string Customer_Longitude { set; get; }
        /// <summary>
        /// 纬度
        /// </summary>
        public string Customer_Latitude { set; get; }
        public string Customer_Memo { set; get; }

        //[DisplayFormat(DataFormatString = "{0:s}")]
        public DateTime Customer_UpdateTime { set; get; }
        public int Member_ID { set; get; }
    }

    public class ReturnData<T>
    {
        public int code { get; set; }
        public int id { get; set; }
        public string message { get; set; }
        public T datas { get; set; }
    }

    public class NameData<T>
    {
        public string name { get; set; }
        public T data { get; set; }
    }

    public class MailDto
    {
        public string inputMailsTo { get; set; }
        public string inputMailSubject { get; set; }
        public string textBoxMailBody { get; set; }
        public string mailServer { get; set; }
        public string mailAcount { get; set; }
        public string mailPassword { get; set; }
    }

    public class APIDTO
    {
        public string CateName { get; set; }
        public List<FCKAPIDto> APIS { get; set; }
    }

    public class ShareDto
    {
        public int Share_ID { set; get; }
        /// <summary>
        /// 分享码
        /// </summary>
        public string Share_Code { set; get; }
        /// <summary>
        /// 产品编号
        /// </summary>
        public int Product_ID { set; get; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string Product_Name { set; get; }
        /// <summary>
        /// 产品图片
        /// </summary>
        public string Product_Pic { set; get; }
        /// <summary>
        /// 产品价格
        /// </summary>
        public decimal Product_Price { set; get; }
        /// <summary>
        /// 会员编号
        /// </summary>
        public int Member_ID { set; get; }
        /// <summary>
        /// 分享佣金
        /// </summary>
        public decimal Share_Money { set; get; }
        /// <summary>
        /// 分享时间
        /// </summary>
        public DateTime Share_Time { set; get; }

        public int Register_ID { set; get; }
    }

    public class V_SalesDto
    {
        public int Order_ID { set; get; }
        public string Order_Number { set; get; }
        public decimal Order_Amount { set; get; }
        public int Order_Status { set; get; }
        public string Product_Name { set; get; }
        public string Category_Name { set; get; }
        public int Register_ID { set; get; }
        public int Member_ID { set; get; }
        public string Product_Pic { set; get; }
        public DateTime Order_Time { set; get; }
        public string Order_Payway { set; get; }
        public string Member_Name { set; get; }
        public string Member_Mobile { set; get; }
        public decimal Product_Price { set; get; }
    }

    /// <summary>
    /// 交易类型枚举变量
    /// </summary>
    public enum enumFinanceType
    {
        /// <summary>
        /// 收入
        /// </summary>
        comein,
        /// <summary>
        /// 支出
        /// </summary>
        payout,
        /// <summary>
        /// 退款
        /// </summary>
        refund,
        /// <summary>
        /// 提现
        /// </summary>
        cash
    }

    /// <summary>
    /// 日志枚举类型
    /// </summary>
    public enum enumLogType
    {
        /// <summary>
        /// 系统日志
        /// </summary>
        system,
        /// <summary>
        /// 会员日志
        /// </summary>
        member,
        /// <summary>
        /// 订单日志
        /// </summary>
        order,
        /// <summary>
        /// 短信日志
        /// </summary>
        sms,
        /// <summary>
        /// 其它日志
        /// </summary>
        other
    }
}

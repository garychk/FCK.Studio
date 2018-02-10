using System;
using System.ComponentModel.DataAnnotations;

namespace FCK.Studio.Entity
{
    public class FCK_Articles : FCK_Model
    {
        [Key]
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
        /// 分类编号
        /// </summary>
        public int Article_CateID { set; get; }
        /// <summary>
        /// 点击量
        /// </summary>
        public int Article_Hits { set; get; }
        /// <summary>
        /// 作者
        /// </summary>
        public string Article_Author { set; get; }
        /// <summary>
        /// 外部链接
        /// </summary>
        public string Article_NavUrl { set; get; }
        /// <summary>
        /// 优化关键词
        /// </summary>
        public string Article_Keywords { set; get; }
        /// <summary>
        /// 优化描述
        /// </summary>
        public string Article_Description { set; get; }
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
        /// 删除标记
        /// </summary>
        public int Article_IsDel { set; get; }
        /// <summary>
        /// 推荐标记
        /// </summary>
        public int Article_IsRec { set; get; }
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
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime Article_CreateTime { set; get; }

        public FCK_Articles()
        {
            Article_UpdateTime = DateTime.Now;
            Article_CreateTime = DateTime.Now;
        }
        
    }
}

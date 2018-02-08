using System;
using System.ComponentModel.DataAnnotations;

namespace FCK.Studio.Entity
{
    public class FCK_Comments : FCK_Model
    {
        [Key]
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
        /// 父级评论
        /// </summary>
        public int Comment_ParentId { set; get; }
        /// <summary>
        /// 删除标记
        /// </summary>
        public bool IsDel { set; get; }
        /// <summary>
        /// 删除标记
        /// </summary>
        public bool IsTop { set; get; }
        /// <summary>
        /// 删除标记
        /// </summary>
        public bool IsActive { set; get; }
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
}

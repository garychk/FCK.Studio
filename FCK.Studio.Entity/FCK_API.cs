using System.ComponentModel.DataAnnotations;

namespace FCK.Studio.Entity
{
    public class FCK_API
    {
        [Key]
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
}

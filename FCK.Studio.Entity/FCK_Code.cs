using System.ComponentModel.DataAnnotations;

namespace FCK.Studio.Entity
{
    public class FCK_Code
    {
        [Key]
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
}

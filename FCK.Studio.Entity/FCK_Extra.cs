using System.ComponentModel.DataAnnotations;

namespace FCK.Studio.Entity
{
    public class FCK_Extra : FCK_Model
    {
        [Key] 
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
}

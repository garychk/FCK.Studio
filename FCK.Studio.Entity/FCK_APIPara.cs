using System.ComponentModel.DataAnnotations;

namespace FCK.Studio.Entity
{
    public class FCK_APIPara
    {
        [Key]
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
}

using System.ComponentModel.DataAnnotations;

namespace FCK.Studio.Entity
{
    public class FCK_Groups : FCK_Model
    {
        [Key]
        public int Group_ID { set; get; }
        /// <summary>
        /// 分组名称
        /// </summary>
        public string Group_Name { set; get; }
        /// <summary>
        /// 分组索引
        /// </summary>
        public string Group_Index { set; get; }
        /// <summary>
        /// 折扣
        /// </summary>
        public double Group_Discount { set; get; }
        /// <summary>
        /// 积分
        /// </summary>
        public int Group_Points { set; get; }
        /// <summary>
        /// 
        /// </summary>
        public int Group_ParentId { set; get; }
    }
}

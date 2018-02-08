using System.ComponentModel.DataAnnotations;

namespace FCK.Studio.Entity
{
    public class FCK_Category : FCK_Model
    {
        [Key]
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
    }
}

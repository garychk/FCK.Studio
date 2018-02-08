using System.ComponentModel.DataAnnotations;

namespace FCK.Studio.Entity
{
    public class FCK_HouseImages
    {
        [Key]
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
        /// <summary>
        /// 房屋代码
        /// </summary>
        public string House_Code { set; get; }
    }
}

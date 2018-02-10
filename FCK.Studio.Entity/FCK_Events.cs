using System;
using System.ComponentModel.DataAnnotations;

namespace FCK.Studio.Entity
{
    public class FCK_Events : FCK_Model
    {
        [Key]
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
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss}")]
        public DateTime Event_StartTime { set; get; }
        /// <summary>
        /// 结束时间
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss}")]
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
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss}")]
        public DateTime Event_TakeTime { set; get; }
        /// <summary>
        /// 事务完成时间
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss}")]
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
}

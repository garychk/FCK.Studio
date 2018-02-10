using System;
using System.ComponentModel.DataAnnotations;

namespace FCK.Studio.Entity
{
    public class FCK_Log : FCK_Model
    {
        [Key]
        public int Log_ID { set; get; }
        /// <summary>
        /// 日志内容
        /// </summary>
        public string Log_Content { set; get; }
        /// <summary>
        /// 日志类型
        /// </summary>
        public string Log_Type { set; get; }
        /// <summary>
        /// 日志状态
        /// </summary>
        public int Log_Status { set; get; }
        /// <summary>
        /// 日志时间
        /// </summary>
        public DateTime Log_Time { set; get; }
        public int Member_ID { set; get; }
    }
}

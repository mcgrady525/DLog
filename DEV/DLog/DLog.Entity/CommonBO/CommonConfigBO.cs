using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace DLog.Entity.CommonBO
{
    [Serializable,DataContract]
    public class CommonConfigBO
    {
        [DataMember]
        public CommonConfigBase ErrorLog { get; set; }
        [DataMember]
        public CommonConfigBase DebugLog { get; set; }
        [DataMember]
        public PerfLogConfig PerfLog { get; set; }
    }

    [Serializable,DataContract]
    public class CommonConfigBase
    {
        /// <summary>
        /// 日志系统最大接收队列数量
        /// </summary>
        [Display(Name = "日志系统最大接收队列数量")]
        [DataMember]
        [Required]
        public long MaxReceiveCount { get; set; }

        /// <summary>
        /// 日志系统最大提交数据库数量
        /// </summary>
        [DataMember]
        [Display(Name = "日志系统最大提交数据库数量")]
        [Required]
        public int MaxPostCount { get; set; }

        /// <summary>
        /// 每次间隔多长执行一次插入数据库 ms
        /// </summary>
        [DataMember]
        [Display(Name = "每次间隔多长执行一次插入数据库(ms)")]
        [Required]
        public int InsertCycleTime { get; set; }

        /// <summary>
        /// 是否启动
        /// </summary>
        [DataMember]
        [Display(Name = "是否启动")]
        [Required]
        public bool IsEnabled { get; set; }
    }

    [Serializable, DataContract]
    public class PerfLogConfig : CommonConfigBase
    {
        /// <summary>
        /// 执行时间多长才写入数据库
        /// </summary>
        [DataMember]
        [Display(Name = "执行时间多长才写入数据库")]
        [Required]
        public int Duration { get; set; }
    }
}

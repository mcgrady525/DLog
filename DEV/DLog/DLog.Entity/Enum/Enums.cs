using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace DLog.Entity.Enum
{
    /// <summary>
    /// 日志类型
    /// </summary>
    [DataContract]
    public enum LogType: byte
    {
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "错误日志")]
        [EnumMember]
        Error = 1,

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "调试日志")]
        [EnumMember]
        Debug = 2
    }

    /// <summary>
    /// 资源类别
    /// </summary>
    [Description("资源类别")]
    [DataContract]
    public enum ResourceType: byte
    {
        /// <summary>
        ///  SystemCode
        /// </summary>
        [EnumMember]
        [Display(Name = "SystemCode")]
        SystemCode = 1,

        /// <summary>
        /// Source
        /// </summary>
        [EnumMember]
        [Display(Name = "Source")]
        Source = 2,

        /// <summary>
        /// ClassName
        /// </summary>
        [EnumMember]
        [Display(Name = "ClassName")]
        ClassName = 3,

        /// <summary>
        /// MethodName
        /// </summary>
        [Display(Name = "MethodName")]
        [EnumMember]
        MethodName = 4,
    }


    /// <summary>
    /// 提示类别
    /// </summary>
    [Description("提示类别")]
    [DataContract]
    public enum SearchInfoType: byte
    {
        /// <summary>
        ///  PerfLog
        /// </summary>
        [EnumMember]
        [Display(Name = "PerfLog")]
        PerfLog = 1,

        /// <summary>
        /// XmlLog
        /// </summary>
        [EnumMember]
        [Display(Name = "XmlLog")]
        XmlLog = 2,

        /// <summary>
        /// ErrorLog
        /// </summary>
        [EnumMember]
        [Display(Name = "ErrorLog")]
        ErrorLog = 3,

        /// <summary>
        /// DebugLog
        /// </summary>
        [EnumMember]
        [Display(Name = "DebugLog")]
        DebugLog = 4
    }
}

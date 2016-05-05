using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace DLog.Entity.Metadata
{
    /// <summary>
    /// ErrorLog
    /// </summary>
    [DataContract]
    public class ErrorLogMetadata : ErrorLogMetadataBase
    {
        /// <summary>
        /// 重新设置创建时间格式
        /// </summary>
        [Required]
        [DataType(DataType.Date)]
        [UIHint("DatePicker")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        [DataMember]
        public new object CreateTime { get; set; }
    }

    /// <summary>
    /// DebugLogMetadata
    /// </summary>
    [DataContract]
    public class DebugLogMetadata : DebugLogMetadataBase
    {
        /// <summary>
        /// 重新设置创建时间格式
        /// </summary>
        [Required]
        [DataType(DataType.Date)]
        [UIHint("DatePicker")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        [DataMember]
        public new object CreateTime { get; set; }
    }

    /// <summary>
    /// PerfLog
    /// </summary>
    [DataContract]
    public class PerfLogMetadata : PerfLogMetadataBase
    {
        /// <summary>
        /// 重新设置创建时间格式
        /// </summary>
        [Required]
        [DataType(DataType.Date)]
        [UIHint("DatePicker")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        [DataMember]
        public new object CreateTime { get; set; }
    }

    /// <summary>
    /// SearchInfo
    /// </summary>
    [DataContract]
    public class SearchInfoMetadata : SearchInfoMetadataBase
    {

    }

    /// <summary>
    /// CommonConfig
    /// </summary>
    [DataContract]
    public class CommonConfigMetadata : CommonConfigMetadataBase
    {

    }

    /// <summary>
    /// PerfLogSearchInfo
    /// </summary>
    [DataContract]
    public class PerfLogSearchInfoMetadata : PerfLogSearchInfoMetadataBase
    {

    }

    /// <summary>
    /// Resource
    /// </summary>
    [DataContract]
    public class ResourceMetadata : ResourceMetadataBase
    {

    }
}

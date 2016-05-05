using DLog.Entity.CommonBO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace DLog.Entity.ViewModel
{
    [DataContract(IsReference = true)]
    [Serializable]
    public class SearchPerfLogListRQ : PagingBase
    {
        [Display(Name = "ID")]
        [DataMember]
        public long? ID { get; set; }

        [Display(Name = "StartTime")]
        [DataType(DataType.Date)]
        [UIHint("DatePicker")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        [DataMember]
        public DateTime? StartTime { get; set; }

        [Display(Name = "EndTime")]
        [DataType(DataType.Date)]
        [UIHint("DatePicker")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        [DataMember]
        public DateTime? EndTime { get; set; }

        [Display(Name = "SystemCode")]
        [DataMember]
        public string SystemCode { get; set; }

        [Display(Name = "Source")]
        [DataMember]
        public string Source { get; set; }

        [Display(Name = "ClassName")]
        [DataMember]
        public string ClassName { get; set; }

        [Display(Name = "MethodName")]
        [DataMember]
        public string MethodName { get; set; }

        [Display(Name = "DurationMinValue")]
        [DataMember]
        public int? DurationMinValue { get; set; }

        [Display(Name = "DurationMaxValue")]
        [DataMember]
        public int? DurationMaxValue { get; set; }

        [Display(Name = "Remark")]
        [DataMember]
        public string Remark { get; set; }
    }
}

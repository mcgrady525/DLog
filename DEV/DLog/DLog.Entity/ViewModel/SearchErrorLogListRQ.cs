using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using DLog.Entity.CommonBO;
using System.ComponentModel.DataAnnotations;

namespace DLog.Entity.ViewModel
{
    [DataContract(IsReference = true)]
    [Serializable]
    public class SearchErrorLogListRQ : PagingBase
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
        [UIHint("SystemCodeSelector")]
        [DataMember]
        public string SystemCode { get; set; }

        [Display(Name = "Source")]
        [UIHint("SourceSelector")]
        [DataMember]
        public string Source { get; set; }

        [Display(Name = "AppDomainName")]
        [DataMember]
        public string AppDomainName { get; set; }

        [Display(Name = "MachineName")]
        [DataMember]
        public string MachineName { get; set; }

        [Display(Name = "IP")]
        [DataMember]
        public string IpAddress { get; set; }

        [Display(Name = "Message")]
        [DataMember]
        public string Message { get; set; }

        [Display(Name = "ProcessName")]
        [DataMember]
        public string ProcessName { get; set; }

        [Display(Name = "Detail")]
        [DataMember]
        public string Detail { get; set; }

    }
}

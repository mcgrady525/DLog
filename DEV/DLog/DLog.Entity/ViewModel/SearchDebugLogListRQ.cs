using DLog.Entity.CommonBO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace DLog.Entity.ViewModel
{
    [DataContract(IsReference = true)]
    [Serializable]
    public class SearchDebugLogListRQ : SearchErrorLogListRQ
    {

    }
}

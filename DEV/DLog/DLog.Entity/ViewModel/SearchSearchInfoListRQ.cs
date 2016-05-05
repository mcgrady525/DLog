using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace DLog.Entity.ViewModel
{
    /// <summary>
    /// 查询search info的request
    /// </summary>
    [DataContract(IsReference = true)]
    [Serializable]
    public class SearchSearchInfoListRQ: SearchPerfLogListRQ
    {
    }
}

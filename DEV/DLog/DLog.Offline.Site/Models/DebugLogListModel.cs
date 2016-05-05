using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DLog.Entity;
using Webdiyer.WebControls.Mvc;
using DLog.Entity.ViewModel;

namespace DLog.Offline.Site.Models
{
    public class DebugLogListModel
    {
        /// <summary>
        /// Request
        /// </summary>
        public SearchDebugLogListRQ Request { get; set; }

        /// <summary>
        /// Response
        /// </summary>
        public PagedList<DebugLog> QueryResult { get; set; }
    }
}
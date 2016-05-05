using DLog.Entity;
using DLog.Entity.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Webdiyer.WebControls.Mvc;

namespace DLog.Offline.Site.Models
{
    public class PerfLogListModel
    {
        /// <summary>
        /// Request
        /// </summary>
        public SearchPerfLogListRQ Request { get; set; }

        /// <summary>
        /// Response
        /// </summary>
        public PagedList<PerfLog> QueryResult { get; set; }
    }
}
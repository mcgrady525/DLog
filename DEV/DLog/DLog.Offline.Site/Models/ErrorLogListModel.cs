using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DLog.Entity;
using DLog.Entity.ViewModel;
using Webdiyer.WebControls.Mvc;

namespace DLog.Offline.Site.Models
{
    public class ErrorLogListModel
    {
        /// <summary>
        /// Request
        /// </summary>
        public SearchErrorLogListRQ Request { get; set; }

        /// <summary>
        /// Response
        /// </summary>
        public PagedList<ErrorLog> QueryResult { get; set; }
    }
}
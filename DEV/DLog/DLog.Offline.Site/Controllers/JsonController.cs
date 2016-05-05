using DLog.Entity.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DLog.Offline.Site.Models;
using DLog.Entity.ViewModel;

namespace DLog.Offline.Site.Controllers
{
    public class JsonController : BaseController
    {
        public JsonResult GetSystemCodeResult(string systemCode, string source, string className, string methodName, SearchInfoType searchInfoType)
        {
            var model = new PerfLogListModel();
            if (model.Request == null)
            {
                model.Request = new SearchPerfLogListRQ();
                model.Request.SystemCode = systemCode;
                model.Request.Source = source;
                model.Request.ClassName = className;
                model.Request.MethodName = methodName;
            }

            //DebugLog和ErrorLog从表SearchInfo中取,PerfLog从表PerfLogSearchInfo中取


            //var result = LogAccessor.GetSystemCodeList(model.Request, searchInfoType);
            return Json(null, JsonRequestBehavior.AllowGet);
        }
    }
}

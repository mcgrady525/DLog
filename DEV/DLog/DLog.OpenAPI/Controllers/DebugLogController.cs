using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DLog.Entity;
using DLog.OpenAPI.Models;
using DLog.OpenAPI.Helpers;
using DLog.OpenAPI.Unity;

namespace DLog.OpenAPI.Controllers
{
    /// <summary>
    /// 调试日志
    /// </summary>
    [RoutePrefix("api/DebugLog")]
    public class DebugLogController : BaseApiController
    {
        [Route("AddLog")]
        [HttpPost]
        public HttpResponseMessage AddLog(List<DebugLog> request)
        {
            var result = new CommonBO<bool>
            {
                head = new Header { auth = string.Empty, errcode = 0 },
                data = false
            };

            DebugLogTaskManager.Enqueue(request);
            result.data = true;

            return result.ToJsonResult();
        }
    }
}

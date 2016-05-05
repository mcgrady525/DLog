using DLog.Entity;
using DLog.OpenAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DLog.OpenAPI.Helpers;

namespace DLog.OpenAPI.Controllers
{
    /// <summary>
    /// 性能日志
    /// </summary>
    [RoutePrefix("api/PerfLog")]
    public class PerfLogController : BaseApiController
    {
        [Route("AddLog")]
        [HttpPost]
        public HttpResponseMessage AddLog(List<PerfLog> request)
        {
            var result = new CommonBO<bool>
            {
                head = new Header { auth = string.Empty, errcode = 0 },
                data = false
            };

            //PerfLogTaskManager.Enqueue(request);
            result.data = true;

            return result.ToJsonResult();
        }
    }
}

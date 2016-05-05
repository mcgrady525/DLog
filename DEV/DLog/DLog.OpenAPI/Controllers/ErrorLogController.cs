using DLog.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DLog.OpenAPI.Models;
using DLog.OpenAPI.Helpers;
using DLog.OpenAPI.Unity;
using DLog.Entity.CommonBO;
using System.ServiceModel;
using DLog.IService;

namespace DLog.OpenAPI.Controllers
{
    /// <summary>
    /// 错误日志
    /// </summary>
    [RoutePrefix("api/ErrorLog")]
    public class ErrorLogController : BaseApiController
    {
        [Route("AddLog")]
        [HttpPost]
        public HttpResponseMessage AddLog(List<ErrorLog> request)
        {
            var result = new CommonBO<bool> 
            {
                head = new Header { auth = string.Empty, errcode = 0 },
                data = false
            };

            ErrorLogTaskManager.Enqueue(request);
            result.data = true;

            return result.ToJsonResult();
        }

    }
}

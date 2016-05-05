using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using DLog.Entity.CommonBO;
using DLog.Entity;
using Tmac.Frameworks.Common.Result;
using DLog.Entity.ViewModel;

namespace DLog.IService
{
    [ServiceContract(ConfigurationName = "DLogPerfLogService.IDLogPerfLogService")]
    public interface IDLogPerfLogService
    {
        /// <summary>
        /// 批量插入性能日志
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        [OperationContract]
        DLogResult<bool> BatchInsert(List<PerfLog> list);

        /// <summary>
        /// 按条件查询性能日志
        /// </summary>
        /// <param name="rq">请求rq</param>
        /// <returns>分页后的性能日志列表</returns>
        [OperationContract]
        PagingResult<PerfLog> SearchPerfLogList(SearchPerfLogListRQ rq);

        /// <summary>
        /// 依据id查询性能日志
        /// </summary>
        /// <param name="id">日志id</param>
        /// <returns></returns>
        [OperationContract]
        DLogResult<PerfLog> SearchPerfLogByID(long id);
    }
}

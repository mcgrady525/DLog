using DLog.Entity.CommonBO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using DLog.Entity;
using DLog.Entity.ViewModel;
using Tmac.Frameworks.Common.Result;

namespace DLog.IService
{
    [ServiceContract(ConfigurationName = "DLogDebugLogService.IDLogDebugLogService")]
    public interface IDLogDebugLogService
    {
        /// <summary>
        /// 批量插入调试日志
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        [OperationContract]
        DLogResult<bool> BatchInsert(List<DebugLog> list);

        /// <summary>
        /// 按条件查询调试日志
        /// </summary>
        /// <param name="rq">请求rq</param>
        /// <returns>分页后的调试日志列表</returns>
        [OperationContract]
        PagingResult<DebugLog> SearchDebugLogList(SearchDebugLogListRQ rq);

        /// <summary>
        /// 依据id查询调试日志
        /// </summary>
        /// <param name="id">日志id</param>
        /// <returns></returns>
        [OperationContract]
        DLogResult<DebugLog> SearchDebugLogByID(long id);
    }
}

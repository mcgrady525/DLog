using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using DLog.Entity.CommonBO;
using DLog.Entity;
using DLog.Entity.ViewModel;
using Tmac.Frameworks.Common.Result;

namespace DLog.IService
{
    [ServiceContract(ConfigurationName = "DLogErrorLogService.IDLogErrorLogService")]
    public interface IDLogErrorLogService
    {
        /// <summary>
        /// 批量插入错误日志
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        [OperationContract]
        DLogResult<bool> BatchInsert(List<ErrorLog> list);

        /// <summary>
        /// 按条件查询错误日志
        /// </summary>
        /// <param name="rq">请求rq</param>
        /// <returns>分页后的错误日志列表</returns>
        [OperationContract]
        PagingResult<ErrorLog> SearchErrorLogList(SearchErrorLogListRQ rq);

        /// <summary>
        /// 依据id查询错误日志
        /// </summary>
        /// <param name="id">日志id</param>
        /// <returns></returns>
        [OperationContract]
        DLogResult<ErrorLog> SearchErrorLogByID(long id);

    }
}

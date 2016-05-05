using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DLog.IService;
using System.ServiceModel.Activation;
using System.ServiceModel;
using DLog.Entity.CommonBO;
using DLog.Entity;
using DLog.Data;
using DLog.Entity.ViewModel;
using Tmac.Frameworks.Log;
using Tmac.Frameworks.Common.Result;
using Tmac.Frameworks.Common.Extends;

namespace DLog.Service
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    [WcfServiceCounter(SystemCode = "DLog", Source = "Offline.Service")]
    public class DLogDebugLogService : IDLogDebugLogService
    {
        /// <summary>
        /// 批量插入调试日志
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public DLogResult<bool> BatchInsert(List<DebugLog> list)
        {
            var result = new DLogResult<bool>
            {
                ReturnCode = Entity.ReturnCodeType.Error
            };

            using (var db = new DLogDB())
            {
                db.DebugLog.AddRange(list);
                if (db.SaveChanges() > 0)
                {
                    result.Content = true;
                    result.ReturnCode = ReturnCodeType.Success;
                }
            }

            return result;
        }

        /// <summary>
        /// 按条件查询调试日志
        /// </summary>
        /// <param name="rq">请求rq</param>
        /// <returns>分页后的调试日志列表</returns>
        public PagingResult<DebugLog> SearchDebugLogList(SearchDebugLogListRQ rq)
        {
            var result = new PagingResult<DebugLog>();
            DLog.Common.Helper.DBHelper.NoLockInvokeDB(() =>
            {
                using (var db = new DLogDB())
                {
                    //条件
                    var query = db.DebugLog.AsQueryable();
                    if (rq.ID.HasValue)
                    {
                        query = query.Where(p => p.ID == rq.ID);
                    }
                    if (rq.StartTime.HasValue)
                    {
                        query = query.Where(p => p.CreateTime >= rq.StartTime);
                    }
                    if (rq.EndTime.HasValue)
                    {
                        query = query.Where(p => p.CreateTime <= rq.EndTime);
                    }
                    if (!rq.SystemCode.IsNullOrEmpty())
                    {
                        query = query.Where(p => p.SystemCode.Equals(rq.SystemCode));
                    }
                    if (!rq.Source.IsNullOrEmpty())
                    {
                        query = query.Where(p => p.Source.Equals(rq.Source));
                    }
                    if (!rq.AppDomainName.IsNullOrEmpty())
                    {
                        query = query.Where(p => p.AppDomainName.Equals(rq.AppDomainName));
                    }
                    if (!rq.MachineName.IsNullOrEmpty())
                    {
                        query = query.Where(p => p.MachineName.Equals(rq.MachineName));
                    }
                    if (!rq.IpAddress.IsNullOrEmpty())
                    {
                        query = query.Where(p => p.IpAddress.Equals(rq.IpAddress));
                    }
                    if (!rq.Message.IsNullOrEmpty())
                    {
                        query = query.Where(p => p.Message.Equals(rq.Message));
                    }
                    if (!rq.ProcessName.IsNullOrEmpty())
                    {
                        query = query.Where(p => p.ProcessName.Equals(rq.ProcessName));
                    }

                    //排序
                    query = query.OrderByDescending(p => p.CreateTime);
                    result = query.Paging(rq.PageIndex, rq.PageSize);
                }
            });

            return result;
        }

        /// <summary>
        /// 依据id查询调试日志
        /// </summary>
        /// <param name="id">日志id</param>
        /// <returns></returns>
        public DLogResult<DebugLog> SearchDebugLogByID(long id)
        {
            var result = new DLogResult<DebugLog>
            {
                Content = new DebugLog(),
                ReturnCode = ReturnCodeType.InterfaceError
            };

            DLog.Common.Helper.DBHelper.NoLockInvokeDB(() =>
            {
                using (var db = new DLogDB())
                {
                    result.Content = db.DebugLog.FirstOrDefault(p => p.ID == id);
                    result.ReturnCode = ReturnCodeType.Success;
                }
            });

            return result;
        }
    }
}

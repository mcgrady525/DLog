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
    public class DLogPerfLogService : IDLogPerfLogService
    {
        /// <summary>
        /// 批量插入性能日志
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public DLogResult<bool> BatchInsert(List<PerfLog> list)
        {
            var result = new DLogResult<bool>
            {
                ReturnCode = Entity.ReturnCodeType.Error
            };

            using (var db = new DLogDB())
            {
                db.PerfLog.AddRange(list);
                if (db.SaveChanges() > 0)
                {
                    result.Content = true;
                    result.ReturnCode = ReturnCodeType.Success;
                }
            }

            return result;
        }

        /// <summary>
        /// 按条件查询性能日志
        /// </summary>
        /// <param name="rq">请求rq</param>
        /// <returns>分页后的性能日志列表</returns>
        public PagingResult<PerfLog> SearchPerfLogList(SearchPerfLogListRQ rq)
        {
            var result = new PagingResult<PerfLog>();
            DLog.Common.Helper.DBHelper.NoLockInvokeDB(() =>
            {
                using (var db = new DLogDB())
                {
                    var query = db.PerfLog.AsQueryable();
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
                    if (!rq.ClassName.IsNullOrEmpty())
                    {
                        query = query.Where(p => p.ClassName.Equals(rq.ClassName));
                    }
                    if (!rq.MethodName.IsNullOrEmpty())
                    {
                        query = query.Where(p => p.MethodName.Equals(rq.MethodName));
                    }
                    if (rq.DurationMinValue.HasValue)
                    {
                        query = query.Where(p => p.Duration >= rq.DurationMinValue.Value);
                    }
                    if (rq.DurationMaxValue.HasValue)
                    {
                        query = query.Where(p => p.Duration <= rq.DurationMaxValue.Value);
                    }
                    if (!rq.Remark.IsNullOrEmpty())
                    {
                        query = query.Where(p => p.Remark.Contains(rq.Remark));
                    }

                    //排序
                    query = query.OrderByDescending(p => p.CreateTime);
                    result = query.Paging(rq.PageIndex, rq.PageSize);
                }
            });

            return result;
        }

        /// <summary>
        /// 依据id查询性能日志
        /// </summary>
        /// <param name="id">日志id</param>
        /// <returns></returns>
        public DLogResult<PerfLog> SearchPerfLogByID(long id)
        {
            var result = new DLogResult<PerfLog>
            {
                Content = new PerfLog(),
                ReturnCode = ReturnCodeType.InterfaceError
            };

            DLog.Common.Helper.DBHelper.NoLockInvokeDB(() =>
            {
                using (var db = new DLogDB())
                {
                    result.Content = db.PerfLog.FirstOrDefault(p => p.ID == id);
                    result.ReturnCode = ReturnCodeType.Success;
                }
            });

            return result;
        }
    }
}

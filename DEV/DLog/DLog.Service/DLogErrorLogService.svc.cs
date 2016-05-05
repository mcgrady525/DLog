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
using Tmac.Frameworks.Translation.Translate;

namespace DLog.Service
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    [WcfServiceCounter(SystemCode = "DLog", Source = "Offline.Service")]
    [Translate(TranslationType = TranslationType.SimplifiedToTraditional)]//简繁转换
    public class DLogErrorLogService : IDLogErrorLogService
    {
        /// <summary>
        /// 批量插入错误日志
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public DLogResult<bool> BatchInsert(List<ErrorLog> list)
        {
            var result = new DLogResult<bool>
            {
                ReturnCode = ReturnCodeType.Error
            };

            using (var db = new DLogDB())
            {
                db.ErrorLog.AddRange(list);
                if (db.SaveChanges() > 0)
                {
                    result.Content = true;
                    result.ReturnCode = ReturnCodeType.Success;
                }
            }

            return result;
        }

        /// <summary>
        /// 按条件查询错误日志
        /// </summary>
        /// <param name="rq">请求rq</param>
        /// <returns>分页后的错误日志列表</returns>
        public PagingResult<ErrorLog> SearchErrorLogList(SearchErrorLogListRQ rq)
        {
            var result = new PagingResult<ErrorLog>();
            DLog.Common.Helper.DBHelper.NoLockInvokeDB(() => 
            {
                using (var db = new DLogDB())
                {
                    //条件
                    var query = db.ErrorLog.AsQueryable();
                    if (rq.ID.HasValue)
                    {
                        query = query.Where(p=> p.ID== rq.ID);
                    }
                    if (rq.StartTime.HasValue)
                    {
                        query = query.Where(p=> p.CreateTime>= rq.StartTime);
                    }
                    if (rq.EndTime.HasValue)
                    {
                        query = query.Where(p=> p.CreateTime<= rq.EndTime);
                    }
                    if (!rq.SystemCode.IsNullOrEmpty())
                    {
                        query = query.Where(p=> p.SystemCode.Equals(rq.SystemCode));
                    }
                    if (!rq.Source.IsNullOrEmpty())
                    {
                        query = query.Where(p=> p.Source.Equals(rq.Source));
                    }
                    if (!rq.AppDomainName.IsNullOrEmpty())
                    {
                        query = query.Where(p=> p.AppDomainName.Equals(rq.AppDomainName));
                    }
                    if (!rq.MachineName.IsNullOrEmpty())
                    {
                        query = query.Where(p=> p.MachineName.Equals(rq.MachineName));
                    }
                    if (!rq.IpAddress.IsNullOrEmpty())
                    {
                        query = query.Where(p=> p.IpAddress.Equals(rq.IpAddress));
                    }
                    if (!rq.Message.IsNullOrEmpty())
                    {
                        query = query.Where(p=> p.Message.Equals(rq.Message));
                    }
                    if (!rq.ProcessName.IsNullOrEmpty())
                    {
                        query = query.Where(p=> p.ProcessName.Equals(rq.ProcessName));
                    }

                    //排序
                    query = query.OrderByDescending(p=> p.CreateTime);
                    result = query.Paging(rq.PageIndex, rq.PageSize);
                }
            });

            return result;
        }

        /// <summary>
        /// 依据id查询错误日志
        /// </summary>
        /// <param name="id">日志id</param>
        /// <returns></returns>
        public DLogResult<ErrorLog> SearchErrorLogByID(long id)
        {
            var result = new DLogResult<ErrorLog>
            {
                Content= new ErrorLog(),
                ReturnCode = ReturnCodeType.Error
            };

            DLog.Common.Helper.DBHelper.NoLockInvokeDB(() => 
            {
                using (var db = new DLogDB())
                {
                    result.Content= db.ErrorLog.FirstOrDefault(p=> p.ID== id);
                    result.ReturnCode = ReturnCodeType.Success;
                }
            });

            return result;
        }

    }
}

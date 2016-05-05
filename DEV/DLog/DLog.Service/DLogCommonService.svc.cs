using DLog.Entity.CommonBO;
using DLog.IService;
using DLog.Common.Helper;
using DLog.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using Tmac.Frameworks.Log;
using Tmac.Frameworks.Common.Extends;
using DLog.Entity;
using DLog.Entity.Enum;
using DLog.Entity.ViewModel;

namespace DLog.Service
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    [WcfServiceCounter(SystemCode = "DLog", Source = "Offline.Service")]
    public class DLogCommonService : IDLogCommonService
    {
        /// <summary>
        /// 获取公共配置
        /// </summary>
        /// <returns></returns>
        public DLogResult<CommonConfigBO> GetCommonConfig()
        {
            var result = new DLogResult<CommonConfigBO>
            {
                ReturnCode = Entity.ReturnCodeType.Error
            };
            DLog.Common.Helper.DBHelper.NoLockInvokeDB(() =>
            {
                using (var db = new DLogDB())
                {
                    var commonConfig = db.CommonConfig.Single();
                    if (commonConfig != null)
                    {
                        result.Content = commonConfig.Content.FromJson<CommonConfigBO>();
                        result.ReturnCode = Entity.ReturnCodeType.Success;
                    }
                }
            });

            return result;
        }

        /// <summary>
        /// 初始化公共配置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public DLogResult<bool> InitCommonConfig(CommonConfigBO model)
        {
            var result = new DLogResult<bool>
            {
                Content = false,
                ReturnCode = Entity.ReturnCodeType.Error
            };

            using (var db = new DLogDB())
            {
                var commonConfigs = db.CommonConfig.ToList();
                if (commonConfigs.HasValue())
                {
                    db.CommonConfig.RemoveRange(commonConfigs);
                }

                db.CommonConfig.Add(new DLog.Entity.CommonConfig
                {
                    Content = model.ToJson(),
                    LastUpdateUser = "system",
                    LastUpdateTime = DateTime.Now
                });
                if (db.SaveChanges() > 0)
                {
                    result.Content = true;
                    result.ReturnCode = Entity.ReturnCodeType.Success;
                }
            }

            return result;
        }

        /// <summary>
        /// 刷新search info
        /// </summary>
        /// <returns></returns>
        public DLogResult<bool> RefreshSearchInfo()
        {
            var result = new DLogResult<bool>
            {
                Content = false,
                ReturnCode = Entity.ReturnCodeType.Error
            };

            using (var db = new DLogDB())
            {
                //先清除所有数据,然后从error log中查询出SystemCode和Source插入
                var all = db.SearchInfo.AsQueryable().ToList();
                if (all.HasValue())
                {
                    db.SearchInfo.RemoveRange(all);
                }

                //使用查询表达式写法
                //var temp = (from p in db.ErrorLog
                //            select new
                //            {
                //                SystemCode = p.SystemCode,
                //                Source = p.Source
                //            }).Distinct().ToList();
                var temp = db.ErrorLog.Select(p => new { SystemCode = p.SystemCode, Source = p.Source }).Distinct().ToList();
                var tempDebug = db.DebugLog.Select(p => new { SystemCode = p.SystemCode, Source = p.Source }).Distinct().ToList();
                temp.AddRange(tempDebug);
                var temp1 = temp.DistinctBy(p => new { p.SystemCode, p.Source }).OrderBy(p => p.SystemCode).ThenBy(q => q.Source).ToList();
                var searchInfos = temp1.ConvertAll(p => new SearchInfo { SystemCode = p.SystemCode, Source = p.Source });
                db.SearchInfo.AddRange(searchInfos);
                if (db.SaveChanges() > 0)
                {
                    result.Content = true;
                    result.ReturnCode = ReturnCodeType.Success;
                }
            }

            return result;
        }

        /// <summary>
        /// 依据searchInfoType查询SystemCode list
        /// </summary>
        /// <param name="searchInfoType"></param>
        /// <returns></returns>
        public DLogResult<List<string>> SearchSystemCodeList(SearchInfoType searchInfoType)
        {
            var result = new DLogResult<List<string>>
            {
                Content= new List<string>(),
                ReturnCode = Entity.ReturnCodeType.Error
            };

            using (var db = new DLogDB())
            {
                switch (searchInfoType)
                {
                    case SearchInfoType.PerfLog:
                        //result.Content = GetPefLogSystemCodeList(searchInfoType);
                        break;
                    //case SearchInfoType.XmlLog:
                    case SearchInfoType.ErrorLog:
                    case SearchInfoType.DebugLog:
                        //result.Content = GetErrorLogSystemCodeList(searchInfoType);
                        break;
                    default:
                        break;
                }
            }

            return result;
        }

        /// <summary>
        /// 依据查询条件和searchInfoType查询source list
        /// </summary>
        /// <param name="request"></param>
        /// <param name="searchInfoType"></param>
        /// <returns></returns>
        public DLogResult<List<string>> SearchSourceList(SearchSearchInfoListRQ request, SearchInfoType searchInfoType)
        {
            var result = new DLogResult<List<string>>
            {
                Content = new List<string>(),
                ReturnCode = Entity.ReturnCodeType.Error
            };

            return result;
        }

        /// <summary>
        /// 依据查询条件和searchInfoType查询className list
        /// </summary>
        /// <param name="request"></param>
        /// <param name="searchInfoType"></param>
        /// <returns></returns>
        public DLogResult<List<string>> SearchClassNameList(SearchSearchInfoListRQ request, SearchInfoType searchInfoType)
        {
            var result = new DLogResult<List<string>>
            {
                Content = new List<string>(),
                ReturnCode = Entity.ReturnCodeType.Error
            };

            return result;
        }

        /// <summary>
        /// 依据查询条件和searchInfoType查询methodName list
        /// </summary>
        /// <param name="request"></param>
        /// <param name="searchInfoType"></param>
        /// <returns></returns>
        public DLogResult<List<string>> SearchMethodNameList(SearchSearchInfoListRQ request, SearchInfoType searchInfoType)
        {
            var result = new DLogResult<List<string>>
            {
                Content = new List<string>(),
                ReturnCode = Entity.ReturnCodeType.Error
            };

            return result;
        }

        #region Private

        private object GetErrorLogSystemCodeList(SearchInfoType searchInfoType)
        {
            var result = new object();

            //从seachInfo表中取
            DLog.Common.Helper.DBHelper.NoLockInvokeDB(() => 
            {
                using (var db = new DLogDB())
                {
                    var systemCodes= db.SearchInfo.Select(p => p.SystemCode).Distinct().ToList();
                    var s2 = db.SearchInfo.Select(p => p.SystemCode).GroupBy(p => p).ToDictionary(p => p.Key, p => p);

                }
            });


            return result;
        }

        #endregion
    }
}

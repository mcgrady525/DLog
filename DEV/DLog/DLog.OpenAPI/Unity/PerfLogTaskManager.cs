using DLog.Common.Helper;
using DLog.Entity;
using DLog.Entity.CommonBO;
using DLog.Entity.Enum;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Tmac.Frameworks.Log;
using DLog.OpenAPI.Helpers;
using DLog.IService;

namespace DLog.OpenAPI.Unity
{
    public class PerfLogTaskManager
    {
        internal static ConcurrentQueue<PerfLog> Queue = new ConcurrentQueue<PerfLog>();
        internal static ConcurrentDictionary<string, string> SystemCodeDict = new ConcurrentDictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        internal static ConcurrentDictionary<string, string> SourceDict = new ConcurrentDictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        internal static ConcurrentDictionary<string, string> ClassNameDict = new ConcurrentDictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        internal static ConcurrentDictionary<string, string> MethodNameDict = new ConcurrentDictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        static PerfLogTaskManager()
        {
            SystemCodeDict = new ConcurrentDictionary<string, string>(DLogOpenAPIHelper.GetResourceDict(ResourceType.SystemCode), StringComparer.OrdinalIgnoreCase);
            SourceDict = new ConcurrentDictionary<string, string>(DLogOpenAPIHelper.GetResourceDict(ResourceType.Source), StringComparer.OrdinalIgnoreCase);
            ClassNameDict = new ConcurrentDictionary<string, string>(DLogOpenAPIHelper.GetResourceDict(ResourceType.ClassName), StringComparer.OrdinalIgnoreCase);
            MethodNameDict = new ConcurrentDictionary<string, string>(DLogOpenAPIHelper.GetResourceDict(ResourceType.MethodName), StringComparer.OrdinalIgnoreCase);
        }

        internal static readonly Task PerfWriteLogTask = Task.Factory.StartNew(() =>
        {
            Thread.CurrentThread.Name = "PerfLogTaskManager.Run";
            WriteLog();
        });

        /// <summary>
        /// 写入性能监控日志
        /// </summary>
        private static void WriteLog()
        {
            var insertCycleTime = 10000;
            while (true)
            {
                try
                {
                    var config = CacheHelper.CommonConfig.PerfLog;
                    insertCycleTime = config.InsertCycleTime;
                    var list = Dequeue(config);
                    if (list.Count > 0)
                    {
                        using (var factory = new ChannelFactory<IDLogPerfLogService>("*"))
                        {
                            var client = factory.CreateChannel();
                            var result = client.BatchInsert(list);
                        }
                    }

                    #region 看起来没有用，先注释掉
                    //if (list.Count == config.MaxPostCount)
                    //{
                    //    continue;
                    //} 
                    #endregion
                }
                catch (Exception ex)
                {
                    ErrorLogManager.Instance.Enqueue(new ErrorLogInfo
                    {

                        Message = ex.Message,
                        Detail = ex.ToString(),
                        Source = "Offline.Site",
                        SystemCode = "Log"

                    });
                }

                Thread.Sleep(insertCycleTime);
            }
        }

        /// <summary>
        /// 出队
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        private static List<PerfLog> Dequeue(PerfLogConfig config)
        {
            var result = new List<PerfLog>();
            while (true)
            {
                PerfLog item;
                if (Queue.TryDequeue(out item))
                {

                    try
                    {
                        item.SystemCode = string.IsNullOrEmpty(item.SystemCode) ? "Empty" : item.SystemCode;
                        item.SystemCode = SystemCodeDict[item.SystemCode];
                    }
                    catch
                    {
                        var id = DLogOpenAPIHelper.AddResource(ResourceType.SystemCode, item.SystemCode);
                        if (id > 0)
                        {
                            SystemCodeDict = new ConcurrentDictionary<string, string>(DLogOpenAPIHelper.GetResourceDict(ResourceType.SystemCode), StringComparer.OrdinalIgnoreCase);
                            item.SystemCode = id.ToString();
                        }
                    }
                    try
                    {
                        item.Source = string.IsNullOrEmpty(item.Source) ? "Empty" : item.Source;
                        item.Source = SourceDict[item.Source];
                    }
                    catch
                    {
                        var id = DLogOpenAPIHelper.AddResource(ResourceType.Source, item.Source);
                        if (id > 0)
                        {
                            SourceDict = new ConcurrentDictionary<string, string>(DLogOpenAPIHelper.GetResourceDict(ResourceType.Source), StringComparer.OrdinalIgnoreCase);
                            item.Source = id.ToString();
                        }
                    }
                    try
                    {
                        item.ClassName = string.IsNullOrEmpty(item.ClassName) ? "Empty" : item.ClassName;
                        item.ClassName = ClassNameDict[item.ClassName];
                    }
                    catch
                    {
                        var id = DLogOpenAPIHelper.AddResource(ResourceType.ClassName, item.ClassName);
                        if (id > 0)
                        {
                            ClassNameDict = new ConcurrentDictionary<string, string>(DLogOpenAPIHelper.GetResourceDict(ResourceType.ClassName), StringComparer.OrdinalIgnoreCase);
                            item.ClassName = id.ToString();
                        }
                    }
                    try
                    {
                        item.MethodName = string.IsNullOrEmpty(item.MethodName) ? "Empty" : item.MethodName;
                        item.MethodName = MethodNameDict[item.MethodName];
                    }
                    catch
                    {
                        var id = DLogOpenAPIHelper.AddResource(ResourceType.MethodName, item.MethodName);
                        if (id > 0)
                        {
                            MethodNameDict = new ConcurrentDictionary<string, string>(DLogOpenAPIHelper.GetResourceDict(ResourceType.MethodName), StringComparer.OrdinalIgnoreCase);
                            item.MethodName = id.ToString();
                        }
                    }
                    result.Add(item);
                    if (result.Count >= config.MaxPostCount)
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// 入队
        /// </summary>
        /// <param name="model"></param>
        public static void Enqueue(List<PerfLog> list)
        {
            var config = CacheHelper.CommonConfig.PerfLog;
            if (!config.IsEnabled)
            {
                return;
            }

            if (PerfWriteLogTask.Status == TaskStatus.Running)
            {
                foreach (var item in list)
                {
                    if (Queue.Count < config.MaxReceiveCount && item.Duration >= config.Duration)
                    {
                        Queue.Enqueue(item);
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
    }
}
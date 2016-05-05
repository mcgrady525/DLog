using DLog.Common.Helper;
using DLog.Entity;
using DLog.Entity.CommonBO;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.ServiceModel;
using DLog.IService;
using Tmac.Frameworks.Common.Extends;

namespace DLog.OpenAPI.Unity
{
    public class DebugLogTaskManager
    {
        internal static ConcurrentQueue<DebugLog> Queue = new ConcurrentQueue<DebugLog>();

        internal static readonly Task DebugWriteLogTask = Task.Factory.StartNew(() =>
        {
            Thread.CurrentThread.Name = "DebugLogTaskManager.Run";
            WriteLog();
        });

        /// <summary>
        /// 写入日志
        /// </summary>
        private static void WriteLog()
        {
            var insertCycleTime = 10000;
            while (true)
            {
                try
                {
                    var config = CacheHelper.CommonConfig.DebugLog;
                    insertCycleTime = config.InsertCycleTime;
                    var list = Dequeue(config);
                    if (list.Count > 0)
                    {
                        using (var factory = new ChannelFactory<IDLogDebugLogService>("*"))
                        {
                            var client = factory.CreateChannel();
                            var result = client.BatchInsert(list);
                        }
                    }

                    #region 看起来没什么用，先注释掉，因为已在Dequeue内部做了判断
                    //if (list.Count == config.MaxPostCount)
                    //{
                    //    continue;
                    //} 
                    #endregion
                }
                catch
                {
                }

                Thread.Sleep(insertCycleTime);
            }
        }

        /// <summary>
        /// 出队
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        private static List<DebugLog> Dequeue(CommonConfigBase config)
        {
            var result = new List<DebugLog>();
            while (true)
            {
                DebugLog item;
                if (Queue.TryDequeue(out item))
                {
                    if (item.Message.IsNullOrEmpty())
                    {
                        item.Message = "无，请加上，此处为Log系统修正结果";
                    }
                    item.Message = item.Message.Truncate();
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
        /// <param name="list"></param>
        public static void Enqueue(List<DebugLog> list)
        {
            var config = CacheHelper.CommonConfig.DebugLog;
            if (!config.IsEnabled)
            {
                return;
            }

            if (DebugWriteLogTask.Status == TaskStatus.Running)
            {
                foreach (var item in list)
                {
                    if (Queue.Count < config.MaxReceiveCount)
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
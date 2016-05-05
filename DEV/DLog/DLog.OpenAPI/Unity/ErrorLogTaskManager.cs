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
    public class ErrorLogTaskManager
    {
        internal static ConcurrentQueue<ErrorLog> Queue = new ConcurrentQueue<ErrorLog>();

        internal static readonly Task ErrorWriteLogTask = Task.Factory.StartNew(() =>
        {
            Thread.CurrentThread.Name = "ErrorLogTaskManager.Run";
            WriteLog();
        });

        /// <summary>
        /// 写入错误日志
        /// </summary>
        private static void WriteLog()
        {
            var insertCycleTime = 10000;
            while (true)
            {
                try
                {
                    var config = CacheHelper.CommonConfig.ErrorLog;
                    insertCycleTime = config.InsertCycleTime;
                    var list = Dequeue(config);
                    if (list.Count > 0)
                    {
                        using (var factory = new ChannelFactory<IDLogErrorLogService>("*"))
                        {
                            var client = factory.CreateChannel();
                            var result= client.BatchInsert(list);
                        }
                    }

                    #region 这一段应该没有意义，因为Dequeue里已经判断了，所以暂时注释掉
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
        /// 入队
        /// </summary>
        /// <param name="model"></param>
        public static void Enqueue(List<ErrorLog> list)
        {
            var config = CacheHelper.CommonConfig.ErrorLog;
            if (!config.IsEnabled)
            {
                return;
            }

            if (ErrorWriteLogTask.Status == TaskStatus.Running)
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

        /// <summary>
        /// 出队
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        private static List<ErrorLog> Dequeue(CommonConfigBase config)
        {
            var result = new List<ErrorLog>();
            while (true)
            {
                ErrorLog item;
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
    }
}
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace DLog.Common.Helper
{
    [DebuggerDisplay("Key:{Key}  HasValue:{HasValue}")]
    public class CacheInfo
    {
        public string Key { get; set; }
        public string Desc { get; set; }
        public int? Count { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? ExpireTime { get; set; }
        public TimeSpan? BuildTime { get; set; }

        private bool? _HasValue;

        public bool HasValue
        {
            get
            {
                if (!_HasValue.HasValue)
                {
                    _HasValue = CreateTime.HasValue || (!string.IsNullOrWhiteSpace(Key) && HttpRuntime.Cache[Key] != null);
                }
                return _HasValue.Value;
            }
            set
            {
                _HasValue = value;
            }
        }
    }

    public class CacheStatus
    {
        public string SiteName { get; set; }
        public string SiteUrl { get; set; }
        public string Status { get; set; }
        public List<CacheInfo> CacheInfos { get; set; }

        public string MachineName { get; set; }

        public string IP { get; set; }

        public DateTime? UploadTime { get; set; }
    }

    public static partial class CacheHelper
    {
        //第一次調用時便加載到靜態變量里，以后不用再執行此操作，提速
        //不能將其改為public,不然有隱患
        private static readonly Dictionary<CacheKeys, Tuple<string, string>> AllKeyName = FillAllKeyName();

        private static Dictionary<CacheKeys, Tuple<string, string>> FillAllKeyName()
        {
            var dict = new Dictionary<CacheKeys, Tuple<string, string>>();
            foreach (var item in Enum.GetValues(typeof(CacheKeys)))
            {
                var key = (CacheKeys)item;
                var keyName = Enum.GetName(typeof(CacheKeys), item);

                var obj = Attribute.GetCustomAttribute(typeof(CacheKeys).GetField(keyName), typeof(DescriptionAttribute)) as DescriptionAttribute;
                var desc = obj == null ? null : (obj as DescriptionAttribute).Description;

                dict.Add(key, new Tuple<string, string>("CacheHelper_" + keyName, desc));
            }
            return dict;
        }

        public static bool NoCache(this CacheKeys input)
        {
            return HttpRuntime.Cache[input.GetName()] == null;
        }

        public static string GetName(this CacheKeys input)
        {
            return AllKeyName[input].Item1;
        }

        public static string GetDescription(this CacheKeys input)
        {
            return AllKeyName[input].Item2;
        }

        #region Get方法，多個重載（主體代碼基本相同） ，不用Object是考慮拆箱裝箱問題，如覺得不妥，請提出。

        private static ConcurrentDictionary<string, DateTime> Queue = new ConcurrentDictionary<string, DateTime>();
        public static List<T> Get<T>(CacheKeys key, Func<int> func, ref List<T> value, int expireMinutes)
        {
            if (key.NoCache())
            {
                var startTime = DateTime.Now;
                var isFirst = Queue.TryAdd(key.GetName(), startTime);//第一個進來的填充就行，其它的等結果吧
                if (isFirst)
                {
                    Task.Factory.StartNew(() =>
                    {
                        try
                        {
                            if (key.NoCache())
                            {
                                var count = func();
                                var cacheInfo = new CacheInfo
                                {
                                    Key = key.GetName(),
                                    Desc = key.GetDescription(),
                                    Count = count,
                                    CreateTime = DateTime.UtcNow,
                                    ExpireTime = DateTime.UtcNow.AddMinutes(expireMinutes),
                                    BuildTime = (DateTime.Now - startTime)
                                };

                                HttpRuntime.Cache.Insert(cacheInfo.Key, cacheInfo, null, cacheInfo.ExpireTime.Value, Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, CacheOnRemovedCallback);
                            }
                        }
                        finally
                        {
                            Queue.TryRemove(key.GetName(), out startTime);
                        }
                    });
                }
            }

            while (true)
            {
                if (value != null)
                {
                    break;
                }
                Thread.Sleep(500);
            }
            return value;
        }

        public static Dictionary<TKey, TValue> Get<TKey, TValue>(CacheKeys key, Func<int> func, ref Dictionary<TKey, TValue> value, int expireMinutes)
        {
            if (key.NoCache())
            {
                var startTime = DateTime.Now;
                var isFirst = Queue.TryAdd(key.GetName(), startTime);//第一個進來的填充就行，其它的等結果吧
                if (isFirst)
                {
                    Task.Factory.StartNew(() =>
                    {
                        try
                        {
                            if (key.NoCache())
                            {
                                var count = func();
                                var cacheInfo = new CacheInfo
                                {
                                    Key = key.GetName(),
                                    Desc = key.GetDescription(),
                                    Count = count,
                                    CreateTime = DateTime.UtcNow,
                                    ExpireTime = DateTime.UtcNow.AddMinutes(expireMinutes),
                                    BuildTime = (DateTime.Now - startTime)
                                };

                                HttpRuntime.Cache.Insert(cacheInfo.Key, cacheInfo, null, cacheInfo.ExpireTime.Value, Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, CacheOnRemovedCallback);
                                //PkgHelper.WritePerfLog("CacheHelper", (DateTime.Now - startTime).Milliseconds, key.GetName(), string.Format("items：{0} ", count), "Offline.Service");
                            }
                        }
                        finally
                        {
                            Queue.TryRemove(key.GetName(), out startTime);
                        }
                    });
                }
            }

            while (true)
            {
                if (value != null)
                {
                    break;
                }
                Thread.Sleep(500);
            }
            return value;
        }

        public static T Get<T>(CacheKeys key, Action action, ref T value, int expireMinutes)
        {
            if (key.NoCache())//沒數據就嘗試去填充數據
            {
                var startTime = DateTime.Now;
                var isFirst = Queue.TryAdd(key.GetName(), startTime);//第一個進來的填充就行，其它的等結果吧
                if (isFirst)
                {
                    Task.Factory.StartNew(() =>
                    {
                        try
                        {
                            if (key.NoCache())
                            {
                                action();
                                var cacheInfo = new CacheInfo
                                {
                                    Key = key.GetName(),
                                    Desc = key.GetDescription(),
                                    Count = 1,
                                    CreateTime = DateTime.UtcNow,
                                    ExpireTime = DateTime.UtcNow.AddMinutes(expireMinutes),
                                    BuildTime = (DateTime.Now - startTime)
                                };

                                HttpRuntime.Cache.Insert(cacheInfo.Key, cacheInfo, null, cacheInfo.ExpireTime.Value, Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, CacheOnRemovedCallback);
                                //PkgHelper.WritePerfLog("CacheHelper", (DateTime.Now - startTime).Milliseconds, key.GetName(), string.Format("items：{0} ", 1), "Offline.Service");
                            }
                        }
                        finally
                        {
                            Queue.TryRemove(key.GetName(), out startTime);
                        }
                    });
                }
            }

            while (true)
            {
                if (value != null)
                {
                    break;
                }
                Thread.Sleep(500);
            }
            return value;
        }

        /// <summary>
        /// 定义在从 System.Web.Caching.Cache 移除缓存项时通知应用程序的回调方法。
        /// </summary>
        /// <param name="key">从缓存中移除的键（当初由Add, Insert传入的）。</param>
        /// <param name="value">与从缓存中移除的键关联的缓存项（当初由Add, Insert传入的）。</param>
        /// <param name="reason">从缓存移除项的原因。 </param>
        private static void CacheOnRemovedCallback(string key, object value, CacheItemRemovedReason reason)
        {
            //PkgHelper.WritePerfLog("CacheHelper", 1, key, string.Format("Remove Reason：{0}", reason), "Offline.Service");
        }

        #endregion
    }

    /// <summary>
    /// 如果哪些屬性用到了Cache也可加上此特性
    /// 以便后續統一填充或清除
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class IsCacheDataAttribute : Attribute
    {
    }
}

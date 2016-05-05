using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tmac.Frameworks.Common.Extends;

namespace DLog.Common.Helper
{
    public static class DLogHelper
    {
        /// <summary>
        /// gzip向zlib过期产物，初期可能较多触发异常。
        /// 兼容mysql的zlib,及gzip,如果解压失败则返回  解压失败
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string UnCompress(byte[] input)
        {
            var detail = string.Empty;

            if (input != null)
            {
                try
                {
                    detail = "zlib\r\n" + input.MySQLUncompress();
                }
                catch
                {

                    try
                    {
                        detail = "gzip\r\n" + input.GZipDecompress();
                    }
                    catch
                    {
                        detail = "解压失败";
                    }
                }
            }

            return detail;

        }

        /// <summary>
        /// 去除重复，可以是一个字段，也可以是多个字段
        /// 用法：
        /// 一个字段：var query = people.DistinctBy(p => p.Id);
        /// 多个字段：var query = people.DistinctBy(p => new { p.Id, p.Name });
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }

    }
}

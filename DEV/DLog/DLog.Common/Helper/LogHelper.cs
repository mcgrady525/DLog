using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

#region readme

//使用方法：
//一，web.config中添加节点<system.diagnostics>，如下
//<system.diagnostics>
//    <trace autoflush="true"/>
//    <assert logfilename="Roy.log"/>
//</system.diagnostics>

//二，修改LogHelper.cs中的名称，如下
//private static NLog.Logger defaultLog = NLog.LogManager.GetLogger("Roy.Log"); // 这里的logger name可以从web.config中读取

//三，appSettings中添加key 'Nlog.Config.Path',如下
//<!--Nlog.Config路徑-->
//<add key ="Nlog.Config.Path" value="VConfigs/Dev/Nlog.config"/>

#endregion

namespace DLog.Common.Helper
{
    /// <summary>
    /// 写文本日志帮助类
    /// </summary>
    public static class LogHelper
    {
        private static NLog.Logger defaultLog = NLog.LogManager.GetLogger("DLog.Log");

        static LogHelper()
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                ConfigurationManager.AppSettings["Nlog.Config.Path"]);
            NLog.LogManager.Configuration = new NLog.Config.XmlLoggingConfiguration(filePath);
        }

        [Flags]
        public enum Tag
        {
            Default = 0,
            Online = 1 << 0,
            Offline = 1 << 1,
            Service = 1 << 2,
            BookingSearch = 1 << 3,
            Order = 1 << 4,
            Task = 1 << 5,
        }

        /// <summary>
        /// NLog记录Error日志
        /// </summary>
        /// <param name="action">数据库字段限制，信息长度不能超过256个字符，否则进行截取处理</param>
        /// <param name="tag">标签Tag</param>
        /// <param name="ex">Exception</param>
        public static void Error(Func<string> action, Tag tag = Tag.Default, Exception ex = null)
        {
            if (defaultLog.IsErrorEnabled)
            {
                var len = 256;
                var msg = action() + (tag == Tag.Default ? "" : " Tag:" + tag.ToString());
                if (msg.Length > len)
                {
                    msg = msg.Substring(0, len);
                }
                if (ex == null)
                {
                    defaultLog.Error(msg);
                }
                else
                {
                    defaultLog.Error(msg, ex);
                }
            }
        }

        public static void Info(Func<string> action, Tag tag = Tag.Default, Exception ex = null)
        {
            if (defaultLog.IsInfoEnabled)
            {
                if (ex == null)
                {
                    defaultLog.Info(action() + (tag == Tag.Default ? "" : " Tag:" + tag.ToString()));
                }
                else
                {
                    defaultLog.Info(action() + (tag == Tag.Default ? "" : " Tag:" + tag.ToString()), ex);
                }
            }
        }

        public static void Debug(Func<string> action, Tag tag = Tag.Default, Exception ex = null)
        {
            if (defaultLog.IsDebugEnabled)
            {
                if (ex == null)
                {
                    defaultLog.Debug(action() + (tag == Tag.Default ? "" : " Tag:" + tag.ToString()));
                }
                else
                {
                    defaultLog.Debug(action() + (tag == Tag.Default ? "" : " Tag:" + tag.ToString()), ex);
                }
            }
        }
    }
}

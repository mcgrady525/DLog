using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DLog.OpenAPI.Models
{
    public class CommonBO<T>
    {
        public Header head { get; set; }

        public T data { get; set; }
    }

    public class Header
    {
        /// <summary>
        /// 版本
        /// </summary>
        public string ver { get; set; }

        public string auth { get; set; }

        public int errcode { get; set; }

        public string cid { get; set; }
    }
}
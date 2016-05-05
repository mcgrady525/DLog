using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

/*********************************************************
 * 开发人员：鲁宁
 * 创建时间：2015年9月2日18:06:24
 * 描述说明：
 * 
 * 更改历史：
 * 
 * *******************************************************/
namespace DLog.Entity.CommonBO
{
    [DataContract, Serializable]
    public class DLogResult<DLogBO>
    {
        /// <summary>
        /// 返回單個實體內容
        /// </summary>
        [DataMember]
        public DLogBO Content { get; set; }

        /// <summary>
        /// 返回集合內容
        /// </summary>
        [DataMember]
        public List<DLogBO> Contents { get; set; }

        /// <summary>
        /// 返回信息
        /// </summary>
        [DataMember]
        public string Message { get; set; }

        /// <summary>
        /// 數據庫返回記錄數
        /// </summary>
        [DataMember]
        public int RecordCount { get; set; }

        /// <summary>
        /// 返回枚舉
        /// 常用 未定義的結果：Unknown = 0, 成功：Success = 1, 出錯：Error = 2 
        /// </summary>
        [DataMember]
        public ReturnCodeType ReturnCode { get; set; }

        /// <summary>
        /// 返回值
        /// 目前用做存儲 SessionKey
        /// </summary>
        [DataMember]
        public string Item1 { get; set; }
    }
}

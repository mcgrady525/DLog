using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

/*********************************************************
 * 开发人员：鲁宁
 * 创建时间：2015年9月2日18:06:10
 * 描述说明：
 * 
 * 更改历史：
 * 
 * *******************************************************/
namespace DLog.Entity.CommonBO
{
    [DataContract(IsReference = true)]
    [Serializable]
    public class PagingBase
    {
        public PagingBase()
        {
            PageIndex = 1;
            PageSize = 20;
        }

        /// <summary>
        /// 當前頁
        /// </summary>
        [DataMember]
        public int PageIndex { get; set; }


        /// <summary>
        /// 每頁顯示數
        /// </summary>
        [DataMember]
        public int PageSize { get; set; }
    }
}

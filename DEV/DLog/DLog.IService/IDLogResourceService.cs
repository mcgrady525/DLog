using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using DLog.Entity.CommonBO;
using DLog.Entity;
using DLog.Entity.Enum;

namespace DLog.IService
{
    [ServiceContract(ConfigurationName = "DLogResourceService.IDLogResourceService")]
    public interface IDLogResourceService
    {
        /// <summary>
        /// 依据资源类型获取资源字典
        /// </summary>
        /// <param name="type">资源类型</param>
        /// <returns></returns>
        [OperationContract]
        DLogResult<Dictionary<string, string>> GetResourceDict(ResourceType type);

        /// <summary>
        /// 增加资源
        /// </summary>
        /// <param name="type">资源类型</param>
        /// <param name="name">资源名称</param>
        /// <returns></returns>
        [OperationContract]
        DLogResult<int> AddResource(ResourceType type, string name);

    }
}

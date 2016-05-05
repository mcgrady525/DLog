using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using DLog.Entity.CommonBO;
using DLog.Entity.Enum;
using DLog.Entity.ViewModel;

namespace DLog.IService
{
    [ServiceContract(ConfigurationName = "DLogCommonService.IDLogCommonService")]
    public interface IDLogCommonService
    {
        /// <summary>
        /// 获取公共配置
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        DLogResult<CommonConfigBO> GetCommonConfig();

        /// <summary>
        /// 初始化公共配置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        DLogResult<bool> InitCommonConfig(CommonConfigBO model);

        /// <summary>
        /// 刷新search info
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        DLogResult<bool> RefreshSearchInfo();

        /// <summary>
        /// 依据searchInfoType查询SystemCode list
        /// </summary>
        /// <param name="searchInfoType"></param>
        /// <returns></returns>
        [OperationContract]
        DLogResult<List<string>> SearchSystemCodeList(SearchInfoType searchInfoType);

        /// <summary>
        /// 依据查询条件和searchInfoType查询source list
        /// </summary>
        /// <param name="request"></param>
        /// <param name="searchInfoType"></param>
        /// <returns></returns>
        [OperationContract]
        DLogResult<List<string>> SearchSourceList(SearchSearchInfoListRQ request, SearchInfoType searchInfoType);

        /// <summary>
        /// 依据查询条件和searchInfoType查询className list
        /// </summary>
        /// <param name="request"></param>
        /// <param name="searchInfoType"></param>
        /// <returns></returns>
        [OperationContract]
        DLogResult<List<string>> SearchClassNameList(SearchSearchInfoListRQ request, SearchInfoType searchInfoType);

        /// <summary>
        /// 依据查询条件和searchInfoType查询methodName list
        /// </summary>
        /// <param name="request"></param>
        /// <param name="searchInfoType"></param>
        /// <returns></returns>
        [OperationContract]
        DLogResult<List<string>> SearchMethodNameList(SearchSearchInfoListRQ request, SearchInfoType searchInfoType);

    }
}

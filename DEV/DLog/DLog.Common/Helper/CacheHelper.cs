using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DLog.Entity.CommonBO;
using System.ServiceModel;
using DLog.IService;

namespace DLog.Common.Helper
{
    public static partial class CacheHelper
    {
        #region 公共配置
        private static CommonConfigBO _CommonConfigBO = null;

        [IsCacheData]
        public static CommonConfigBO CommonConfig
        {
            get
            {
                return CacheHelper.Get(CacheKeys.CommonConfig, () =>
                {
                    using (var factory = new ChannelFactory<IDLogCommonService>("*"))
                    {
                        var client = factory.CreateChannel();
                        _CommonConfigBO = client.GetCommonConfig().Content;
                    }
                }, ref _CommonConfigBO, DLogConfigHelper.CacheTimeOutMinutes);
            }
        } 
        #endregion
    }
}

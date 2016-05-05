using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DLog.IService;
using System.ServiceModel.Activation;
using System.ServiceModel;
using Tmac.Frameworks.Log;
using DLog.Entity.CommonBO;
using DLog.Entity.Enum;
using DLog.Data;
using DLog.Entity;

namespace DLog.Service
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    [WcfServiceCounter(SystemCode = "DLog", Source = "Offline.Service")]
    public class DLogResourceService: IDLogResourceService
    {
        /// <summary>
        /// 依据资源类型获取资源字典
        /// </summary>
        /// <param name="type">资源类型</param>
        /// <returns></returns>
        public DLogResult<Dictionary<string, string>> GetResourceDict(ResourceType type)
        {
            var result = new DLogResult<Dictionary<string, string>>
            {
                ReturnCode = Entity.ReturnCodeType.Error,
                Content= new Dictionary<string,string>()
            };

            DLog.Common.Helper.DBHelper.NoLockInvokeDB(() => 
            {
                using (var db = new DLogDB())
                {
                    result.Content= db.Resource.Where(p => p.Type == (int)type).ToDictionary(i => i.Name, i => i.ID.ToString(), StringComparer.OrdinalIgnoreCase);
                    result.ReturnCode = Entity.ReturnCodeType.Success;
                }
            });

            return result;
        }

        /// <summary>
        /// 增加资源
        /// </summary>
        /// <param name="type">资源类型</param>
        /// <param name="name">资源名称</param>
        /// <returns></returns>
        public DLogResult<int> AddResource(ResourceType type, string name)
        {
            var result = new DLogResult<Int32> 
            {
                ReturnCode = Entity.ReturnCodeType.Error,
                Content = 0
            };

            using (var db = new DLogDB())
            {
                var item= db.Resource.FirstOrDefault(p=> p.Type== (int)type && p.Name.Equals(name));
                if (item != null)
                {
                    result.Content = item.ID;
                }
                else
                {
                    var model = new Resource 
                    {
                        Type= (int)type,
                        Name= name
                    };
                    db.Resource.Add(model);
                    if (db.SaveChanges() > 0)
                    {
                        result.Content = model.ID;
                    }
                }
                result.ReturnCode = ReturnCodeType.Success;
            }

            return result;
        }
    }
}

using DLog.Entity.Enum;
using DLog.IService;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel;
using System.Text;
using System.Web;

namespace DLog.OpenAPI.Helpers
{
    public static class DLogOpenAPIHelper
    {
        /// <summary>
        /// 输出JSON结果
        /// </summary>
        /// <param name="input"></param>
        /// <param name="statusCode"></param>
        /// <param name="isNeedFormat"></param>
        /// <returns></returns>
        public static HttpResponseMessage ToJsonResult(this object input, HttpStatusCode statusCode = HttpStatusCode.OK, bool isNeedFormat = true)
        {
            var jsonConverter = new List<JsonConverter>(){
             new Newtonsoft.Json.Converters.IsoDateTimeConverter(){ DateTimeFormat = "yyyy-MM-dd HH:mm:ss"}
            };
            var json = JsonConvert.SerializeObject(input, isNeedFormat ? Formatting.Indented : Formatting.None, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
                PreserveReferencesHandling = PreserveReferencesHandling.None,
                Converters = jsonConverter
            });

            var response = new HttpResponseMessage
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json"),
                StatusCode = statusCode
            };
            return response;
        }

        /// <summary>
        /// 依据资源类型获取资源字典
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetResourceDict(ResourceType type)
        {
            var result = new Dictionary<string, string>();

            using (var factory = new ChannelFactory<IDLogResourceService>("*"))
            {
                var client = factory.CreateChannel();
                result = client.GetResourceDict(type).Content;
            }

            return result;
        }

        /// <summary>
        /// 新增资源
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <returns>新的资源ID</returns>
        public static int AddResource(ResourceType type, string name)
        {
            var result = 0;

            using (var factory = new ChannelFactory<IDLogResourceService>("*"))
            {
                var client = factory.CreateChannel();
                result = client.AddResource(type, name).Content;
            }

            return result;
        }

    }
}
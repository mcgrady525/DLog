using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace DLog.OpenAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            #region Route
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            ); 
            #endregion

        }
    }
}

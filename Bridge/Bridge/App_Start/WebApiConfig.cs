using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Net.Http;
using System.Web.Http.Dispatcher;

namespace Bridge
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}",
                defaults: new { id = RouteParameter.Optional },
                constraints: new { httpMethod = new System.Web.Routing.HttpMethodConstraint("GET") }
            );
            config.EnableSystemDiagnosticsTracing();
            
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Bridge
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(name: "GetAllUserWorkflowTasks", 
            url: "User/{GetAllUserWorkflowTasks}/{workflowID}/{userID}", 
             defaults: new
             {
                 controller = "User",
                 action = "GetAllUserWorkflowTasks",
                 // nothing optional 
             });
            routes.MapRoute(name: "Default1",
           url: "controller/{action}/{id1}/{id2}",
            defaults: new { controller = "Home", action = "Index", id1 = UrlParameter.Optional, id2 = UrlParameter.Optional }
            );
        }
    }
}
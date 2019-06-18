using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace olightvn
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "lv",
                url: "lv/{controllername}/{id}/{name}",
                defaults: new { controller = "Redirect", action = "Index", id = UrlParameter.Optional, name = UrlParameter.Optional, controllername=UrlParameter.Optional },
                namespaces: new[] { "olightvn.Areas.Main.Controllers" }
            ).DataTokens.Add("area", "Main");
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}/{name}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional, name=UrlParameter.Optional },
                namespaces: new[] { "olightvn.Areas.Main.Controllers" }
            ).DataTokens.Add("area", "Main");
            
        }
    }
}
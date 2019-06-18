using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace olightvn
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //Initialize Bootstrapper
            T.Core.DenpendencyResolver.Bootstrapper.Initialize("olightvn.dll");

            //log4net
            T.Code.Common.LogManager.Configurator();
            T.Code.Common.LogManager.LogDebug("Application start");
        }
        protected void Application_Error()
        {
            T.Code.Common.LogManager.LogError("Application ERROR");
        }
        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            var url=HttpContext.Current.Request.Url;
            //olightvn.Common.Header.BindMetaTags(url.AbsolutePath, url.Authority);
        }
    }
}
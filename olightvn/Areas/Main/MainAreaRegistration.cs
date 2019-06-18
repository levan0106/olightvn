using System.Web.Mvc;

namespace olightvn.Areas.Main
{
    public class MainAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Main";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
           
            context.MapRoute(
                "Main_default",
                "Main/{controller}/{action}/{id}/{name}",
                new { action = "Index", id = UrlParameter.Optional,name=UrlParameter.Optional },
                new[] { "olightvn.Areas.Main.Controllers" }
            ); 
            //context.MapRoute(
            //    "Main_dettail",
            //    "Main/product/{action}/{id}/{name}",
            //    new { action = "Index", id = UrlParameter.Optional, name = UrlParameter.Optional },
            //    new[] { "olightvn.Areas.Main.Controllers" }
            //);
        }
    }
}

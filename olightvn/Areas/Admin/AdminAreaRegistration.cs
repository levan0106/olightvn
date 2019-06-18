using System.Web.Mvc;

namespace olightvn.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Login_default",
                "Admin/login",
                new { controller = "User", action = "Login" },
                new[] { "olightvn.Areas.Admin.Controllers" }
            );
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new[] { "olightvn.Areas.Admin.Controllers" }
            );
        }
    }
}

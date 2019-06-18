using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using olightvn.Models;
using olightvn.Common;
using System.Web.UI;
using System.Data;
namespace olightvn.Areas.Main.Controllers
{
    public class BaseController : Controller
    {
        //
        // GET: /Base/
        public T.Core.Security.SecureData SecureData = new T.Core.Security.SecureData();
        public string CurrentUserLogin
        {
            get { return User != null ? User.Identity.Name : string.Empty; }
        }
        public string BaseUrl
        {
            get
            {
              return  string.Format("{0}://{1}", Request.Url.Scheme, Request.Url.Authority);
            }
        }

        //protected override void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //    var url = HttpContext.Request.Url.Authority;
        //    base.OnActionExecuting(filterContext);
        //}
        //protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        //{
        //    var url = HttpContext.Request.Url.Authority;
        //    base.Initialize(requestContext);
        //}
        
        public IEnumerable<olightvn.Models.SiteMap> Menu
        {
            get
            {
                if (Session[Constants.SITE_MAP] == null)
                    return null;
                return Session[Constants.SITE_MAP] as IEnumerable<olightvn.Models.SiteMap>;
            }
            set { Session[Constants.SITE_MAP] = value; }
        }
        public IEnumerable<olightvn.Models.SiteMap> Menu2
        {
            get
            {
                if (Session[Constants.SITE_MAP2] == null)
                    return null;
                return Session[Constants.SITE_MAP2] as IEnumerable<olightvn.Models.SiteMap>;
            }
            set { Session[Constants.SITE_MAP2] = value; }
        }
        protected internal PartialViewResult PartialViewCustom(string viewName)
        {
            return PartialViewCustom(viewName, null);
        }
       protected internal PartialViewResult PartialViewCustom(string viewName,object model)
        {

            string newPartialViewName = string.Empty;
            if (viewName.Contains(".cshtml"))
            {
                newPartialViewName = viewName.GetCurrentPath();
                if (!Helpers.CheckExistsFile(newPartialViewName))
                {
                    newPartialViewName = viewName;
                }
            }
            else
            {
                newPartialViewName = new MyPartialView(viewName).FindView(ControllerContext);
            }

            if (!Helpers.CheckExistsFile(newPartialViewName))
            {
                newPartialViewName = viewName;
            }
            return PartialView(newPartialViewName,model);
        }
    }
    public class MyPartialView
    {
        public MyPartialView(string viewName)
        {
            ViewName = viewName;
        }
        private string ViewName { get; set; }
        public string FindView(ControllerContext context)
        {
            string controllerName = context.RouteData.Values["controller"].ToString();
            string actionName = context.RouteData.Values["action"].ToString();
            string areaName = context.RouteData.DataTokens["area"].ToString();
            areaName = areaName == null || actionName == "" ? null : "/Areas/" + areaName;
            // if view name is null then set view is action
            ViewName = ViewName ?? actionName;

            string file = "~" + areaName + "/Views" + "/" + controllerName + "/" + ViewName + ".cshtml";
            
            file = file.GetCurrentPath();
            if (!Helpers.CheckExistsFile(file))
            {
                file = file.Replace(controllerName, "Shared");
            }
            return file;
        }
    }
   
}

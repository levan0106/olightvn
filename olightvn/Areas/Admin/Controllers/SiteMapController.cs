using olightvn.Models;
using olightvn.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace olightvn.Areas.Admin.Controllers
{
    public class SiteMapController : BaseController
    {
        readonly ISiteMapRepository _SiteMap;
        public SiteMapController(ISiteMapRepository siteMap)
        {
            _SiteMap = siteMap;
        }
        public ActionResult GetSitemap()
        {
            if (Menu == null)
                Menu = _SiteMap.GetSiteMap(CurrentUserLogin, (int)SitemapType.Admin);
            return Json(Menu, JsonRequestBehavior.AllowGet);
        }
       
        public bool ClearSession()
        {
            Session.Clear();
            return false;
        }

    }
}

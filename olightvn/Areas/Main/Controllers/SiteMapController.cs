using olightvn.Models;
using olightvn.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace olightvn.Areas.Main.Controllers
{
    public class SiteMapController : BaseController
    {
        readonly ISiteMapRepository _SiteMap;
        readonly IBreadCrumbRepository _BreadCrumb;
        public SiteMapController(ISiteMapRepository siteMap, IBreadCrumbRepository breadCrumb)
        {
            _SiteMap = siteMap;
            _BreadCrumb = breadCrumb;
        }
        public ActionResult GetSitemap()
        {
            if (Menu == null)
                Menu = _SiteMap.GetSiteMap((int)SitemapType.Header);
            return Json(Menu, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetSitemap2()
        {
            if (Menu2 == null)
                Menu2 = _SiteMap.GetSiteMap((int)SitemapType.Footer);
            return Json(Menu2, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetBreadCrumb(int id, string name)
        {
            var result = _BreadCrumb.GetData(id, name);
            return Json(result);
        }

        public bool ClearSession()
        {
            Session.Clear();
            return false;
        }

    }
}

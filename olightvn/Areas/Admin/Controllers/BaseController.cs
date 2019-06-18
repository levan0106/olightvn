using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using olightvn.Models;

namespace olightvn.Areas.Admin.Controllers
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
        public IEnumerable<olightvn.Models.SiteMap> Menu
        {
            get
            {
                if (Session[Constants.SITE_MAP_ADMIN] == null)
                    return null;
                return Session[Constants.SITE_MAP_ADMIN] as IEnumerable<olightvn.Models.SiteMap>;
            }
            set { Session[Constants.SITE_MAP_ADMIN] = value; }
        }

    }
}

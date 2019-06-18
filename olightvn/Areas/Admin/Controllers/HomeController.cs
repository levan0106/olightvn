using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace olightvn.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /Main/Home/

        public ActionResult Index()
        {
            return View();
        }

    }
}

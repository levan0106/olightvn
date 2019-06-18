using olightvn.Common;
using olightvn.Models;
using olightvn.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace olightvn.Areas.Main.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ICategoryRepository _category;
        private readonly IProductRepository _product;
        private readonly ISite _site;
        public HomeController(ICategoryRepository category, IProductRepository product, ISite site)
        {
            _category = category;
            _product = product;
            _site = site;
        }
        //
        // GET: /Main/Home/

        public ActionResult Index()
        {
            Site data = _site.GetInfo(SessionManager.SiteId);
            string thumbnail = string.IsNullOrEmpty(data.Image) ? "thumbnail.jpg" : data.Image;
            SessionManager.CurrentSite.Title = data.Name;
            SessionManager.CurrentSite.Description = data.Description;
            SessionManager.CurrentSite.Url = BaseUrl + Request.Url.AbsolutePath;
            SessionManager.CurrentSite.Image = BaseUrl + WebConfigurations.ImageFullPath + thumbnail;

            return View();
        }

        public PartialViewResult Main()
        {
            return PartialView();
        }

        public ActionResult GetData()
        {
            var result = _category.GetAllForHomePage();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}

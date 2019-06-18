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
    public class RedirectController : BaseController
    {
        private readonly IProductRepository _product;
        private readonly ICategoryRepository _category;

        public RedirectController(IProductRepository product, ICategoryRepository category)
        {
            _product = product;
            _category = category;
        }
        public ActionResult Index(int id, string name, string controllername)
        {
            if (controllername.Equals("detail"))
            {
                Product data = _product.GetInfo(id); 
                SessionManager.CurrentSite.Title = data.Name;
                SessionManager.CurrentSite.Description = data.Description;
                SessionManager.CurrentSite.Url = BaseUrl + Request.Url.AbsolutePath;
                SessionManager.CurrentSite.Image = BaseUrl + WebConfigurations.ImageFullPath + data.Thumbnail;
            }else if (controllername.Equals("cat"))
            {
                Category data = _category.GetInfo(id);
                SessionManager.CurrentSite.Title = data.Name;
                SessionManager.CurrentSite.Description = data.Description;
                SessionManager.CurrentSite.Url = BaseUrl + Request.Url.AbsolutePath;
                SessionManager.CurrentSite.Image = BaseUrl + WebConfigurations.ImageFullPath + data.Thumbnail;
            }

            object url = string.Format("{0}/#/{1}/{2}/{3}/{4}", BaseUrl, "lv", controllername, id, name);
            return View(url);
        }

    }
}

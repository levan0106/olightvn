using olightvn.Common;
using olightvn.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace olightvn.Areas.Main.Controllers
{
    public class ArticleController : BaseController
    {

        private readonly IArticle _article;
        public ArticleController(IArticle article)
        {
            _article = article;
        }

        [HttpGet]
        public ActionResult AboutUs()
        {
            var result = _article.GetInfo(SessionManager.SiteId);
            return View(result);
        }


    }
}

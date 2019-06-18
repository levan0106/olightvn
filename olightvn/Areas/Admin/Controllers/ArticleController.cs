using olightvn.Models;
using olightvn.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using olightvn.Common;
using T.Core.Filters;

namespace olightvn.Areas.Admin.Controllers
{
    public class ArticleController : BaseController
    {
       
        private readonly IArticle _article;
        public ArticleController(IArticle article, IContactRepository contact)
        {
            _article = article;
        }
        

        [HttpGet]
        [AuthorizationRequired(Permissions = "ModAll,AdminAll")]
        public ActionResult AboutUs()
        {
            var result = _article.GetInfo(SessionManager.SiteId);
            return View(result);
        }
        [HttpPost]
        [AuthorizationRequired(Permissions = "ModAll,AdminAll")]
        [ValidateInput(false)]
        public ActionResult AboutUs(Article article)
        {
            var result = _article.Update(article, CurrentUserLogin);
            return View(article);
        }

    }
}

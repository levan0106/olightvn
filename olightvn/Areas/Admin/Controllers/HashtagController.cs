using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using olightvn.Repositories;
using olightvn.Models;

namespace olightvn.Areas.Admin.Controllers
{
    public class HashtagController : BaseController
    {
        readonly ITag _hashtag;
        public HashtagController(ITag hashtag)
        {
            _hashtag = hashtag;
        }

        public IEnumerable<Tag> GetAll()
        {
            var result = _hashtag.GetAll();
            return result;
        }
        public IEnumerable<Tag> GetAllByProduct(int id)
        {
            var result = _hashtag.GetAllByProduct(id);
            return result;
        }

        public ActionResult GetHashtagByProduct(int id)
        {
            var result = GetAllByProduct(id).ToList();
            return Json(result);
        }
    }
}

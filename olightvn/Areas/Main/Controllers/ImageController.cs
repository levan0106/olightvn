using olightvn.Models;
using olightvn.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using olightvn.Common;
using System.IO;
using T.Core.Filters;
using T.Core.Common;

namespace olightvn.Areas.Main.Controllers
{
    public class ImageController : BaseController
    {
        //
        // GET: /Image/
        readonly IImageRepository _image;
        public ImageController(IImageRepository image)
        {
            _image = image;
        }

        public ActionResult GetImageByProduct(int id)
        {
            var result = _image.GetAll(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetImageBanner(int id)
        {
            var result = _image.GetAll(id);
            return Json(result,JsonRequestBehavior.AllowGet);
        }

       
    }
}

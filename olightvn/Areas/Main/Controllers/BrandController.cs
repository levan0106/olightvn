using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using olightvn.Repositories;
using T.Core.Filters;
using olightvn.Models;
using olightvn.Common;

namespace olightvn.Areas.Main.Controllers
{
    public class BrandController : BaseController
    {
        readonly IBrandRepository _brand;

        public BrandController(IBrandRepository brand)
        {
            _brand = brand;
        }

        public IEnumerable<Brand> GetAllBrand()
        {
            var result = _brand.GetAll();
            return result;
        }

        [AuthorizationRequired(Permissions = "ModAll,AdminAll")]
        public ActionResult Management()
        {
            return View();
        }

        [HttpPost]
        [AuthorizationRequired(Permissions = "ModAll,AdminAll")]
        public ActionResult GetAll()
        {
            var data = _brand.GetAll();
            return Json(data);
        }

        [AuthorizationRequired(Permissions = "ModAll,AdminAll")]
        public ActionResult Add()
        {
            ViewBag.Title = "Thêm thương hiệu";
            return View(new Brand());
        }

        [AuthorizationRequired(Permissions = "ModAll,AdminAll")]
        public ActionResult Edit(int id)
        {
            var result = _brand.GetInfo(id);
            ViewBag.Title = "Chỉnh sửa thương hiệu";
            return View("Add", result);
        }

        [HttpPost]
        [AuthorizationRequired(Permissions = "ModAll,AdminAll")]
        [ValidateInput(false)]
        public ActionResult Update(Brand brand, HttpPostedFileBase file)
        {
            if (file != null)
            {
                bool uploadFile;
                string fileName = GeneralFuncs.UploadFile(file, WebConfigurations.ThumbPath, null, out uploadFile);
                if (!uploadFile)
                {
                    return View("Add", brand);
                }

                brand.Thumbnail = fileName;
            }

            var result = _brand.Insert(brand, CurrentUserLogin);
            return Redirect("management");
        }

        [HttpPost]
        [AuthorizationRequired(Permissions = "ModAll,AdminAll")]
        public ActionResult Delete(int id)
        {
            var result = _brand.Delete(id, CurrentUserLogin);
            return Json(result);
        }
    }
}

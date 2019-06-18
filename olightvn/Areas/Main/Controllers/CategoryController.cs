using olightvn.Common;
using olightvn.Models;
using olightvn.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T.Core.Filters;

namespace olightvn.Areas.Main.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly ICategoryRepository _category;
        public CategoryController(ICategoryRepository category)
        {
            _category = category;
        }

        [AuthorizationRequired(Permissions = "ModAll,AdminAll")]
        public ActionResult GetAllActiveCategories()
        {
            var result = GetAllCategories();
            return Json(result);
        }

        [AuthorizationRequired(Permissions = "ModAll,AdminAll")]
        private IEnumerable<Category> GetAllCategories()
        {
            var result = _category.GetAll();
            return result;
        }

        [HttpPost]
        [AuthorizationRequired(Permissions = "ModAll,AdminAll")]
        public ActionResult GetAll(Paging paging)
        {
            var data = _category.GetAll(paging);

            paging.GetTotalRow = true;
            int total = _category.GetAll_ToTal(paging);

            var result = new { data = data, total = total };
            return Json(result);
        }

        public ActionResult GetInfo(int id)
        {
            var result = _category.GetInfo(id);
            return Json(result);
        }

        public ActionResult Index()
        {
            return View();
        }

        [AuthorizationRequired(Permissions = "ModAll,AdminAll")]
        public ActionResult Management()
        {
            return View();
        }

        [AuthorizationRequired(Permissions = "ModAll,AdminAll")]
        public ActionResult Add()
        {
            var cat = GetAllCategories();
            ViewBag.Categories = new SelectList(cat, "Id", "Name");
            ViewBag.Title = "Thêm danh mục";
            return View(new Category());
        }

        [AuthorizationRequired(Permissions = "ModAll,AdminAll")]
        public ActionResult Edit(int id)
        {
            var result= _category.GetInfo(id);
            var cat = GetAllCategories();
            ViewBag.Categories = new SelectList(cat, "Id", "Name", result != null ? result.ParentId : 0);
            ViewBag.Title = "Chỉnh sửa danh mục";
            return View("Add", result);
        }
        [HttpPost]
        [AuthorizationRequired(Permissions = "ModAll,AdminAll")]
        [ValidateInput(false)]
        public ActionResult Update(Category category, HttpPostedFileBase file)
        {
            if (file != null)
            {
                bool uploadFile;
                string fileName = GeneralFuncs.UploadFile(file, WebConfigurations.ThumbPath, DateTime.Now.Ticks.ToString(), out uploadFile);
                if (!uploadFile)
                {
                    return View("Add", category);
                }

                category.Thumbnail = fileName;
            }

            var result = _category.Insert(category, CurrentUserLogin);
            return Redirect("management");
        }

        [HttpPost]
        [AuthorizationRequired(Permissions = "ModAll,AdminAll")]
        public ActionResult Delete(int id)
        {
            var result = _category.Delete(id, CurrentUserLogin);
            return Json(result);
        }
    }
}

using olightvn.Common;
using olightvn.Models;
using olightvn.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T.Core.Filters;
using T.Core.Common;

namespace olightvn.Areas.Admin.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly ICategoryRepository _category;
        private readonly IImageRepository _image;
        public CategoryController(ICategoryRepository category, IImageRepository image)
        {
            _category = category;
            _image = image;
        }

        [AuthorizationRequired(Permissions = "ModAll,AdminAll")]
        public ActionResult GetAllActiveCategories()
        {
            var result = GetAllCategories();
            return Json(result);
        }

        //[AuthorizationRequired(Permissions = "ModAll,AdminAll")]
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
            return View(new CategoryModel());
        }

        [AuthorizationRequired(Permissions = "ModAll,AdminAll")]
        public ActionResult Edit(int id)
        {
            var result= _category.GetInfo(id).CastTo<CategoryModel>();
            var cat = GetAllCategories().Where(m => m.Id != id).ToList();
            ViewBag.Categories = new SelectList(cat, "Id", "Name", result != null ? result.ParentId : 0);
            ViewBag.Title = "Chỉnh sửa danh mục";
            return View("Add", result);
        }
        [HttpPost]
        [AuthorizationRequired(Permissions = "ModAll,AdminAll")]
        [ValidateInput(false)]
        public ActionResult Update(CategoryModel category, string fileCollections)
        {
            if (ModelState.IsValid)
            {
                List<FileModel> files = fileCollections.ConvertToFiles();

                if (files != null)
                {
                    FileModel file = files.Where(m => m.Selected).FirstOrDefault();

                    // if not have file which selected then system will pick the first file
                    if (file == null)
                        file = files[0];
                    category.Thumbnail = file.Name;
                    //Upload thumb image
                    var uploadFile = new ImageController(_image).FilesUpload(file, new Image
                    {
                        ProductId = -2,
                        Width = WebConfigurations.ImageThumbSize.Width,
                        Height = WebConfigurations.ImageThumbSize.Height
                    }, WebConfigurations.ThumbPath, string.Empty,true);

                    //Upload full zise
                    uploadFile = new ImageController(_image).FilesUpload(file, new Image
                    {
                        ProductId = -2,
                        Width = WebConfigurations.ImageFullSize.Width,
                        Height = WebConfigurations.ImageFullSize.Height
                    }, WebConfigurations.ImageFullPath, string.Empty);
                    if (!uploadFile)
                    {
                        return View("Add", category);
                    }
                }

                Category cat = new Category()
                {
                    Id = category.Id,
                    Name = category.Name,
                    ParentId = category.ParentId ?? 0,
                    Thumbnail = category.Thumbnail,
                    ShowHomePage = category.ShowHomePage,
                    ShowMenu = category.ShowMenu,
                    ActiveStatus = category.ActiveStatus,
                    Description = category.Description,
                    SortOrder = category.SortOrder ?? 0
                };
                var result = _category.Insert(cat, CurrentUserLogin);
                return Redirect("management");
            }
            return View("Add", category);
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

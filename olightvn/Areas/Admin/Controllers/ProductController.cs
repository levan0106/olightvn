using olightvn.Common;
using olightvn.Models;
using olightvn.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using T.Core.Filters;
using T.Core.Common;

namespace olightvn.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductRepository _product;
        private readonly ICategoryRepository _category;
        private readonly IImageRepository _image;
        private readonly IOriginRepository _origin;
        private readonly IBrandRepository _brand;
        private readonly ITag _tag;
        public ProductController(IProductRepository product, ICategoryRepository category, IImageRepository image, IOriginRepository origin, IBrandRepository brand, ITag tag)
        {
            _product = product;
            _category = category;
            _image = image;
            _origin = origin;
            _brand = brand;
            _tag = tag;
        }

        [HttpPost]
        public ActionResult GetAll(Paging paging)
        {
            var data = _product.GetAll(paging);

            paging.GetTotalRow = true;
            int total = _product.GetAll_Total(paging);

            var result = new { data = data, total = total };
            return Json(result);
        }
        public ActionResult GetAllByCategory(string id, Paging paging, int status)
        {
            string[] item = id == null ? null : id.Trim(',').Split(',');
            IEnumerable<Product> data = new List<Product>();
            Dictionary<string, int> total = new Dictionary<string, int>();
            if (item != null)
            {
                foreach (var i in item)
                {
                    int catId = Int32.Parse(i);
                    paging.GetTotalRow = false;
                    data = data.Concat(_product.GetAllByCategory(catId, paging, status));

                    paging.GetTotalRow = true;
                    total.Add(i, _product.GetAllByCategory_Total(catId, paging, status));
                }
            }
            //Exclude duplicates.
            List<Product> d = data.GroupBy(group => group.Id).Select(group => group.FirstOrDefault()).ToList();

            var result = new { data = d, total = total };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDetail(int id)
        {
            var result = _product.GetInfo(id);

            return Json(result);
        }


        [HttpPost]
        [AuthorizationRequired(Permissions = "ModAll,AdminAll")]
        public ActionResult Delete(int id)
        {
            var result = _product.Delete(id, CurrentUserLogin);
            return Json(result);
        }


        private IEnumerable<Category> GetAllCategories()
        {
            var result = _category.GetAll();
            return result;
        }

        [HttpGet]
        [AuthorizationRequired(Permissions = "ModAll,AdminAll")]
        public ActionResult Add()
        {
            var cat = GetAllCategories();
            ViewBag.Categories = new SelectList(cat, "Id", "Name");

            var origin = _origin.GetAll();
            ViewBag.Origin = new SelectList(origin, "Id", "Name");

            var brand = _brand.GetAll();
            ViewBag.Brand = new SelectList(brand, "Id", "Name");

            var hashtag = new HashtagController(_tag).GetAll();
            ViewBag.Hashtag = new MultiSelectList(hashtag, "Name", "Name");

            ViewBag.Title = "Thêm sản phẩm";
            return View(new ProductModel());
        }

        [HttpGet]
        [AuthorizationRequired(Permissions = "ModAll,AdminAll")]
        public ActionResult Edit(int id)
        {
            var result = _product.GetInfo(id).CastTo<ProductModel>();
            var hashtags = new HashtagController(_tag).GetAllByProduct(id);
            result.Tags = new List<string>(hashtags.Select(_ => _.Name));

            var cat = GetAllCategories();
            ViewBag.Categories = new SelectList(cat, "Id", "Name", result != null ? result.CatId : 0);

            var origin = _origin.GetAll();
            ViewBag.Origin = new SelectList(origin, "Id", "Name", result != null ? result.OriginId : 0);

            var brand = _brand.GetAll();
            ViewBag.Brand = new SelectList(brand, "Id", "Name", result != null ? result.BrandId : 0);

            var hashtag = new HashtagController(_tag).GetAll();
            MultiSelectList list = new MultiSelectList(hashtag, "Name", "Name", hashtags.Select(_ => _.Name));
            ViewBag.Hashtag = list;

            ViewBag.Title = "Cập nhật sản phẩm";
            return View("Add", result);
        }
        [HttpPost]
        [AuthorizationRequired(Permissions = "ModAll,AdminAll")]
        [ValidateInput(false)]
        public ActionResult Update(ProductModel model, string fileCollections)
        {
            if (ModelState.IsValid)
            {

                Product product = model.CastTo<Product>();
                string signature = product.Signature;
                string tags = product.Tags == null ? string.Empty : string.Join(",", product.Tags);

                string fileNameExt = DateTime.Now.Ticks.ToStringToDefault().ToUnSign() + "_";
                List<FileModel> files = fileCollections.ConvertToFiles();
                //Set thumbnail for product
                if (files != null)
                {
                    FileModel file = files.Where(m => m.Selected).FirstOrDefault();
                    if (file == null)
                    {
                        //If not select a file, system will pick the first item.
                        file = files[0];
                    }

                    string fileName = (file.Base64.IsNotNullOrEmpty() ? fileNameExt : "") + file.Name;
                    product.Thumbnail = fileName;
                }
                // Insert product: output is product info
                product = _product.InsertProduct(product, CurrentUserLogin);
                // Insert tags of product
                _tag.UpdateTagByProduct(product.Id, tags);


                //Delete all product image before add new images.
                var status = _image.DeleteImageByProduct(product.Id);
                //Upload image for product
                //Upload thumb image
                var uploadFiles = new ImageController(_image).FilesUpload(files, new Image
                {
                    ProductId = product.Id,
                    Signature = signature,
                    Width = WebConfigurations.ImageThumbSize.Width,
                    Height = WebConfigurations.ImageThumbSize.Height
                }, WebConfigurations.ThumbPath, fileNameExt, true);

                //Upload full size
                uploadFiles = new ImageController(_image).FilesUpload(files, new Image
                {
                    ProductId = product.Id,
                    Signature = signature,
                    Width = WebConfigurations.ImageFullSize.Width,
                    Height = WebConfigurations.ImageFullSize.Height
                }, WebConfigurations.ImageFullPath, fileNameExt);

                //If upload file is successful then return to current page else to management page.
                if (!uploadFiles)
                {
                    var cat = GetAllCategories();
                    ViewBag.Categories = new SelectList(cat, "Id", "Name", product != null ? product.CatId : 0);
                    var hashtag = new HashtagController(_tag).GetAll();
                    MultiSelectList list = new MultiSelectList(hashtag, "Name", "Name", tags);
                    ViewBag.Hashtag = list;
                    TempData["result"] = "Lỗi trong quá trình upload hình ảnh.";
                    return View("Add", model);
                }
                return Redirect("management");
            }
            return View("Add", model);
        }

        [AuthorizationRequired(Permissions = "ModAll,AdminAll")]
        public ActionResult Management()
        {
            return View();
        }
    }
}

using olightvn.Models;
using olightvn.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T.Core.Filters;
using T.Core.Common;
using olightvn.Common;
using System.Text.RegularExpressions;

namespace olightvn.Areas.Main.Controllers
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

        public ActionResult GetRelatedItems(int id, Paging paging)
        {

            var data = _product.GetRelatedItems(id, paging);

            paging.GetTotalRow = true;
            int total = _product.GetRelatedItems_Total(id, paging);

            var result = new { data = data, total = total };
            return Json(result);
        }

        public ActionResult GetDetail(int id)
        {
            var result = _product.GetInfo(id);

            return Json(result,JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult Search()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult SearchBy(Filtering filtering)
        {
            var data = _product.SearchBy(filtering);
            filtering.GetTotalRow = true;
            var total = _product.SearchBy_Total(filtering);
            var result = new { data = data, total = total };
            return Json(result, JsonRequestBehavior.AllowGet);
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

        public ActionResult Detail(int id)
        {
           // var result = _product.GetInfo(id);
            //if (result != null)
            //{
            //    SessionManager.CurrentSite.Keyword = result.Name + " | " + SessionManager.CurrentSite.Title;
            //    SessionManager.CurrentSite.Description = result.ShortDescription != null ? Regex.Replace(result.ShortDescription, "<.*?>", string.Empty) : string.Empty;
            //}
            return PartialViewCustom("Detail");
        }
        public ActionResult GetList()
        {
            return PartialView("List");
        }


    }
}

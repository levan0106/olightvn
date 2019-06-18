using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using olightvn.Repositories;
using olightvn.Models;
using T.Core.Filters;
using olightvn.Common;
using olightvn.Areas.Main.Controllers;

namespace olightvn.Areas.Main.Controllers
{
    public class OriginController : BaseController
    {
        //
        // GET: /Origin/
        readonly IOriginRepository _origin;
        public OriginController(IOriginRepository origin)
        {
            _origin = origin;
        }
        public IEnumerable<Origin> GetAllOrigin()
        {
            var result = _origin.GetAll();
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
            var data = _origin.GetAll();
            return Json(data);
        }

        [AuthorizationRequired(Permissions = "ModAll,AdminAll")]
        public ActionResult Add()
        {
            ViewBag.Title = "Thêm xuất sứ";
            return View(new Origin());
        }

        [AuthorizationRequired(Permissions = "ModAll,AdminAll")]
        public ActionResult Edit(int id)
        {
            var result = _origin.GetInfo(id);
            ViewBag.Title = "Chỉnh sửa xuất xứ";
            return View("Add", result);
        }

        [HttpPost]
        [AuthorizationRequired(Permissions = "ModAll,AdminAll")]
        [ValidateInput(false)]
        public ActionResult Update(Origin origin)
        {

            var result = _origin.Insert(origin, CurrentUserLogin);
            return Redirect("management");
        }

        [HttpPost]
        [AuthorizationRequired(Permissions = "ModAll,AdminAll")]
        public ActionResult Delete(int id)
        {
            var result = _origin.Delete(id, CurrentUserLogin);
            return Json(result);
        }
    }
}

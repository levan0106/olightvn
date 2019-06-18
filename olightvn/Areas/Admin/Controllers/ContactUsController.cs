using olightvn.Models;
using olightvn.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T.Core.Filters;

namespace olightvn.Areas.Admin.Controllers
{
    public class ContactUsController : BaseController
    {
        //
        // GET: /Admin/ContactUs/

        private readonly IContactRepository _contact;
        public ContactUsController( IContactRepository contact)
        {
            _contact = contact;
        }
        
        [AuthorizationRequired(Permissions = "ModAll,AdminAll")]
        public ActionResult Index()
        {
            var result = _contact.GetInfo(1);
            return View(result);
        }
        [HttpPost]
        [AuthorizationRequired(Permissions = "ModAll,AdminAll")]
        public ActionResult Index(Contact contact)
        {
            var result = _contact.Update(contact, CurrentUserLogin);

            return View(contact);
        }

    }
}

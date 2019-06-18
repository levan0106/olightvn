using olightvn.Common;
using olightvn.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using olightvn.Models;

namespace olightvn.Areas.Main.Controllers
{
    public class ContactUsController : BaseController
    {
        //
        // GET: /Main/ContactUs/

        private readonly IContactRepository _contact;
        private readonly IEmailRepository _email;
        public ContactUsController(IContactRepository contact, IEmailRepository email)
        {
            _contact = contact;
            _email = email;
        }
        public ActionResult Index()
        {
            var result = SessionManager.ContactInfo;
            return PartialViewCustom("Index", result);
            //return View(result);
        }
        //footer is using
        public ActionResult GetContact()
        {
            var result = SessionManager.ContactInfo;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #region[Send email]
        [HttpPost]
        public ActionResult SendEmail(FormCollection collection, ContactUsModel contact)
        {
            if(ModelState.IsValid)
            {

            var name = collection["Name"];
            var emailContact = collection["Email"];
            var subject = collection["Subject"];
            var content = collection["Content"];

            //var contact = new EmailController(_email).GetEmailConfig();
            //var to = contact.To;
            //var from = contact.From;
            //var password = contact.Password;
            //var smtpPort = contact.SMTPPort;
            //var enableSSL = contact.EnableSSL;

            var html = string.Format(@"<table style='text-align: left;'>
                                        <tr><td colspan='2'>Thư góp ý.</td></tr><tr>
                                        <tr><td>Gửi từ:</td><td>{0} </td> </tr> 
                                        <tr><td>Email:</td><td>{1}</td> </tr> 
                                        <tr><td>Tiêu đề:</td><td>{2}</td></tr>
                                        <tr><td>Nội dung:</td><th></th></tr><tr><td colspan='2'>{3}</td></tr>
                                        </table>", contact.Name, contact.Email, contact.Subject, contact.Content);
            var result = new EmailController(_email).SendEmail("Thư góp ý", html);
            @TempData["result"] = result == true ? "Email gửi thành công" : "Email không gửi được.";
            }

            return Redirect("~/#/lien-he");

        }
        #endregion
    }
}

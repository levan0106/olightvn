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
    public class EmailController : BaseController
    {
        //
        // GET: /Email/
        readonly IEmailRepository _email;
        public EmailController(IEmailRepository email)
        {
            _email = email;
        }

        protected Models.Email GetEmailConfig()
        {

            var result = _email.GetInfo(0);
            //Secure data
            result.Password = SecureData.DecryptText(result.Password, WebConfigurations.KeySecure);
            result.From = SecureData.DecryptText(result.From, WebConfigurations.KeySecure);
            result.To = SecureData.DecryptText(result.To, WebConfigurations.KeySecure);
            return result;
        }

        [HttpGet]
        [AuthorizationRequired(Permissions = "AdminAll")]
        public ActionResult Management()
        {
            EmailModel email = GetEmailConfig().CastTo<EmailModel>();
            return View(email);
        }

        [HttpPost]
        [AuthorizationRequired(Permissions = "AdminAll")]
        public ActionResult Management(EmailModel emailModel)
        {
            if (ModelState.IsValid)
            {
                //Secure data
                emailModel.Password = SecureData.EncryptText(emailModel.Password, WebConfigurations.KeySecure);
                emailModel.From = SecureData.EncryptText(emailModel.From, WebConfigurations.KeySecure);
                emailModel.To = SecureData.EncryptText(emailModel.To, WebConfigurations.KeySecure);
                var email = emailModel.CastTo<Models.Email>();
                var result = _email.Update(email, CurrentUserLogin);
            }
            return View(emailModel);
        }
        public bool SendEmail(string subject, string body)
        {
            var contact = GetEmailConfig();
            return SendEmail(contact, subject, body);
        }
        public bool SendEmail(string to, string subject, string body)
        {
            var contact = GetEmailConfig();
            contact.To = to;
            return SendEmail(contact, subject, body);
        }
        public bool SendEmail(Models.Email info, string subject, string body)
        {
            var to = info.To;
            var from = info.From;
            var password = info.Password;
            var smtpPort = info.SMTPPort;
            var enableSSL = info.EnableSSL;
            string Subject = GeneralFuncs.GetEmailFormatByKey("Subject");
            string Body = GeneralFuncs.GetEmailFormatByKey("Body");
            Body = Body.Replace("[content]", body);
            Subject = Subject.Replace("[content]", subject);

            T.Core.Common.Email.SendMail(from, to, from, password, smtpPort, enableSSL, Subject, Body);
            return true;
        }
    }
}

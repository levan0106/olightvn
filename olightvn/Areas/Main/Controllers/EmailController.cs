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
    public class EmailController : BaseController
    {
        //
        // GET: /Email/
        readonly IEmailRepository _email;
        public EmailController(IEmailRepository email)
        {
            _email = email;
        }

        protected Email GetEmailConfig()
        {

            Email result = _email.GetInfo(0);
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
            return View(GetEmailConfig());
        }

        [HttpPost]
        [AuthorizationRequired(Permissions = "AdminAll")]
        public ActionResult Management(Email email)
        {
            //Secure data
            email.Password = SecureData.EncryptText(email.Password,WebConfigurations.KeySecure);
            email.From = SecureData.EncryptText(email.From, WebConfigurations.KeySecure);
            email.To = SecureData.EncryptText(email.To, WebConfigurations.KeySecure);

            var result = _email.Update(email, CurrentUserLogin);
            return View(email);
        }

        public bool SendEmail(string subject, string body)
        {
            var contact = GetEmailConfig();
            return SendEmail(contact, subject, body);
        }
        public bool SendEmail(string to, string subject,string body)
        {
            var contact = GetEmailConfig();
            contact.To = to;
            return SendEmail(contact, subject, body);
        }
        public bool SendEmail(Email info, string subject, string body)
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

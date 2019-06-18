using olightvn.Common;
using olightvn.Models;
using olightvn.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace olightvn.Areas.Main.Controllers
{
    public class CartController : BaseController
    {
        //
        // GET: /Cart/
        private readonly IEmailRepository _email;
        public CartController(IEmailRepository email)
        {
            _email = email;
        }

        public ActionResult Index()
        {
            return PartialView("Index");
        }

        public ActionResult SubmitToCart(CartInfo cart)
        {
            // Read the file as one string.
            string text = GeneralFuncs.ReadFile("Templates\\CartTemplate.html");
            int bodyIndexStart = text.IndexOf("<tbody>") + 7;
            int bodyIndexEnd = text.IndexOf("</tbody>");
            string bodyTemplate = text.Substring(bodyIndexStart, bodyIndexEnd - bodyIndexStart);
            string newBody = string.Empty;
            int count = 0;
            double totalPay = 0;

            foreach (Cart c in cart.Carts)
            {
                count++;
                totalPay += c.Price * c.Quantity;
                string image = BaseUrl + WebConfigurations.ThumbPath + c.Image;
                newBody += bodyTemplate.Replace("[STT]", count.ToString()).Replace("[Image]", image).Replace("[Name]", c.ProName).Replace("[Price]", c.Price.ToString())
                    .Replace("[Quantity]", c.Quantity.ToString()).Replace("[Total]", (c.Price * c.Quantity).ToString());
            }
            text = text.Replace(bodyTemplate, newBody);
            text = text.Replace("[PriceTotal]", totalPay.ToString());
            text = text.Replace("[ReceiverName]", cart.YourName);
            text = text.Replace("[ReceiverAddress]", cart.YourAddress);
            text = text.Replace("[ReceiverPhone]", cart.YourPhone);
            text = text.Replace("[ReceiverEmail]", cart.YourEmail);

            var sendToUser = new EmailController(_email).SendEmail(cart.YourEmail, "Đơn đặt hàng", text);
            var sendToAdmin = new EmailController(_email).SendEmail("Đơn đặt hàng: " + cart.YourName, text);

            return Json("true",JsonRequestBehavior.AllowGet);
        }

    }

}

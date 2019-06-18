using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace olightvn.Models
{
    public class Cart
    {
        public int ProId { get; set; }
        public string ProName { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }
    }

    public class CartInfo
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string CreateDTS { get; set; }
        public string Voucher { get; set; }
        public string YourName { get; set; }
        public string YourAddress { get; set; }
        public string YourPhone { get; set; }
        public string YourEmail { get; set; }
        public List<Cart> Carts { get; set; }


    }
}
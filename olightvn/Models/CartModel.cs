using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace olightvn.Models
{
    public class CartModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Người nhận")]
        public string YourName { get; set; }
        [Display(Name = "Địa chỉ")]
        public string YourAddress { get; set; }

        [Display(Name = "Điện thoại")]
        [Required]
        [Phone]
        public string YourPhone { get; set; }
        [Display(Name = "Email")]

        [EmailAddress]
        public string YourEmail { get; set; }
    }
}
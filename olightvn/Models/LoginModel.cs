using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace olightvn.Models
{
    public class LoginModel
    {

        [Display(Name = "Tên đăng nhập")]
        [Required(ErrorMessage = "{0} không được để trống.")]
        public string UserName { get; set; }
        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "{0} không được để trống.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Nhớ mật khẩu?")]
        public bool RememberMe { get; set; }
    }
}
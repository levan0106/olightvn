using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace olightvn.Models
{
    public class AddUserModel
    {
        [Display(Name = "Tên đăng nhập")]
        public string UserName { get; set; }

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "{0} không được để trống.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Nhập lại Mật khẩu")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "{0} và {1} không trùng khớp.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Giới tính")]
        public int Sex { get; set; }

        [Display(Name = "Email")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Đổi mật khẩu")]
        public bool ChangePassword { get; set; }

        [Display(Name = "Role")]
        public string Role { get; set; }

        public int ActiveStatus { get; set; }
    }
}
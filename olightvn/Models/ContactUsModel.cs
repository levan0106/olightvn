using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace olightvn.Models
{
    public class ContactUsModel
    {
        [Display(Name = "Tên:")]
        [Required]
        public string Name { get; set; }
        [Display(Name = "Email:")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Display(Name = "Tiêu đề:")]
        [Required]
        public string Subject { get; set; }
        [Display(Name = "Nội dung:")]
        [Required]
        public string Content { get; set; }
    }
}
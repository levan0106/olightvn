using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace olightvn.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        [Display(Name = "Tên sản phẩm")]
        [Required]
        public string Name { get; set; }
        public string UserId { get; set; }
        [Display(Name = "Loại sản phẩm")]
        [Required]
        public int CatId { get; set; }
        public int? LocationId { get; set; }
        public string Thumbnail { get; set; }
        //public List<Image> Images { get; set; }
        //public string Title { get { return T.Core.Common.Converter.ToUnSign(Name); } set{} }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string StartDTS { get; set; }
        public string EndDTS { get; set; }
        public double Price { get; set; }
        //public double Deposit { get; set; }
        //public double Rating { get; set; }
        //public int? View { get; set; }
        //public int? ApprovedStatus { get; set; }
        //public string ApprovedBy { get; set; }
        //public string ApprovedComment { get; set; }
        public string Unit { get; set; }
        public int? Quantity { get; set; }
        public int? ProductType { get; set; }
        public double PriceOff { get; set; }
        //public string ProductStatus { get; set; }
        public int? BrandId { get; set; }
        public int? OriginId { get; set; }
        //public int Shipping { get; set; }
        public string Signature { get; set; }
        public List<string> Tags { get; set; }
        public int? ActiveStatus { get; set; }
    }
}
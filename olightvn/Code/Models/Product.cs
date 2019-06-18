using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace olightvn.Models
{
    public class Product : Base
    {
        public string UserId { get; set; }
        public int CatId { get; set; }
        public string CategoryName { get; set; }
        public int? LocationId { get; set; }
        public string LocationName { get; set; }
        public string Thumbnail { get; set; }
        public List<Image> Image { get; set; }
        public string Title { get { return T.Core.Common.Converter.ToUnSign(Name); } set { } }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string StartDTS { get; set; }
        public string EndDTS { get; set; }
        public double Price { get; set; }
        public double Deposit { get; set; }
        public double Rating { get; set; }
        public int? View { get; set; }
        public int? ApprovedStatus { get; set; }
        public string ApprovedBy { get; set; }
        public string ApprovedComment { get; set; }
        public int? SaleBy { get; set; }
        public string Unit { get; set; }
        public int? Quantity { get; set; }
        public int? ProductType { get; set; }
        public double PriceOff { get; set; }
        public string ProductStatus { get; set; }
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public int OriginId { get; set; }
        public string OriginName { get; set; }
        public int Shipping {get;set;}
        public string Signature { get; set; }
        public List<string> Tags { get; set; }

    }    
   
}

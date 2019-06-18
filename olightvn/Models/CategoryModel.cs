using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace olightvn.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public string Thumbnail { get; set; }
        public string Description { get; set; }
        public int? SortOrder { get; set; }
        public int ShowHomePage { get; set; }
        public int ShowMenu { get; set; }
        public int? ActiveStatus { get; set; }
    }
}
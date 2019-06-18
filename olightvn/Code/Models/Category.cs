using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace olightvn.Models
{
    public class Category:Base
    {
        public int ParentId { get; set; }
        public string Thumbnail { get; set; }
        public string Description { get; set; }
        public int SortOrder { get; set; }
        public string NameUnSign { get { return T.Core.Common.Converter.ToUnSign(Name); } }
        public int ShowHomePage { get; set; }
        public int ShowMenu { get; set; }
        public string IdChilds { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace olightvn.Models
{
    public class SiteMap:Base
    {
        public string URL { get; set; }
        public string PermissionCode { get; set; }
        public int ParentId { get; set; }
        public int RefTableId { get; set; }
        public int CategoryId { get; set; }
        public int SortIndex { get; set; }
        public string SitemapCode { get; set; }
        public string Thumbnail { get; set; }
        public string Description { get; set; }
    }
}

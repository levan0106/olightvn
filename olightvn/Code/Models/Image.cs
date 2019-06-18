using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace olightvn.Models
{
    public class Image:Base
    {
        public int ProductId { get; set; }
        public string Path { get; set; }
        public string Signature { get; set; }
        public bool IsSelected { get; set; }
        public string Title { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}

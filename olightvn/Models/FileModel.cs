using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace olightvn.Models
{
    public class FileModel
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public int Size { get; set; }
        public string Base64 { get; set; }
        public bool Selected { get; set; }
    }
}
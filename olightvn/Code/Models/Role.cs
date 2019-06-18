using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace olightvn.Models
{
    public class Role:Base
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public string Group { get; set; }
    }
}
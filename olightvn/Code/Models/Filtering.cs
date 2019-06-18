using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace olightvn.Models
{
    public class Filtering:Paging
    {
        public string Value { get; set; }
        public int Category { get; set; }
        public int Location { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace olightvn.Models
{
    public class Rating : Base
    {
        public int ProductId { get; set; }
        public double Level { get; set; }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace olightvn.Models
{
    public abstract class Base
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CreateDTS { get; set; }
        public string UpdateDTS { get; set; }
        public Guid? CreateBy { get; set; }
        public Guid? UpdateBy { get; set; }
        public int? ActiveStatus { get; set; }
    }
}

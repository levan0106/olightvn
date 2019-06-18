using olightvn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace olightvn.Repositories
{
    public interface IBreadCrumbRepository : IRepository<BreadCrumb>
    {
        IEnumerable<BreadCrumb> GetData(object id, string type);
    }
}

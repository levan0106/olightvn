using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using olightvn.Models;

namespace olightvn.Repositories
{
    public interface IPermissionRepository:IRepository<Permission>
    {
        IEnumerable<Permission> GetPermissionByUser(string userName);
        IEnumerable<Permission> GetPermissionByRole(string roleName);
    }
}

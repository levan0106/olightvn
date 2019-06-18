using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using olightvn.Models;
using Dapper;
using T.Core.Authenticate;

namespace olightvn.Repositories
{
    public class PermissionRepository:BaseRepository,IPermissionRepository
    {
        public IEnumerable<Permission> GetPermissionByUser(string userName)
        {
            using (var connection = GetConnection())
            {
                return connection.Query<Permission>(@"
                   sp_GetPermissionByUser @UserName=@userName
                ".AddParametersDefaul(), new { userName }).ToList();

            }
        }

        public IEnumerable<Permission> GetPermissionByRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Permission> GetAll()
        {
            throw new NotImplementedException();
        }

        public Permission GetInfo(object id)
        {
            throw new NotImplementedException();
        }

        public bool Insert(Permission entity, string userLogin)
        {
            throw new NotImplementedException();
        }

        public bool Delete(object id, string userLogin)
        {
            throw new NotImplementedException();
        }

        public bool Update(Permission entity, string userLogin)
        {
            throw new NotImplementedException();
        }
    }
}

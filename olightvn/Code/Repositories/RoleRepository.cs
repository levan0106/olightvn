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
    public class RoleRepository:BaseRepository, IRoleRepository
    {
        public IEnumerable<Role> GetAll()
        {
            using(var connection = GetConnection())
            {
                return connection.Query<Role>(@" sp_GetAllRoles".AddParametersDefaul(firstParam:true)).ToList();
            }
        }

        public Role GetInfo(object id)
        {
            throw new NotImplementedException();
        }

        public bool Insert(Role entity, string userLogin)
        {
            throw new NotImplementedException();
        }

        public bool Delete(object id, string userLogin)
        {
            throw new NotImplementedException();
        }

        public bool Update(Role entity, string userLogin)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Role> GetRoleByUser(string userName, UserType userType)
        {
            using (var connection = GetConnection())
            {
                return connection.Query<Role>(@"
                    sp_GetRoleByUser @UserName=@userName, @UserType=@userType                    
                ".AddParametersDefaul(), new { userName, userType }).ToList();
            }
        }
    }
}

using olightvn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace olightvn.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        User Authenticate(string userName, string password);
        IEnumerable<User> GetAllByParent(string userName);
        IEnumerable<User> GetAllByRole(string roleName);
        IEnumerable<User> GetAllByPermission(string permissionName);
        IEnumerable<User> GetAll(Paging paging);
        int GetAll_ToTal(Paging paging);
        bool ValidateUserEmail(string userName, string email);
    }
}

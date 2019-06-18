using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T.Core.Authenticate;

namespace olightvn.Repositories
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetInfo(object id);
        bool Insert(T entity,string userLogin);
        bool Delete(object id, string userLogin);
        bool Update(T entity, string userLogin);


    }
}

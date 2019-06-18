using olightvn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace olightvn.Repositories
{
    public interface ICategoryRepository:IRepository<Category>
    {
        IEnumerable<Category> GetAllForHomePage();
        IEnumerable<Category> GetAll(Paging paging);
        int GetAll_ToTal(Paging paging);
    }
}

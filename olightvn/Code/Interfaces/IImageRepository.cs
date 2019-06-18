using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using olightvn.Models;

namespace olightvn.Repositories
{
    public interface IImageRepository:IRepository<Image>
    {
        IEnumerable<Image> GetAll(int proId);
        bool DeleteImageByProduct(int proId);
    }
}

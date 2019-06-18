using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using olightvn.Models;
using T.Core.Authenticate;

namespace olightvn.Repositories
{
    public interface IBasketRepository: IRepository<Basket>
    {
        bool Insert(int productId, string userLogin);
        bool Delete(int productId, string userLogin);
        bool Update(ProductInBasket product, string userLogin);
    }
}

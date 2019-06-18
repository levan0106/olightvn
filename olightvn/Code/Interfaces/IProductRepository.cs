using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using olightvn.Models;
using T.Core.Authenticate;

namespace olightvn.Repositories
{
    public interface IProductRepository: IRepository<Product>
    {
        IEnumerable<Product> GetProductMarked(string userName, Paging paging);
        bool RemoveProductMarked(int proId, string userName);
        bool Marked(int proId, string userName);
        IEnumerable<Product> SearchBy(Filtering value);
        int SearchBy_Total(Filtering value);
        IEnumerable<Product> GetAllByUser(string userName, Paging paging);
        Product ApproveProduct(Product entity, string userLogin);
        IEnumerable<Product> GetAllByCategory(int categoriId, Paging paging, int status);
        int GetAllByCategory_Total(int categoriId, Paging paging, int status);
        IEnumerable<Product> GetAll(Paging paging);
        int GetAll_Total(Paging paging);
        IEnumerable<Product> GetRelatedItems(int id, Paging paging);
        int GetRelatedItems_Total(int id, Paging paging);
        Product InsertProduct(Product entity, string userLogin);
    }
}

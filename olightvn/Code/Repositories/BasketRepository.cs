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
    public class BasketRepository:BaseRepository,IBasketRepository
    {

        public IEnumerable<Basket> GetAll()
        {
            throw new NotImplementedException();
        }

        public Basket GetInfo(object id)
        {
            using(var connection = GetConnection())
            {
                Basket basket = connection.Query<Basket>(@"
                                 exec sp_GetBasketInfo @UserName=@id
                                ".AddParametersDefaul(), new { id }).FirstOrDefault();

                if (basket.Id > 0)
                    basket.Products = connection.Query<ProductInBasket>(@"
                                        exec sp_GetBasketDetail @BasketId=@Id
                                        ".AddParametersDefaul(), basket.Id);
                return basket;
            }
        }

        public bool Insert(Basket entity, string userLogin)
        {
            throw new NotImplementedException();
        }

        public bool Insert(int productId, string userLogin)
        {
            using (var connection = GetConnection())
            {
                return connection.Query(@"
                exec sp_InsertProductIntoBasket @UserName=@UserName, @ProductId=@productId
                ".AddParametersDefaul(), new { productId , userLogin}).Any();
            }
        }

        public bool Delete(object id, string userLogin)
        {
            throw new NotImplementedException();
        }
        public bool Delete(int productId, string userLogin)
        {
            using (var connection = GetConnection())
            {
                return connection.Query(@"
                exec sp_DeleteProductInBasket @UserName=@UserName, @ProductId=@productId
                ".AddParametersDefaul(), new { productId, userLogin }).Any();
            }
        }

        public bool Update(Basket entity, string userLogin)
        {
            using (var connection = GetConnection())
            {
                return connection.Query(@"
                exec sp_UpdateBasket @BasketId=@Id, @OrdererName=@OrdererName, @OrdererPhone=@OrdererPhone, @ReceiverName=@ReceiverName
                , @ReceiverAddress=@ReceiverAddress, @ReceiverPhone=@ReceiverPhone, @ReceiveLocation=@ReceiveLocation, @Note=@Note
                ".AddParametersDefaul(), new { entity }).Any();
            }
        }

        public bool Update(ProductInBasket product, string userLogin)
        {
            using (var connection = GetConnection())
            {
                return connection.Query(@"
                exec sp_UpdateProductInBasket @UserName=@UserName, @ProductId=@Id, @StartDate=@StartDTS, @EndDate=@EndDTS, @Quantity=@Quantity
                ".AddParametersDefaul(), new { product, userLogin }).Any();
            }
        }
    }
}

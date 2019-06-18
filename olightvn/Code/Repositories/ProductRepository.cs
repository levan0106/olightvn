using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using olightvn.Models;
using T.Core.Authenticate;
using Dapper;
using T.Core.Common;

namespace olightvn.Repositories
{
    public class ProductRepository:BaseRepository,IProductRepository
    {

        public IEnumerable<Product> GetProductMarked(string userName, Paging paging)
        {
            using (var connection = GetConnection())
            {
                return connection.Query<Product>(@"
                EXEC sp_GetProductMarked @UserName=@userName, @PageSize=@PageSize,@Page=@CurrentPage,@GetTotalRow=@GetTotalRow,@SortBy=@SortBy
                ".AddParametersDefaul(), Merger.Merge(userName, paging)).ToList();
            }
        }

        public bool RemoveProductMarked(int proId, string userName)
        {
            using (var connection = GetConnection())
            {
                return connection.Query(@"
                EXEC sp_RemoveProductInMarked @ProductId=@proId, @UserName=@userName
                ".AddParametersDefaul(), Merger.Merge(proId, userName)).Any();
            }
        }

        public bool Marked(int proId, string userName)
        {
            using (var connection = GetConnection())
            {
                return connection.Query(@"
                EXEC sp_InsertProductToMarked @ProductId=@proId, @UserName=@userName
                ".AddParametersDefaul(), Merger.Merge(proId, userName)).Any();
            }
        }

        public IEnumerable<Product> GetAllByUser(string userName, Paging paging)
        {
            using (var connection = GetConnection())
            {
                return connection.Query<Product>(@"
                EXEC sp_GetAllProductByUser @UserName=@userName, @PageSize=@PageSize,@Page=@CurrentPage,@GetTotalRow=@GetTotalRow,@SortBy=@SortBy
                ".AddParametersDefaul(), Merger.Merge(userName, paging)).ToList();
            }
        }
        public IEnumerable<Product> GetAll(Paging paging)
        {
            using (var connection = GetConnection())
            {
                return connection.Query<Product>(@"
                EXEC sp_GetAllProduct @PageSize=@PageSize,@Page=@CurrentPage,@GetTotalRow=@GetTotalRow,@SortBy=@SortBy
                ".AddParametersDefaul(), paging).ToList();
            }
        }
        public int GetAll_Total(Paging paging)
        {
            using (var connection = GetConnection())
            {
                return connection.Query<int>(@"
                EXEC sp_GetAllProduct @PageSize=@PageSize,@Page=@CurrentPage,@GetTotalRow=@GetTotalRow,@SortBy=@SortBy
                ".AddParametersDefaul(), paging).FirstOrDefault();
            }
        }
        public IEnumerable<Product> GetAllByCategory(int categoriId, Paging paging, int status)
        {
            using (var connection = GetConnection())
            {
                return connection.Query<Product>(@"
                EXEC sp_GetAllProductByCategory @CategoryId=@categoriId, @PageSize=@PageSize,@Page=@CurrentPage,@GetTotalRow=@GetTotalRow,@SortBy=@SortBy,@Status=@status
                ".AddParametersDefaul(), new { categoriId, paging.PageSize, paging.CurrentPage, paging.GetTotalRow, paging.SortBy, status }).ToList();
            }
        }
        public int GetAllByCategory_Total(int categoriId, Paging paging, int status)
        {
            using (var connection = GetConnection())
            {
                return connection.Query<int>(@"
                EXEC sp_GetAllProductByCategory @CategoryId=@categoriId, @PageSize=@PageSize,@Page=@CurrentPage,@GetTotalRow=@GetTotalRow,@SortBy=@SortBy,@Status=@status
                ".AddParametersDefaul(), new { categoriId, paging.PageSize, paging.CurrentPage, paging.GetTotalRow, paging.SortBy, status }).FirstOrDefault();
            }
        }

        public IEnumerable<Product> GetRelatedItems(int id, Paging paging)
        {
            using (var connection = GetConnection())
            {
                return connection.Query<Product>(@"
                EXEC sp_GetRelatedItems @Id=@id, @PageSize=@PageSize,@Page=@CurrentPage,@GetTotalRow=@GetTotalRow,@SortBy=@SortBy
                ".AddParametersDefaul(), new { id, paging.PageSize, paging.CurrentPage, paging.GetTotalRow, paging.SortBy }).ToList();
            }
        }
        public int GetRelatedItems_Total(int id, Paging paging)
        {
            using (var connection = GetConnection())
            {
                return connection.Query<int>(@"
                EXEC sp_GetRelatedItems @Id=@id, @PageSize=@PageSize,@Page=@CurrentPage,@GetTotalRow=@GetTotalRow,@SortBy=@SortBy
                ".AddParametersDefaul(), new { id, paging.PageSize, paging.CurrentPage, paging.GetTotalRow, paging.SortBy }).FirstOrDefault();
            }
        }

        public Product GetInfo(object id)
        {
            using (var connection = GetConnection())
            {
                var result= connection.Query<Product>(@"
                EXEC sp_GetProduct @ProductId=@id
                ".AddParametersDefaul(), new { id }).FirstOrDefault();
                return result;
            }
        }

        public bool Insert(Product entity, string userLogin)
        {
//            using (var connection = GetConnection())
//            {
//                return connection.Query(@"
//                EXEC sp_InsertProduct @UserName=null
//                                    ,@Id=@Id
//                                    ,@ProductName=@Name
//                                    ,@Title=@Title
//                                    ,@CatId=@CatId
//                                    ,@LocationId=@LocationId
//                                    ,@Thumbnail=@Thumbnail
//                                    ,@ShortDescription=@ShortDescription
//                                    ,@Description=@Description
//                                    ,@StartDTS=@StartDTS
//                                    ,@EndDTS=@EndDTS
//                                    ,@Price=@Price
//                                    ,@Deposit=@Deposit
//                                    ,@SaleBy=@SaleBy
//                                    ,@ActiveStatus=@ActiveStatus
//                                    ,@Unit=@Unit
//                                    ,@Quantity=@Quantity
//                                    ,@ProductType=@ProductType
//                                    ,@PriceOff=@PriceOff
//                ".AddParametersDefaul(), entity).Any();
//            }
            throw new NotImplementedException();
        }
        public Product InsertProduct(Product entity, string userLogin)
        {
            using (var connection = GetConnection())
            {
                return connection.Query<Product>(@"
                EXEC sp_InsertProduct @UserName=null
                                    ,@Id=@Id
                                    ,@ProductName=@Name
                                    ,@Title=@Title
                                    ,@CatId=@CatId
                                    ,@LocationId=@LocationId
                                    ,@Thumbnail=@Thumbnail
                                    ,@ShortDescription=@ShortDescription
                                    ,@Description=@Description
                                    ,@StartDTS=@StartDTS
                                    ,@EndDTS=@EndDTS
                                    ,@Price=@Price
                                    ,@Deposit=@Deposit
                                    ,@SaleBy=@SaleBy
                                    ,@ActiveStatus=@ActiveStatus
                                    ,@Unit=@Unit
                                    ,@Quantity=@Quantity
                                    ,@ProductType=@ProductType
                                    ,@PriceOff=@PriceOff
                                    ,@BrandId=@BrandId
                                    ,@OriginId=@OriginId
                ".AddParametersDefaul(), entity).FirstOrDefault();
            }
        }

        public bool Delete(object id, string userLogin)
        {
            using (var connection = GetConnection())
            {
                return connection.Query(@"
                EXEC sp_DeleteProduct @ProductId=@id
                ".AddParametersDefaul(), new { id }).Any();
            }
        }

        public bool Update(Product entity, string userLogin)
        {
            using (var connection = GetConnection())
            {
                return connection.Query(@"
                EXEC sp_UpdateProduct @ProductId=@Id
                                    ,@CatId=@CatId
                                    ,@ProductName=@Name
                                    ,@LocationId=@LocationId
                                    ,@Thumbnail=@Thumbnail
                                    ,@Title=@Title
                                    ,@ShortDescription=@ShortDescription
                                    ,@Description=@Description
                                    ,@StartDTS=@StartDTS
                                    ,@EndDTS=@EndDTS
                                    ,@Price=@Price
                                    ,@Deposit=@Deposit
                                    ,@SaleBy=@SaleBy
                                    ,@ActiveStatus=@ActiveStatus
                                    ,@UpdateBy=@UserName
                                    ,@UserType=@UserType
                                    ,@Unit=@Unit
                                    ,@Quantity=@Quantity
                                    ,@ProductType=@ProductType
                                    ,@PriceOff=@PriceOff
                                    ,@BrandId=@BrandId
                                    ,@OriginId=@OriginId
                ".AddParametersDefaul(), Merger.Merge(entity, userLogin)).Any();
            }
        }

        public Product ApproveProduct(Product entity, string userLogin)
        {
            using (var connection = GetConnection())
            {
                return connection.Query<Product>(@"
                EXEC sp_UpdateProduct @ProductId=@Id
                                    ,@ApprovedStatus=@ApprovedStatus
                                    ,@ApprovedBy=@UserRecId
                                    ,@ApprovedComment=@ApprovedComment
                                    ,@UpdateBy=@UserName
                                    ,@UserType=@UserType
                ".AddParametersDefaul(), Merger.Merge(entity, userLogin)).FirstOrDefault();
            }
        }


        public IEnumerable<Product> SearchBy(Filtering value)
        {
            using (var connection = GetConnection())
            {
                
                return connection.Query<Product>(@"
                EXEC sp_GetAllProductByText @Text=@value, @Location=@Location,@Category=@Category, @PageSize=@PageSize,@Page=@CurrentPage,@GetTotalRow=@GetTotalRow,@SortBy=@SortBy
                ".AddParametersDefaul(), value).ToList();
            }
        }
        public int SearchBy_Total(Filtering value)
        {
            using (var connection = GetConnection())
            {

                return connection.Query<int>(@"
                EXEC sp_GetAllProductByText @Text=@value, @Location=@Location,@Category=@Category, @PageSize=@PageSize,@Page=@CurrentPage,@GetTotalRow=@GetTotalRow,@SortBy=@SortBy
                ".AddParametersDefaul(), value).FirstOrDefault();
            }
        }

        public IEnumerable<Product> GetAll()
        {
            throw new NotImplementedException();
        }
        
    }

}

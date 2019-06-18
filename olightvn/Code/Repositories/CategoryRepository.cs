using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using olightvn.Models;
using T.Core.Authenticate;
using Dapper;

namespace olightvn.Repositories
{
    public class CategoryRepository : BaseRepository, ICategoryRepository
    {
        public Category Category;
        public CategoryRepository()
        {
            if (Category == null)
                Category = new Category();
        }
        public IEnumerable<Category> GetAll()
        {
            using (var connection = GetConnection())
            {
                return connection.Query<Category>(@"
                EXEC sp_GetAllCategory
                ".AddParametersDefaul(firstParam:true)).ToList();
            }
        }
        public IEnumerable<Category> GetAllForHomePage()
        {
            using (var connection = GetConnection())
            {
                return connection.Query<Category>(@"
                EXEC sp_GetAllCategoryForHomePage
                ".AddParametersDefaul(firstParam: true)).ToList();
            }
        }

        public Category GetInfo(object id)
        {
            using (var connection = GetConnection())
            {
                return connection.Query<Category>(@"
                EXEC sp_GetCategoryInfo @CategoryId=@id
                ".AddParametersDefaul(), new { id}).FirstOrDefault();
            }
        }

        public bool Insert(Category entity, string userLogin)
        {
            using (var connection = GetConnection())
            {
                return connection.Query(@"
                EXEC sp_InsertCategory @Id=@Id
                                    ,@Name=@Name 
                                    ,@NameUnsign=@NameUnsign                                   
                                    ,@ParentId=@ParentId
                                    ,@Thumbnail=@Thumbnail
                                    ,@Description=@Description
                                    ,@ShowMenu=@ShowMenu
                                    ,@ShowHomePage=@ShowHomePage
                                    ,@ActiveStatus=@ActiveStatus
                ".AddParametersDefaul(), entity).Any();
            }
        }

        public bool Delete(object id, string userLogin)
        {
            using (var connection = GetConnection())
            {
                return connection.Query(@"
                EXEC sp_DeleteCategory @Id=@id
                ".AddParametersDefaul(), new { id }).Any();
            }
        }

        public bool Update(Category entity, string userLogin)
        {
            using (var connection = GetConnection())
            {
                return connection.Query(@"
                EXEC sp_InsertCategory @Id=@Id
                                    ,@Name=@Name                                    
                                    ,@ParentId=@ParentId
                                    ,@Image=@Image
                                    ,@Description=@Description
                                    ,@ShowMenu=@ShowMenu
                                    ,@ShowHomePage=@ShowHomePage
                                    ,@ActiveStatus=@ActiveStatus
                ".AddParametersDefaul(), entity).Any();
            }
        }


        public IEnumerable<Category> GetAll(Paging paging)
        {
            using (var connection = GetConnection())
            {
                return connection.Query<Category>(@"
                EXEC sp_GetAllCategory @PageSize=@PageSize,@Page=@CurrentPage,@GetTotalRow=@GetTotalRow,@SortBy=@SortBy
                ".AddParametersDefaul(), new { paging.PageSize, paging.CurrentPage, paging.GetTotalRow, paging.SortBy }).ToList();
            }
        }

        public int GetAll_ToTal(Paging paging)
        {
            using (var connection = GetConnection())
            {
                return connection.Query<int>(@"
                EXEC sp_GetAllCategory @PageSize=@PageSize,@Page=@CurrentPage,@GetTotalRow=@GetTotalRow,@SortBy=@SortBy
                ".AddParametersDefaul(), new {paging.PageSize, paging.CurrentPage, paging.GetTotalRow, paging.SortBy }).FirstOrDefault();
            }
        }
    }
}

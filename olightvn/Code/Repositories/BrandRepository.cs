using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using olightvn.Models;
using Dapper;

namespace olightvn.Repositories
{
    public class BrandRepository:BaseRepository, IBrandRepository
    {
        public IEnumerable<Brand> GetAll()
        {
            using (var connection = GetConnection())
            {
                return connection.Query<Brand>(@"sp_GetAllBrand".AddParametersDefaul(firstParam:true)).ToList();
            }
        }

        public Brand GetInfo(object id)
        {
            using (var connection = GetConnection())
            {
                return connection.Query<Brand>(@"sp_GetBrandInfo @Id=@id".AddParametersDefaul(), new { id }).FirstOrDefault();
            }
        }

        public bool Insert(Brand entity, string userLogin)
        {
            using (var connection = GetConnection())
            {
                return connection.Query(@"sp_InsertBrand @Id=@Id
                                    ,@Name=@Name         
                                    ,@Thumbnail=@Thumbnail
                                    ,@Description=@Description
                                    ,@ActiveStatus=@ActiveStatus".AddParametersDefaul(), entity).Any();
            }
        }

        public bool Delete(object id, string userLogin)
        {
            using (var connection = GetConnection())
            {
                return connection.Query(@"
                EXEC sp_DeleteBrand @Id=@id
                ".AddParametersDefaul(), new { id }).Any();
            }
        }

        public bool Update(Brand entity, string userLogin)
        {
            using (var connection = GetConnection())
            {
                return connection.Query(@"sp_InsertOrigin @Id=@Id
                                    ,@Name=@Name         
                                    ,@Thumbnail=@Thumbnail
                                    ,@Description=@Description
                                    ,@ActiveStatus=@ActiveStatus".AddParametersDefaul(), entity).Any();
            }
        }
    }
}
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
    public class ImageRepository:BaseRepository,IImageRepository
    {
        public IEnumerable<Image> GetAll(int proId)
        {

            using (var connection = GetConnection())
            {
                return connection.Query<Image>(@"
                EXEC sp_GetAllImageByProduct @Id=@proId
                ".AddParametersDefaul(), new { proId}).ToList();
            }
        }

        public IEnumerable<Image> GetAll()
        {
            throw new NotImplementedException();
        }

        public Image GetInfo(object id)
        {
            throw new NotImplementedException();
        }

        public bool Insert(Image entity, string userLogin)
        {
            using (var connection = GetConnection())
            {
                return connection.Query(@"
                EXEC sp_InsertImage @UserName = null,@IsSelected=@IsSelected,@Name=@Name ,@ProductId=@ProductId,@Path=@Path,@ActiveStatus=@ActiveStatus
                ".AddParametersDefaul(), entity).Any();
            }
        }

        public bool Delete(object id, string userLogin)
        {
            using (var connection = GetConnection())
            {
                return connection.Query(@"
                EXEC sp_DeleteImage @Id=@id
                ".AddParametersDefaul(), new { id }).Any();
            }
        }

        public bool Update(Image entity, string userLogin)
        {
            using (var connection = GetConnection())
            {
                return connection.Query(@"
                EXEC sp_UpdateImage @Id=@Id, @Name=@Name, @Path=@Path, @ActiveStatus=@ActiveStatus, @Title=@Title
                ".AddParametersDefaul(), entity).Any();
            }
        }


        public bool DeleteImageByProduct(int proId)
        {
            using (var connection = GetConnection())
            {
                return !connection.Query(@"
                EXEC sp_DeleteImageByProduct @Id=@proId
                ".AddParametersDefaul(), new { proId }).Any();
            }
        }
    }
}

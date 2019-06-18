using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using olightvn.Models;
using Dapper;

namespace olightvn.Repositories
{
    public class OriginRepository: BaseRepository, IOriginRepository
    {
        public IEnumerable<Origin> GetAll()
        {
            using (var connection = GetConnection())
            {
                return connection.Query<Origin>(@"sp_GetAllOrigin".AddParametersDefaul(firstParam: true)).ToList();
            }
        }

        public Origin GetInfo(object id)
        {
            using (var connection = GetConnection())
            {
                return connection.Query<Origin>(@"sp_GetOriginInfo @Id=@id".AddParametersDefaul(), new { id }).FirstOrDefault();
            }
        }

        public bool Insert(Origin entity, string userLogin)
        {
            using (var connection = GetConnection())
            {
                return connection.Query(@"sp_InsertOrigin @Id=@Id
                                    ,@Name=@Name         
                                    ,@Description=@Description
                                    ,@ActiveStatus=@ActiveStatus".AddParametersDefaul(), entity).Any();
            }
        }

        public bool Delete(object id, string userLogin)
        {
            using (var connection = GetConnection())
            {
                return connection.Query(@"
                EXEC sp_DeleteOrigin @Id=@id
                ".AddParametersDefaul(), new { id }).Any();
            }
        }

        public bool Update(Origin entity, string userLogin)
        {
            using (var connection = GetConnection())
            {
                return connection.Query(@"sp_InsertOrigin @Id=@Id
                                    ,@Name=@Name         
                                    ,@Description=@Description
                                    ,@ActiveStatus=@ActiveStatus".AddParametersDefaul(), entity).Any();
            }
        }
    }
}
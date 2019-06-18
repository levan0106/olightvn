using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using olightvn.Models;
using Dapper;

namespace olightvn.Repositories
{
    public class HashtagRepository:BaseRepository, ITag
    {
        public IEnumerable<Tag> GetAll()
        {
            using(var connection = GetConnection())
            {
                var result = connection.Query<Tag>(@"sp_GetAllTag".AddParametersDefaul(firstParam: true)).ToList();
               return result;
            }
        }

        public Tag GetInfo(object id)
        {
            throw new NotImplementedException();
        }

        public bool Insert(Tag entity, string userLogin)
        {
            throw new NotImplementedException();
        }

        public bool Delete(object id, string userLogin)
        {
            throw new NotImplementedException();
        }

        public bool Update(Tag entity, string userLogin)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Tag> GetAllByProduct(object id)
        {
            using (var connection = GetConnection())
            {
                var result = connection.Query<Tag>(@"sp_GetAllTagByProduct @Id=@id".AddParametersDefaul(), new { id}).ToList();
                return result;
            }
        }

        public bool UpdateTagByProduct(object id, string tags)
        {
            using (var connection = GetConnection())
            {
                return connection.Query(@"sp_UpdateTagByProduct @ProId=@id, @Tags=@tags".AddParametersDefaul(), new { id, tags }).Any();
                
            }
        }
    }
}
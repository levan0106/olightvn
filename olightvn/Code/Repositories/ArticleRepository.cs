using olightvn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;

namespace olightvn.Repositories
{
    public class ArticleRepository:BaseRepository, IArticle
    {

        public IEnumerable<Article> GetAll()
        {
            throw new NotImplementedException();
        }

        public Article GetInfo(object id)
        {
            using (var connection = GetConnection())
            {
                return connection.Query<Article>(@"
                    sp_GetArticle @Id=@id
                ".AddParametersDefaul(), new { id }).FirstOrDefault();
            }
        }

        public bool Insert(Article entity, string userLogin)
        {
            using (var connection = GetConnection())
            {
                return connection.Query(@"
                    sp_InsertArticle @Id=@id, @Name=@Name, @Content=@Content, @ActiveStatus=@ActiveStatus
                ".AddParametersDefaul(), entity).Any();
            }
        }

        public bool Delete(object id, string userLogin)
        {
            using (var connection = GetConnection())
            {
                return connection.Query(@"
                    sp_DeleteArticle @Id=@id
                ".AddParametersDefaul(), new { id }).Any();
            }
        }

        public bool Update(Article entity, string userLogin)
        {
            using (var connection = GetConnection())
            {
                return connection.Query(@"
                    sp_InsertArticle @Id=@id, @Name=@Name, @Content=@Content, @ActiveStatus=@ActiveStatus
                ".AddParametersDefaul(), entity ).Any();
            }
        }
    }
}
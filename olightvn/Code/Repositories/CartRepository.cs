using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using olightvn.Models;

namespace olightvn.Repositories
{
    public class CartRepository:BaseRepository, ICartRepository
    {
        public IEnumerable<Cart> GetAll()
        {
            throw new NotImplementedException();
        }

        public Cart GetInfo(object id)
        {
            using (var connection = GetConnection())
            {
                return connection.Query<Cart>(@"
                    sp_GetCart @Id=@id
                ".AddParametersDefaul(), new { id }).FirstOrDefault();
            }
        }

        public bool Insert(Cart entity, string userLogin)
        {
            using (var connection = GetConnection())
            {
                return connection.Query(@"
                    sp_InsertCart @Id=@id, @Name=@Name, @Content=@Content, @ActiveStatus=@ActiveStatus
                ".AddParametersDefaul(), entity).Any();
            }
        }

        public bool Delete(object id, string userLogin)
        {
            throw new NotImplementedException();
        }

        public bool Update(Cart entity, string userLogin)
        {
            throw new NotImplementedException();
        }
    }
}
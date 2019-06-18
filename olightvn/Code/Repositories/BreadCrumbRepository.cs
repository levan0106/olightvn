using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using olightvn.Models;
using Dapper;

namespace olightvn.Repositories
{
    public class BreadCrumbRepository : BaseRepository, IBreadCrumbRepository
    {
        public IEnumerable<BreadCrumb> GetAll()
        {
            throw new NotImplementedException();
        }

        public BreadCrumb GetInfo(object id)
        {
            throw new NotImplementedException();
        }

        public bool Insert(BreadCrumb entity, string userLogin)
        {
            throw new NotImplementedException();
        }

        public bool Delete(object id, string userLogin)
        {
            throw new NotImplementedException();
        }

        public bool Update(BreadCrumb entity, string userLogin)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BreadCrumb> GetData(object id, string type)
        {
            using (var connection = GetConnection())
            {
                return connection.Query<BreadCrumb>(@"
                EXEC sp_GetBreadCrumb @Id=@id, @Type=@type
                ".AddParametersDefaul(), new { id, type }).ToList();
            }
        }
    }
}
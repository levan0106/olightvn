using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using olightvn.Models;
using Dapper;

namespace olightvn.Repositories
{
    public class SiteRepository:BaseRepository, ISite
    {
        public IEnumerable<Models.Site> GetAll()
        {
            throw new NotImplementedException();
        }

        public Site GetInfo(object id)
        {
            using (var connection = GetConnection())
            {
                Site site = connection.Query<Site>(@"SELECT TOP 1 * FROM [dbo].[SEC_Site] where Id=@id", new { id }).FirstOrDefault();
                site.Configs = connection.Query<Configuration>(@"sp_GetAllConfigsBySiteId".AddParametersDefaul(firstParam: true, siteId: (int)id), new { id }).ToList();
                return site;
            }
        }

        public bool Insert(Site entity, string userLogin)
        {
            throw new NotImplementedException();
        }

        public bool Delete(object id, string userLogin)
        {
            throw new NotImplementedException();
        }

        public bool Update(Site entity, string userLogin)
        {
            throw new NotImplementedException();
        }

    }
}
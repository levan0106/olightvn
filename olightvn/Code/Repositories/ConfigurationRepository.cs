using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using olightvn.Models;

namespace olightvn.Repositories
{
    public class ConfigurationRepository: BaseRepository, IConfiguration
    {
        public IEnumerable<Configuration> GetAll()
        {
            throw new NotImplementedException();
        }

        public Configuration GetInfo(object id)
        {
            using (var connection = GetConnection())
            {
                return connection.Query<Configuration>(@"sp_GetConfigByKey @key=@id".AddParametersDefaul(), new { id }).FirstOrDefault();
            }
        }

        public bool Insert(Configuration entity, string userLogin)
        {
            using (var connection = GetConnection())
            {
                return connection.Query(@"sp_UpdateConfiguration @key=@Key, @value=@Value".AddParametersDefaul(), entity).Any();
            }
        }

        public bool Delete(object id, string userLogin)
        {
            using (var connection = GetConnection())
            {
                return connection.Query(@"sp_DeleteConfiguration @key=@key".AddParametersDefaul(), new { id }).Any();
            }
        }

        public bool Update(Configuration entity, string userLogin)
        {
            using (var connection = GetConnection())
            {
                return connection.Query(@"sp_UpdateConfiguration @key=@Key, @value=@Value".AddParametersDefaul(), entity).Any();
            }
        }
    }
}
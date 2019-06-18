using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using olightvn.Models;
using Dapper;
using T.Core.Authenticate;
using T.Core.Common;

namespace olightvn.Repositories
{
    public class SiteMapRepository:BaseRepository,ISiteMapRepository
    {
        public IEnumerable<SiteMap> GetSiteMap(string userLogin, int type)
        {
            using(var connection = GetConnection())
            {
                return connection.Query<SiteMap>(@"
                        exec sp_GetSiteMap @UserName=@userLogin, @Type=@type".AddParametersDefaul()
                    , new { userLogin, type });
            }
        }
        public IEnumerable<SiteMap> GetSiteMap(int type)
        {
            return GetSiteMap(null, type);
        }
    }
}

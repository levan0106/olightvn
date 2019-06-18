using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using olightvn.Models;
using T.Core.Authenticate;

namespace olightvn.Repositories
{
    public interface ISiteMapRepository
    {
        IEnumerable<SiteMap> GetSiteMap(string userLogin, int type);
        IEnumerable<SiteMap> GetSiteMap(int type);
    }
}

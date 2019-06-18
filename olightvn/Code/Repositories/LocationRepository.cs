using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using olightvn.Models;
using T.Core.Authenticate;

namespace olightvn.Repositories
{
    public class LocationRepository:BaseRepository, ILocationRepository
    {
        public IEnumerable<Location> GetAll()
        {
            throw new NotImplementedException();
        }

        public Location GetInfo(object id)
        {
            throw new NotImplementedException();
        }

        public bool Insert(Location entity,string userLogin)
        {
            throw new NotImplementedException();
        }

        public bool Delete(object id, string userLogin)
        {
            throw new NotImplementedException();
        }

        public bool Update(Location entity, string userLogin)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using olightvn.Models;
using T.Core.Authenticate;

namespace olightvn.Repositories
{
    public class RatingRepository:BaseRepository,IRatingRepository
    {
        public IEnumerable<Rating> GetAll(int proId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Rating> GetAll()
        {
            throw new NotImplementedException();
        }

        public Rating GetInfo(object id)
        {
            throw new NotImplementedException();
        }

        public bool Insert(Rating entity, string userLogin)
        {
            throw new NotImplementedException();
        }

        public bool Delete(object id, string userLogin)
        {
            throw new NotImplementedException();
        }

        public bool Update(Rating entity, string userLogin)
        {
            throw new NotImplementedException();
        }
    }
}

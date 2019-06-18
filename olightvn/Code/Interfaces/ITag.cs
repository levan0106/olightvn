using olightvn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace olightvn.Repositories
{
    public interface ITag: IRepository<Tag>
    {
        IEnumerable<Tag> GetAllByProduct(object id);

        bool UpdateTagByProduct(object id, string tags);
    }
}

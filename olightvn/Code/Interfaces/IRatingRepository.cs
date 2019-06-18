﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using olightvn.Models;

namespace olightvn.Repositories
{
    public interface IRatingRepository:IRepository<Rating>
    {
        IEnumerable<Rating> GetAll(int proId);
    }
}

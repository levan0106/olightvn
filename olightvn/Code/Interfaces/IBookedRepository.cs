﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using olightvn.Models;
using T.Core.Authenticate;

namespace olightvn.Repositories
{
    public interface IBookedRepository: IRepository<Booked>
    {
        IEnumerable<Booked> GetAllByUser(string userNanme);
        bool Insert(int id, string userLogin);
        bool UpdateProcess(Booked entity, string userLogin);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace olightvn.Models
{
    public class Paging
    {
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public bool GetTotalRow { get; set; }
        public int TotalRow { get; set; }
        public string SortBy { get; set; }
        public Paging()
        {
            GetTotalRow = false;
            PageSize = 20;
            CurrentPage = 1;
            SortBy = null;
        }
    }
}

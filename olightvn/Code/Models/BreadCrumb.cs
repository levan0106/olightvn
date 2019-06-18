using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace olightvn.Models
{
    public class BreadCrumb
    {
        public int Id
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        public string URL
        {
            get;
            set;
        }
        public int ParentId
        {
            get;
            set;
        }
    }
}
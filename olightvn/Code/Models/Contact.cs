using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace olightvn.Models
{
    public class Contact
    {
        public int ContactID
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }
        public string Address
        {
            get;
            set;
        }
        public string Fax
        {
            get;
            set;
        }

        public string Phone
        {
            get;
            set;
        }

        public string HotLine
        {
            get;
            set;
        }
        public string Email
        {
            get;
            set;
        }

        public string Facebook { get; set; }
        public string Youtube { get; set; }
        public string EmailSecond
        {
            get;
            set;
        }

        public string Support1
        {
            get;
            set;
        }

        public string Support2
        {
            get;
            set;
        }

        //public string ImageMap
        //{
        //    get;
        //    set;
        //}
        public string Map_Label
        {
            get;
            set;
        }

        public string Map_Lat
        {
            get;
            set;
        }
        public string Map_Lng
        {
            get;
            set;
        }

        public int Zoom { get; set; }

        public string Subject { get; set; }
        public string Content { get; set; }
    }
}
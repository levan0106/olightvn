using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace olightvn.Models
{
    public class Site
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ThemeName { get; set; }
        private string _title;
        public string Title { get { return _title; } set { Keyword = value; Description = value; _title = value; } }
        public string Keyword { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public List<Configuration> Configs { get; set; }
        public string Url { get; set; }
    }
}
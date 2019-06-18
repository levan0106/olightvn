using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace olightvn.Models
{
    public class EmailModel
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Password { get; set; }
        public int SMTPPort { get; set; }

        public int EnableSSL { get; set; }
    }
}
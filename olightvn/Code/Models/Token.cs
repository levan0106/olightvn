using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace olightvn.Models
{
    public class Token
    {
        public int TokenId { get; set; }
        public string UserId { get; set; }
        public string AuthToken { get; set; }
        public DateTime IssuedOn { get; set; }
        public DateTime ExpiresOn { get; set; }    
    }
}

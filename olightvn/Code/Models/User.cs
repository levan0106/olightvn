using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace olightvn.Models
{
    public class User:Base
    {
        public string RecId { get; set; }
        public string UserName { get; set; }        
        public string Password { get; set; }
        public string Role { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }  
        public string LastLoginDTS { get; set; }
        public string SecurityQuestion { get; set; }
        public string SecurityAnswer { get; set; }
        public int? AccessAttempt { get; set; }
        public int? Authenticate { get; set; }
        public string Address { get; set; }
        public int Sex { get; set; }
        
    }

}
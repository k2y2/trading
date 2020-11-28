 
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace trading.Models
{
    public partial class User
    {
        public int id { get; set; }

        [Display(Name = "User Name")]
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } 

        [Display(Name = "Modified")]
        public DateTime DateTimeModified { get; set; }

        [Display(Name = "Added")]
        public DateTime DateTimeAdded { get; set; }
    }
}

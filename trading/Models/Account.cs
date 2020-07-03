using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace trading.Models
{
    public partial class Account
    {
        public int id { get; set; }

        [Display(Name = "Account Holder")]
        [Required(ErrorMessage = "Field required")]
        [Remote("IsExist", "Account", AdditionalFields = "id", ErrorMessage = "Account exist already")]
        public string AccountName { get; set; }
          
        [Display(Name = "Modified")]
        public DateTime DateTimeModified { get; set; }

        [Display(Name = "Added")]
        public DateTime DateTimeAdded { get; set; }
    }

     

}

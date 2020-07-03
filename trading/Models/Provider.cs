using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace trading.Models
{
    public partial class Provider
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Field required")]
        [Remote("IsExist", "Provider", AdditionalFields = "id", ErrorMessage = "Provider exist already")]
        [Display(Name = "Provider")]
        public string ProviderName { get; set; }
          
        [Display(Name = "Modified")]
        public DateTime DateTimeModified { get; set; }

        [Display(Name = "Added")]
        public DateTime DateTimeAdded { get; set; }
    }
     
}

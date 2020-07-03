using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace trading.Models
{
    public partial class Country
    {
        public byte id { get; set; }

        [Display(Name = "Country")]
        [Required(ErrorMessage = "Field required")]
        [Remote("IsExist", "Country", AdditionalFields = "id", ErrorMessage = "Country exist already")]
        public string CountryName { get; set; }

        [Display(Name = "Modified")]
        public DateTime DateTimeModified { get; set; }

        [Display(Name = "Added")]
        public DateTime DateTimeAdded { get; set; }
    }
}

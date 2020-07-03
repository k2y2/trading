
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace trading.Models
{
    public partial class Currency
    {
        public byte id { get; set; }

        [Display(Name = "Currency")]
        [Required(ErrorMessage = "Field required")]
        [Remote("IsExist", "Currency", AdditionalFields = "id", ErrorMessage = "Currency exist already")]
        public string CurrencyName { get; set; }

        [Display(Name = "Modified")]
        public DateTime DateTimeModified { get; set; }

        [Display(Name = "Added")]
        public DateTime DateTimeAdded { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace trading.Models
{
    public partial class CurrencyPair
    {
        public short id { get; set; }

        [Display(Name = "Currency Pair")]
        [Required(ErrorMessage = "Field required")]
        [Remote("IsExist", "CurrencyPair", AdditionalFields = "id", ErrorMessage = "Currency Pair exist already")]
        public string CurrencyPairName { get; set; }

        [Display(Name = "Modified")]
        public DateTime DateTimeModified { get; set; }

        [Display(Name = "Added")]
        public DateTime DateTimeAdded { get; set; }
    }

    public partial class CurrencyPairView
    {
        public short id { get; set; }
        public string CurrencyPairName { get; set; }
        public short CurrencyPairID2 { get; set; }
        public string CurrencyPairName2 { get; set; }
        public byte? id1 { get; set; }
        public string CurrencyName1 { get; set; }
        public byte? id2 { get; set; }
        public string CurrencyName2 { get; set; }
    }
}

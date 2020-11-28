using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace trading.Models
{
    public partial class Dfr
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Field required")]
        [Display(Name = "Trade Date")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime TradeDate { get; set; }

        [Required(ErrorMessage = "Field required")]
        [Remote("IsExist", "Dfr", AdditionalFields = "id,TradeDate", ErrorMessage = "Currency Pair exist already")]
        [Display(Name = "Currency Pair")]
        public short CurrencyPairID { get; set; }

        [Required(ErrorMessage = "Field required")]
        [Display(Name = "Rate")]
        [DisplayFormat(DataFormatString = "{0:n7}", ApplyFormatInEditMode = true)]
        public decimal Rate { get; set; }

        [Display(Name = "Modified")]
        public DateTime DateTimeModified { get; set; }

        [Display(Name = "Added")]
        public DateTime DateTimeAdded { get; set; }
         
    }

    public partial class DfrView
    {
        public int id { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        [Display(Name = "Trade Date")]
        public DateTime TradeDate { get; set; }

        [Display(Name = "Currency Pair")]
        public short CurrencyPairID { get; set; }

        [Display(Name = "Rate")]
        [DisplayFormat(DataFormatString = "{0:n7}", ApplyFormatInEditMode = true)]
        public decimal Rate { get; set; }

        [Display(Name = "Modified")]
        public DateTime DateTimeModified { get; set; }

        [Display(Name = "Added")]
        public DateTime DateTimeAdded { get; set; }

        [Display(Name = "Currency Pair")]
        public string CurrencyPairName { get; set; }
    }
}

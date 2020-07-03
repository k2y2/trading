using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace trading.Models
{
    public partial class ProviderTradingProfile
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Field required")]
        [Display(Name = "Provider")]
        public int ProviderID { get; set; }

        [Required(ErrorMessage = "Field required")]
        [Remote("IsExist", "ProviderTradingProfile", AdditionalFields = "id", ErrorMessage = "Trading profile exist already")]
        [Display(Name = "Trading Profile Name")]
        public string ProviderTradingProfileName { get; set; }
         
        [Required(ErrorMessage = "Field required")]
        [Display(Name = "Currency Pair")]
        public short CurrencyPairID { get; set; }
          
        [Display(Name = "Modified")]
        public DateTime DateTimeModified { get; set; }

        [Display(Name = "Added")]
        public DateTime DateTimeAdded { get; set; }
    }

    public partial class ProviderTradingProfileView
    {
        public int id { get; set; }
         
        [Display(Name = "Provider")]
        public int ProviderID { get; set; }
         
        [Display(Name = "Trading Profile Name")]
        public string ProviderTradingProfileName { get; set; }
         
        [Display(Name = "Currency Pair")]
        public short CurrencyPairID { get; set; }

        [Display(Name = "Modified")]
        public DateTime DateTimeModified { get; set; }

        [Display(Name = "Added")]
        public DateTime DateTimeAdded { get; set; }

        [Display(Name = "Provider")]
        public string ProviderName { get; set; }

        [Display(Name = "Currency Pair")]
        public string CurrencyPairName { get; set; }
    }
}

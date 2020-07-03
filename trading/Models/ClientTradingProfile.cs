using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace trading.Models
{
    public partial class ClientTradingProfile
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Field required")]
        [Display(Name = "Client")]
        public int ClientID { get; set; }

        [Required(ErrorMessage = "Field required")]
        [Remote("IsExist", "ClientTradingProfile", AdditionalFields = "id,ClientID", ErrorMessage = "Trading profile exist already")]
        [Display(Name = "Trading Profile Name")]
        public string ClientTradingProfileName { get; set; }

        [Required(ErrorMessage = "Field required")]
        [Display(Name = "Currency Pair")]
        public short CurrencyPairID { get; set; }

        [Required(ErrorMessage = "Field required")]
        [Display(Name = "Price (%)")]
        [DisplayFormat(DataFormatString = "{0:n4}", ApplyFormatInEditMode = true)]
        public decimal Price { get; set; }

        [Display(Name = "Modified")]
        public DateTime DateTimeModified { get; set; }

        [Display(Name = "Added")]
        public DateTime DateTimeAdded { get; set; }
    }

    public partial class ClientTradingProfileView
    {
        public int id { get; set; }

        [Display(Name = "Client")]
        public int ClientID { get; set; }

        [Display(Name = "Trading Profile Name")]
        public string ClientTradingProfileName { get; set; }

        [Display(Name = "Currency Pair")]
        public short CurrencyPairID { get; set; }

        [Display(Name = "Price (%)")]
        [DisplayFormat(DataFormatString = "{0:n4}", ApplyFormatInEditMode = true)]
        public decimal Price { get; set; }

        [Display(Name = "Modified")]
        public DateTime DateTimeModified { get; set; }

        [Display(Name = "Added")]
        public DateTime DateTimeAdded { get; set; }

        [Display(Name = "Client")]
        public string ClientName { get; set; }

        [Display(Name = "Currency Pair")]
        public string CurrencyPairName { get; set; }

        [Display(Name = "Introducer Info")]
        public string IntroducerInfo { get; set; }
    }
}

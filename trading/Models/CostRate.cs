using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace trading.Models
{
    public partial class CostRate
    {
        public int id { get; set; }

        [Display(Name = "Trade Date")]
        [Required(ErrorMessage = "Field required")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime TradeDate { get; set; }

        [Display(Name = "Provider Trading Profile")]
        [Remote("IsExist", "CostRate", AdditionalFields = "id,TradeDate", ErrorMessage = "Provider trading profile exist already")]
        [Required(ErrorMessage = "Field required")]
        public int ProviderTradingProfileID { get; set; }

        [Display(Name = "Rate")]
        [DisplayFormat(DataFormatString = "{0:n6}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Field required")]
        public decimal Rate { get; set; }

        [Display(Name = "Modified")]
        public DateTime DateTimeModified { get; set; }

        [Display(Name = "Added")]
        public DateTime DateTimeAdded { get; set; }
    }

    public partial class CostRateView
    {
        public int id { get; set; }

        [Display(Name = "Trade Date")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime TradeDate { get; set; }

        [Display(Name = "Provider Trading Profile")]
        public int ProviderTradingProfileID { get; set; }

        [Display(Name = "Rate")]
        [DisplayFormat(DataFormatString = "{0:n6}", ApplyFormatInEditMode = true)]
        public decimal Rate { get; set; }

        [Display(Name = "Modified")]
        public DateTime DateTimeModified { get; set; }

        [Display(Name = "Added")]
        public DateTime DateTimeAdded { get; set; }

        [Display(Name = "Provider Trading Profile")]
        public string ProviderTradingProfileName { get; set; }

        [Display(Name = "Cost:DFR")]
        [DisplayFormat(DataFormatString = "{0:n4}", ApplyFormatInEditMode = true)]
        public decimal? CostFXRatio { get; set; }
    }
}

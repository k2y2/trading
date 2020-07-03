using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace trading.Models
{
    public partial class ReportProvider
    {
        public int ProviderTradingProfileID { get; set; }

        [Display(Name = "Provider Trading Profile")]
        public string ProviderTradingProfileName { get; set; }

        [Display(Name = "Total Expected Payin Amount")]
        [DisplayFormat(DataFormatString = "{0:n2}")]
        public decimal? TotalProviderExpectedPayInAmount { get; set; }

        [Display(Name = "Total Slip Amount")]
        [DisplayFormat(DataFormatString = "{0:n2}")]
        public decimal? TotalSlipAmount { get; set; }

        [Display(Name = "Total Actual Amount")]
        [DisplayFormat(DataFormatString = "{0:n2}")]
        public decimal? TotalActualAmount { get; set; }

        [Display(Name = "Missing Slip Amount")]
        [DisplayFormat(DataFormatString = "{0:n2}")]
        public decimal? MissingSlipAmount { get; set; }
    }
}

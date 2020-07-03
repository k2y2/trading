using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace trading.Models
{
    public partial class ReportAccountReceivable
    {
        public int ProviderTradingProfileID { get; set; }

        [Display(Name = "Provider Trading Profile")]
        public string ProviderTradingProfileName { get; set; }

        [Display(Name = "Balance")]
        [DisplayFormat(DataFormatString = "{0:n2}")]
        public decimal? AmountReceivable { get; set; }

        [Display(Name = "Currency")]
        public string CurrencyName2 { get; set; }

        [Display(Name = "Balance (USD)")]
        [DisplayFormat(DataFormatString = "{0:n2}")]
        public decimal? AmountReceivableUSD { get; set; }

        public decimal? Rate { get; set; }
    }
}

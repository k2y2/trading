using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace trading.Models
{
    public partial class ReportAccountBalance
    {
        public int id { get; set; }

        [Display(Name = "Account")]
        public string AccountName { get; set; }

        [Display(Name = "Currency")]
        public string CurrencyName { get; set; }

        [Display(Name = "Balance")]
        [DisplayFormat(DataFormatString = "{0:n2}")]
        public decimal? Balance { get; set; }

        [Display(Name = "Balance (USD)")]
        [DisplayFormat(DataFormatString = "{0:n2}")]
        public decimal? BalanceUSD { get; set; }

        public decimal? Rate { get; set; }
    }
}

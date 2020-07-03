using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace trading.Models
{
    public partial class ReportAccountPayable
    {
        public int ClientTradingProfileID { get; set; }

        [Display(Name = "Client Trading Profile")]
        public string ClientTradingProfileName { get; set; }

        [Display(Name = "Currency")]
        public string ClientCurrencyNameOut { get; set; }

        [Display(Name = "Trade Date")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime TradeDate { get; set; }

        [Display(Name = "Balance")]
        [DisplayFormat(DataFormatString = "{0:n2}")]
        public decimal? ClientAmountUncompleted { get; set; }

        [Display(Name = "Balance (USD)")]
        [DisplayFormat(DataFormatString = "{0:n2}")]
        public decimal? ClientAmountUncompletedUSD { get; set; }

        public decimal? Rate { get; set; }
    }
}

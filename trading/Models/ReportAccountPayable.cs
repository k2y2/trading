using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace trading.Models
{
    public partial class ReportAccountPayable
    {
        //public int ClientTradingProfileID { get; set; }

        //[Display(Name = "Client Trading Profile")]
        //public string ClientTradingProfileName { get; set; }

        //[Display(Name = "Currency")]
        //public string ClientCurrencyNameOut { get; set; }

        //[Display(Name = "Trade Date")]
        //[DisplayFormat(DataFormatString = "{0:d}")]
        //public DateTime TradeDate { get; set; }

        //[Display(Name = "Balance")]
        //[DisplayFormat(DataFormatString = "{0:n2}")]
        //public decimal? ClientAmountUncompleted { get; set; }

        //[Display(Name = "Balance (USD)")]
        //[DisplayFormat(DataFormatString = "{0:n2}")]
        //public decimal? ClientAmountUncompletedUSD { get; set; }

        //public decimal? Rate { get; set; }

        public int ClientTradingProfileID { get; set; }

        [Display(Name = "Client Trading Profile")]
        public string ClientTradingProfileName { get; set; }

        [Display(Name = "Currency In")]
        public string ClientCurrencyNameIn { get; set; }

        [Display(Name = "Currency Out")]
        public string ClientCurrencyNameOut { get; set; }

        [Display(Name = "Trade Date (Since)")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime TradeDateSince { get; set; }

        [Display(Name = "Trade Date (Until)")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime TradeDateUp { get; set; }

        [Display(Name = "Balance Out")]
        [DisplayFormat(DataFormatString = "{0:n2}")]
        public decimal? ClientAmountUncompleted { get; set; }

        [Display(Name = "Balance In")]
        [DisplayFormat(DataFormatString = "{0:n2}")]
        public decimal? BalanceIn { get; set; }

        [Display(Name = "Balance In (USD)")]
        [DisplayFormat(DataFormatString = "{0:n2}")]
        public decimal? BalanceInUSD { get; set; }

        [Display(Name = "AD")] 
        public int? ActiveDays { get; set; }

        [Display(Name = "WAD")]
        public int? ActiveDaysWeekend { get; set; }

        [Display(Name = "WD")]
        public int? DaysWeekend { get; set; }

        [Display(Name = "Balance Out (USD)")]
        [DisplayFormat(DataFormatString = "{0:n2}")]
        public decimal? ClientAmountUncompletedUSD { get; set; }

        [DisplayFormat(DataFormatString = "{0:n7}")]
        public decimal? Rate { get; set; }
    }
}

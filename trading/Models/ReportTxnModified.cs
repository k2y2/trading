using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace trading.Models
{
    public partial class ReportTxnModified
    {
        public int TxnID { get; set; }

        [Display(Name = "Provider Payin USD")]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal? ProviderPayinUSD { get; set; }

        [Display(Name = "Client Payout USD")]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal? ClientPayoutUSD { get; set; }

        [Display(Name = "Introducer Comm. USD Type 1")]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal? IntroducerCommissionUSD1 { get; set; }

        [Display(Name = "Introducer Comm. USD Type 2")]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal? IntroducerCommissionUSD2 { get; set; }

        public decimal? ClientPayoutOrig { get; set; }
        public decimal? ClientPayoutMissing { get; set; }

        [Display(Name = "Reference No")]
        public string ReferenceNo { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        [Display(Name = "Trade Date")]
        public DateTime TradeDate { get; set; }

        public int ClientTradingProfileID { get; set; }

        [Display(Name = "Client Trading Profile")]
        public string ClientTradingProfileName { get; set; }

        public int ProviderTradingProfileID { get; set; }

        [Display(Name = "Provider Trading Profile")]
        public string ProviderTradingProfileName { get; set; }

        [Display(Name = "Client Payout Currency")]
        public string ClientCurrencyNameOut { get; set; }

        [Display(Name = "Client Payout Amount")]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal? ClientAmountOut { get; set; }

        [Display(Name = "Gross Profit USD")]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal? GrossProfitUSD { get; set; }

        [Display(Name = "Gross Profit USD (%)")]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal? GrossProfitUSDPct { get; set; }
    }
}

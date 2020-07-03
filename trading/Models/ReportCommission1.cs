using System;
using System.Collections.Generic;

namespace trading.Models
{
    public partial class ReportCommission1
    {
        public int TxnID { get; set; }
        public string ReferenceNo { get; set; }
        public DateTime TradeDate { get; set; }
        public int ClientTradingProfileID { get; set; }
        public string ClientTradingProfileName { get; set; }
        public string ClientCurrencyNameIn { get; set; }
        public decimal ClientAmountIn { get; set; }
        public string ClientCurrencyNameOut { get; set; }
        public decimal? ClientAmountOut { get; set; }
        public int IntroducerID { get; set; }
        public decimal? IntroducerCommissionRate { get; set; }
        public decimal? IntroducerCommissionUSD { get; set; }
    }
}

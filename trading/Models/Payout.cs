using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace trading.Models
{
    public partial class Payout
    {
        public int id { get; set; }

        [Display(Name = "Reference No.")]
        public string ReferenceNo { get; set; }

        [Display(Name = "TX Reference No.")]
        public int TxnID { get; set; }

        [Display(Name = "Client Payout Currency")]
        public byte? ClientPayoutCurrencyID { get; set; }

        [Display(Name = "Client Payout Amount")]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal ClientPayoutAmount { get; set; }

        [Display(Name = "Client Payout USD Rate")]
        [DisplayFormat(DataFormatString = "{0:n4}", ApplyFormatInEditMode = true)]
        public decimal? ClientPayoutUSDRate { get; set; }

        [Display(Name = "Provider Payin Currency")]
        public byte? ProviderPayinCurrencyID { get; set; }

        [Display(Name = "Provider Payin Amount")]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal? ProviderPayinAmount { get; set; }

        [Display(Name = "USD Provider Payin Rate")]
        [DisplayFormat(DataFormatString = "{0:n4}", ApplyFormatInEditMode = true)]
        public decimal ProviderPayinUSDRate { get; set; }

        [Display(Name = "Used Currency")]
        public byte UsedCurrencyID { get; set; }

        [Display(Name = "Used Amount")]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal UsedAmount { get; set; }

        [Display(Name = "Client Payout Used Rate")]
        [DisplayFormat(DataFormatString = "{0:n4}", ApplyFormatInEditMode = true)]
        public decimal UsedClientPayoutFXRate { get; set; }

        [Display(Name = "USD Used Rate")]
        [DisplayFormat(DataFormatString = "{0:n4}", ApplyFormatInEditMode = true)]
        public decimal UsedUSDRate { get; set; }
        
        [Display(Name = "APA Bank Account")]
        [Required(ErrorMessage = "Field required")]
        public int AccountBankAccountID { get; set; }

        [Display(Name = "Modified")]
        public DateTime DateTimeModified { get; set; }

        [Display(Name = "Added")]
        public DateTime DateTimeAdded { get; set; }
    }

    public partial class PayoutView
    {
        public int id { get; set; }

        [Display(Name = "Reference No.")]
        public string ReferenceNo { get; set; }

        [Display(Name = "TX Reference No.")]
        public int TxnID { get; set; }

        [Display(Name = "Client Payout Currency")]
        public byte? ClientPayoutCurrencyID { get; set; }

        [Display(Name = "Client Payout Amount")]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal ClientPayoutAmount { get; set; }

        [Display(Name = "Client Payout USD Rate")]
        [DisplayFormat(DataFormatString = "{0:n4}", ApplyFormatInEditMode = true)]
        public decimal? ClientPayoutUSDRate { get; set; }

        [Display(Name = "Provider Payin Currency")]
        public byte? ProviderPayinCurrencyID { get; set; }

        [Display(Name = "Provider Payin Amount")]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal? ProviderPayinAmount { get; set; }

        [Display(Name = "Provider Payin USD Rate")]
        [DisplayFormat(DataFormatString = "{0:n4}", ApplyFormatInEditMode = true)]
        public decimal ProviderPayinUSDRate { get; set; }

        [Display(Name = "Used Currency")]
        public byte UsedCurrencyID { get; set; }

        [Display(Name = "Used Amount")]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal UsedAmount { get; set; }

        [Display(Name = "Used Client Payout FX Rate")]
        [DisplayFormat(DataFormatString = "{0:n4}", ApplyFormatInEditMode = true)]
        public decimal UsedClientPayoutFXRate { get; set; }

        [Display(Name = "Used USD Rate")]
        [DisplayFormat(DataFormatString = "{0:n4}", ApplyFormatInEditMode = true)]
        public decimal UsedUSDRate { get; set; }

        [Display(Name = "APA Bank Account")]
        public int AccountBankAccountID { get; set; }

        [Display(Name = "Modified")]
        public DateTime DateTimeModified { get; set; }

        [Display(Name = "Added")]
        public DateTime DateTimeAdded { get; set; }
         
        [Display(Name = "TX Reference No.")]
        public string? TxnReferenceNo { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        [Display(Name = "Trade Date")]
        public DateTime? TradeDate { get; set; }

        [Display(Name = "Client Payout Currency")]
        public string? ClientPayoutCurrencyName { get; set; }

        [Display(Name = "Provider Payin Currency")]
        public string? ProviderPayinCurrencyName { get; set; }

        [Display(Name = "Used Currency")]
        public string? UsedCurrencyName { get; set; }

        [Display(Name = "Client Trading Profile")]
        public int? ClientTradingProfileID { get; set; }

        [Display(Name = "Client Trading Profile")]
        public string? ClientTradingProfileName { get; set; }

        [Display(Name = "Provider Trading Profile")]
        public int? LinkedProviderTradingProfileID { get; set; }

        [Display(Name = "Provider Trading Profile")]
        public string? LinkedProviderTradingProfileName { get; set; }

        [Display(Name = "APA Bank Account")]
        public string? AccountName { get; set; }

    }
}

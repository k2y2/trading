using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace trading.Models
{
    public partial class Txn
    {
        public int id { get; set; }

        [Display(Name = "Reference No.")]
        public string ReferenceNo { get; set; }

        public string Type { get; set; }

        [Display(Name = "Linked Deposit")]
        public int? LinkedDepositID { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        [Display(Name = "Trade Date")]
        public DateTime TradeDate { get; set; }

        [Display(Name = "Client Trading Profile")]
        public int ClientTradingProfileID { get; set; }

        public string Status { get; set; }

        [Display(Name = "Client Price Rate")]
        [DisplayFormat(DataFormatString = "{0:n4}", ApplyFormatInEditMode = true)]
        public decimal? ClientPriceRate { get; set; }

        [Display(Name = "Client Currency Pair")]
        public short? ClientCurrencyPairID { get; set; }

        [Display(Name = "Client DFR")]
        [DisplayFormat(DataFormatString = "{0:n4}", ApplyFormatInEditMode = true)]
        public decimal? ClientDfrRate { get; set; }

        [Display(Name = "Client Unique DFR")]
        public bool ClientUniqueDfr { get; set; }

        [Display(Name = "Client Exchange Rate")]
        [DisplayFormat(DataFormatString = "{0:n4}", ApplyFormatInEditMode = true)]
        public decimal? ClientExRate { get; set; }

        [Display(Name = "Client Currency In")]
        public byte ClientCurrencyIDIn { get; set; }

        [Display(Name = "Client Amount In")]
        [DisplayFormat(DataFormatString = "{0:n2}")]
        public decimal ClientAmountIn { get; set; }

        [Display(Name = "Client Currency Out")]
        public byte? ClientCurrencyIDOut { get; set; }

        [Display(Name = "Client Amount Out")]
        [DisplayFormat(DataFormatString = "{0:n2}")]
        public decimal? ClientAmountOut { get; set; }

        [Display(Name = "Client Pay Out Account")]
        public int? ClientPayOutAccountID { get; set; }

        [Display(Name = "Provider Trading Profile")]
        public int? ProviderTradingProfileID { get; set; }

        [Display(Name = "Provider Currency")]
        public byte? ProviderCurrencyID { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        [Display(Name = "Provider Cost Date")]
        public DateTime? ProviderCostDate { get; set; }

        [Display(Name = "Provider Cost Rate")]
        [DisplayFormat(DataFormatString = "{0:n4}", ApplyFormatInEditMode = true)]
        public decimal? ProviderCostRate { get; set; }

        [Display(Name = "Provider Expected Pay In Amount")]
        [DisplayFormat(DataFormatString = "{0:n2}")]
        public decimal? ProviderExpectedPayInAmount { get; set; }

        [Display(Name = "Provider Bank Account")]
        public int? ProviderBankAccountID { get; set; }

        [Display(Name = "Modified")]
        public DateTime DateTimeModified { get; set; }

        [Display(Name = "Added")]
        public DateTime DateTimeAdded { get; set; }
    }

    public partial class TxnView
    {
        public int id { get; set; }

        [Display(Name = "Reference No.")]
        public string ReferenceNo { get; set; }

        public string Type { get; set; }

        [Display(Name = "Linked Deposit")]
        public int? LinkedDepositID { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        [Display(Name = "Trade Date")]
        public DateTime TradeDate { get; set; }

        [Display(Name = "Client Trading Profile")]
        public int ClientTradingProfileID { get; set; }

        public string Status { get; set; }

        [Display(Name = "Client Price Rate")]
        [DisplayFormat(DataFormatString = "{0:n4}", ApplyFormatInEditMode = true)]
        public decimal? ClientPriceRate { get; set; }

        [Display(Name = "Client Currency Pair")]
        public short? ClientCurrencyPairID { get; set; }

        [Display(Name = "Client DFR")]
        [DisplayFormat(DataFormatString = "{0:n4}", ApplyFormatInEditMode = true)]
        public decimal? ClientDfrRate { get; set; }

        [Display(Name = "Client Unique DFR")]
        public bool ClientUniqueDfr { get; set; }

        [Display(Name = "Client Exchange Rate")]
        [DisplayFormat(DataFormatString = "{0:n4}", ApplyFormatInEditMode = true)]
        public decimal? ClientExRate { get; set; }

        [Display(Name = "Client Currency In")]
        public byte ClientCurrencyIDIn { get; set; }

        [Display(Name = "Client Amount In")]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal ClientAmountIn { get; set; }

        [Display(Name = "Client Currency Out")]
        public byte? ClientCurrencyIDOut { get; set; }

        [Display(Name = "Client Amount Out")]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal? ClientAmountOut { get; set; }

        [Display(Name = "Client Pay Out Account")]
        public int? ClientPayOutAccountID { get; set; }

        [Display(Name = "Provider Trading Profile")]
        public int? ProviderTradingProfileID { get; set; }

        [Display(Name = "Provider Currency")]
        public byte? ProviderCurrencyID { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        [Display(Name = "Provider Cost Date")]
        public DateTime? ProviderCostDate { get; set; }

        [Display(Name = "Provider Cost Rate")]
        [DisplayFormat(DataFormatString = "{0:n4}", ApplyFormatInEditMode = true)]
        public decimal? ProviderCostRate { get; set; }

        [Display(Name = "Provider Expected Pay In Amount")]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal? ProviderExpectedPayInAmount { get; set; }

        [Display(Name = "Provider Bank Account")]
        public int? ProviderBankAccountID { get; set; }

        [Display(Name = "Modified")]
        public DateTime DateTimeModified { get; set; }

        [Display(Name = "Added")]
        public DateTime DateTimeAdded { get; set; }



        [Display(Name = "Linked Deposit")]
        public string LinkedDepositReferenceNo { get; set; }
          
        [Display(Name = "Client Trading Profile")]
        public string ClientTradingProfileName { get; set; }
            
        [Display(Name = "Client Currency Pair")]
        public string ClientCurrencyPairName { get; set; }
          
        [Display(Name = "Client Currency In")]
        public string ClientCurrencyNameIn { get; set; }
         
        [Display(Name = "Client Currency Out")]
        public string ClientCurrencyNameOut { get; set; }
          
        [Display(Name = "Client Pay Out Account")]
        public string ClientPayOutAccountName { get; set; }

        [Display(Name = "Provider Trading Profile")]
        public string ProviderTradingProfileName { get; set; }

        [Display(Name = "Provider Currency")]
        public string ProviderCurrencyName { get; set; }
            
        [Display(Name = "Provider Bank Account")]
        public string ProviderBankAccountName { get; set; }

        [Display(Name = "Alert")]
        public string Alert { get; set; }
    }

    public partial class TxnCompleteView
    {
        public int id { get; set; }
        public string ReferenceNo { get; set; }
        public string Type { get; set; }
        public int? LinkedDepositID { get; set; }
        public DateTime TradeDate { get; set; }
        public int ClientTradingProfileID { get; set; }
        public string Status { get; set; }
        public decimal? ClientPriceRate { get; set; }
        public short? ClientCurrencyPairID { get; set; }
        public decimal? ClientDfrRate { get; set; } 
        public bool ClientUniqueDfr { get; set; }
        public decimal? ClientExRate { get; set; }
        public byte ClientCurrencyIDIn { get; set; }
        public decimal ClientAmountIn { get; set; }
        public byte? ClientCurrencyIDOut { get; set; }
        public decimal? ClientAmountOut { get; set; }
        public int? ClientPayOutAccountID { get; set; }
        public int? ProviderTradingProfileID { get; set; }
        public byte? ProviderCurrencyID { get; set; }
        public DateTime? ProviderCostDate { get; set; }
        public decimal? ProviderCostRate { get; set; }
        public decimal? ProviderExpectedPayInAmount { get; set; }
        public int? ProviderBankAccountID { get; set; }
        public DateTime DateTimeModified { get; set; }
        public DateTime DateTimeAdded { get; set; }

        public string LinkedDepositReferenceNo { get; set; }
        public string ClientTradingProfileName { get; set; }
        public string ClientCurrencyPairName { get; set; }
        public string ClientCurrencyNameIn { get; set; }
        public string ClientCurrencyNameOut { get; set; }
        public string ClientPayOutAccountName { get; set; }
        public string ProviderTradingProfileName { get; set; }
        public string ProviderCurrencyName { get; set; }
        public string ProviderBankAccountName { get; set; }

        public int? LinkedProviderTradingProfileID { get; set; }
        public string LinkedProviderTradingProfileName { get; set; }
        public byte? LinkedProviderCurrencyID { get; set; }
        public string LinkedProviderCurrencyName { get; set; }
        public DateTime? LinkedProviderCostDate { get; set; }
        public decimal? LinkedProviderCostRate { get; set; }
        public decimal? LinkedProviderExpectedPayInAmount { get; set; }
        public int? LinkedProviderBankAccountID { get; set; }
        public string LinkedProviderBankAccountName { get; set; }
    }
}

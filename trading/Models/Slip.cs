using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace trading.Models
{
    public partial class Slip
    {
        public int id { get; set; }
         
        [Display(Name = "Reference No.")]
        public string ReferenceNo { get; set; }
         
        [DisplayFormat(DataFormatString = "{0:d}")]
        [Display(Name = "Slip Date")]
        public DateTime SlipDate { get; set; }

        [Display(Name = "Provider Trading Profile")]
        public int ProviderTradingProfileID { get; set; }

        [Display(Name = "TX Reference No.")]
        public int? TxnID { get; set; }

        [Display(Name = "APA Bank Account")]
        public int AccountBankAccountID { get; set; }

        [Display(Name = "Sender")]
        public int? SenderID { get; set; }

        [Display(Name = "Slip Amount")]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal SlipAmount { get; set; }

        [Display(Name = "Actual Amount")]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal? ActualAmount { get; set; }

        [Display(Name = "Modified")]
        public DateTime DateTimeModified { get; set; }

        [Display(Name = "Added")]
        public DateTime DateTimeAdded { get; set; }
    }

    public partial class SlipView
    {
        public int id { get; set; }

        [Display(Name = "Reference No.")]
        public string ReferenceNo { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        [Display(Name = "Slip Date")]
        public DateTime SlipDate { get; set; }

        [Display(Name = "Provider Trading Profile")]
        public int ProviderTradingProfileID { get; set; }

        [Display(Name = "TX Reference No.")]
        public int? TxnID { get; set; }

        [Display(Name = "APA Bank Account")]
        public int AccountBankAccountID { get; set; }

        [Display(Name = "Sender")]
        public int? SenderID { get; set; }

        [Display(Name = "Slip Amount")]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal SlipAmount { get; set; }

        [Display(Name = "Actual Amount")]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal? ActualAmount { get; set; }

        [Display(Name = "Modified")]
        public DateTime DateTimeModified { get; set; }

        [Display(Name = "Added")]
        public DateTime DateTimeAdded { get; set; }

        [Display(Name = "Provider Trading Profile")]
        public string ProviderTradingProfileName { get; set; }

        [Display(Name = "TX Reference No.")]
        public string TxnReferenceNo { get; set; }

        [Display(Name = "APA Bank Account")]
        public string AccountBankAccountName { get; set; }

        [Display(Name = "Sender")]
        public string SenderName { get; set; }
    } 
}

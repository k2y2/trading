using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace trading.Models
{
    public partial class AccountTxn
    {
        public int id { get; set; }
        public string Type { get; set; }
        public int ReferenceID { get; set; }
        public int AccountBankAccountID { get; set; }
        public decimal? AmountCredit { get; set; }
        public decimal? AmountDebit { get; set; }
        public DateTime DateTimeModified { get; set; }
        public DateTime DateTimeAdded { get; set; }
    }

    public partial class AccountTxnView
    {
        public int id { get; set; }

        [Display(Name = "Type")]
        public string Type { get; set; }

        [Display(Name = "Reference No.")]
        public int ReferenceID { get; set; }

        [Display(Name = "Bank Account")]
        public int AccountBankAccountID { get; set; }

        [Display(Name = "Credit")]
        public decimal? AmountCredit { get; set; }

        [Display(Name = "Debit")]
        public decimal? AmountDebit { get; set; }

        [Display(Name = "Modified")]
        public DateTime DateTimeModified { get; set; }

        [Display(Name = "Added")]
        public DateTime DateTimeAdded { get; set; }

        [Display(Name = "Reference No.")]
        public string ReferenceNo { get; set; }

        [Display(Name = "Bank Account")]
        public string AccountName { get; set; }
    }
}

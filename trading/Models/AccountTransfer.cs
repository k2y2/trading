using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace trading.Models
{
    public partial class AccountTransfer
    {
        public int id { get; set; }

        [Display(Name = "Reference No.")]
        public string ReferenceNo { get; set; }

        [Required(ErrorMessage = "Field required")]
        [Display(Name = "APA Bank Account (From)")]
        public int AccountBankAccountIDFrom { get; set; }

        [Required(ErrorMessage = "Field required")]
        [Display(Name = "APA Bank Account (To)")]
        public int AccountBankAccountIDTo { get; set; }

        [Required(ErrorMessage = "Field required")]
        [Display(Name = "Amount")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Field required")]
        [Display(Name = "Rate")]
        public decimal Rate { get; set; }

        [Display(Name = "Reference")]
        public string Reference { get; set; }

        [Display(Name = "Modified")]
        public DateTime DateTimeModified { get; set; }

        [Display(Name = "Added")]
        public DateTime DateTimeAdded { get; set; }
    }

    public partial class AccountTransferView
    {
        public int id { get; set; }

        [Display(Name = "Reference No.")]
        public string ReferenceNo { get; set; }

        [Display(Name = "APA Bank Account (From)")]
        public int AccountBankAccountIDFrom { get; set; }

        [Display(Name = "APA Bank Account (To)")]
        public int AccountBankAccountIDTo { get; set; }

        [Display(Name = "Amount")]
        public decimal Amount { get; set; }

        [Display(Name = "Rate")]
        public decimal Rate { get; set; }

        [Display(Name = "Reference")]
        public string Reference { get; set; }

        [Display(Name = "Modified")]
        public DateTime DateTimeModified { get; set; }

        [Display(Name = "Added")]
        public DateTime DateTimeAdded { get; set; }

        [Display(Name = "APA Bank Account (From)")]
        public string AccountBankAccountNameFrom { get; set; }

        [Display(Name = "APA Bank Account (To)")]
        public string AccountBankAccountNameTo { get; set; }
    }
}

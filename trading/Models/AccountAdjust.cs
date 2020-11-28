using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace trading.Models
{
    public partial class AccountAdjust
    {
        public int id { get; set; }

        [Display(Name = "Reference No.")]
        public string ReferenceNo { get; set; }

        [Required(ErrorMessage = "Field required")]
        [Display(Name = "APA Bank Account")]
        public int AccountBankAccountID { get; set; }

        [Required(ErrorMessage = "Field required")]
        [Display(Name = "Amount")]
        public decimal Amount { get; set; }
         
        [DisplayFormat(DataFormatString = "{0:d}")]
        [Display(Name = "Actual Date")]
        public DateTime? ActualDate { get; set; }

        [Display(Name = "Reference")]
        public string Reference { get; set; }

        [Display(Name = "Modified")]
        public DateTime DateTimeModified { get; set; }

        [Display(Name = "Added")]
        public DateTime DateTimeAdded { get; set; }
    }

    public partial class AccountAdjustView
    {
        public int id { get; set; }

        [Display(Name = "Reference No.")]
        public string ReferenceNo { get; set; }

        [Display(Name = "APA Bank Account")]
        public int AccountBankAccountID { get; set; }

        [Display(Name = "Amount")]
        public decimal Amount { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        [Display(Name = "Actual Date")]
        public DateTime? ActualDate { get; set; }

        [Display(Name = "Reference")]
        public string Reference { get; set; }

        [Display(Name = "Modified")]
        public DateTime DateTimeModified { get; set; }

        [Display(Name = "Added")]
        public DateTime DateTimeAdded { get; set; }

        [Display(Name = "APA Bank Account")]
        public string AccountName { get; set; }
    }
}

using ExpressiveAnnotations.Attributes;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace trading.Models
{
    public partial class ProviderBankAccount
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Field required")]
        [Display(Name = "Provider")]
        public int ProviderID { get; set; }

        [Required(ErrorMessage = "Field required")]
        [Remote("IsExist", "ProviderBankAccount", AdditionalFields = "id", ErrorMessage = "Account exist already")]
        [Display(Name = "Account Ref. Name")]
        public string AccountName { get; set; }

        [Required(ErrorMessage = "Field required")]
        [Display(Name = "Currency")]
        public byte CurrencyID { get; set; }

        [Display(Name = "Account Type")]
        public byte? AccountTypeID { get; set; }

        [Required(ErrorMessage = "Field required")]
        [Display(Name = "Account Holder Name")]
        public string AccountHolderName { get; set; }

        [Required(ErrorMessage = "Field required")]
        [Display(Name = "Account Holder Address")]
        public string AccountHolderAddress { get; set; }

        [Display(Name = "Account Number")]
        [RequiredIf("IBAN == null", ErrorMessage = "Account Number or IBAN is required")]
        public string AccountNo { get; set; }

        [Display(Name = "IBAN")]
        [RequiredIf("AccountNo == null", ErrorMessage = "Account Number or IBAN is required")]
        public string IBAN { get; set; }

        [Display(Name = "BSB/SortCode/Rounting")]
        public string BSB { get; set; }

        [Display(Name = "Bank Code")]
        public string BankCode { get; set; }

        [Required(ErrorMessage = "Field required")]
        [Display(Name = "Bank Name")]
        public string BankName { get; set; }

        [Required(ErrorMessage = "Field required")]
        [Display(Name = "SWIFT")]
        public string SWIFT { get; set; }

        [Required(ErrorMessage = "Field required")]
        [Display(Name = "Country")]
        public byte CountryID { get; set; }

        [Display(Name = "Branch Address")]
        public string BranchAddress { get; set; }

        [Display(Name = "Reference")]
        public string Reference { get; set; }

        [Display(Name = "Modified")]
        public DateTime DateTimeModified { get; set; }

        [Display(Name = "Added")]
        public DateTime DateTimeAdded { get; set; }
    }

    public partial class ProviderBankAccountView
    {
        public int id { get; set; }
         
        [Display(Name = "Provider")]
        public int ProviderID { get; set; }
         
        [Display(Name = "Account Ref. Name")]
        public string AccountName { get; set; }
         
        [Display(Name = "Currency")]
        public byte CurrencyID { get; set; }

        [Display(Name = "Account Type")]
        public byte? AccountTypeID { get; set; }
         
        [Display(Name = "Account Holder Name")]
        public string AccountHolderName { get; set; }
         
        [Display(Name = "Account Holder Address")]
        public string AccountHolderAddress { get; set; }

        [Display(Name = "Account Number")] 
        public string AccountNo { get; set; }

        [Display(Name = "IBAN")] 
        public string IBAN { get; set; }

        [Display(Name = "BSB/SortCode/Rounting")]
        public string BSB { get; set; }

        [Display(Name = "Bank Code")]
        public string BankCode { get; set; }
         
        [Display(Name = "Bank Name")]
        public string BankName { get; set; }
         
        [Display(Name = "SWIFT")]
        public string SWIFT { get; set; }
         
        [Display(Name = "Country")]
        public byte CountryID { get; set; }

        [Display(Name = "Branch Address")]
        public string BranchAddress { get; set; }

        [Display(Name = "Reference")]
        public string Reference { get; set; }

        [Display(Name = "Modified")]
        public DateTime DateTimeModified { get; set; }

        [Display(Name = "Added")]
        public DateTime DateTimeAdded { get; set; }

        [Display(Name = "Provider")]
        public string ProviderName { get; set; }

        [Display(Name = "Currency")]
        public string CurrencyName { get; set; }

        [Display(Name = "Account Type")]
        public string AccountTypeName { get; set; }

        [Display(Name = "Country")]
        public string CountryName { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace trading.Models
{
    public partial class Sender
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Field required")]
        [Remote("IsExist", "Sender", AdditionalFields = "id,ProviderID", ErrorMessage = "Sender exist already")]
        [Display(Name = "Sender")]
        public string SenderName { get; set; }

        [Required(ErrorMessage = "Field required")] 
        [Display(Name = "Provider")]
        public int ProviderID { get; set; }

        [Display(Name = "Unit")]
        public string Unit { get; set; }

        [Display(Name = "Address Line 1")]
        public string AddressLine1 { get; set; }

        [Display(Name = "Address Line 2")]
        public string AddressLine2 { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "Country")]
        public string Country { get; set; }

        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }

        [Display(Name = "Director First Name")]
        public string DirectorFirstName { get; set; }

        [Display(Name = "Director Last Name")]
        public string DirectorLastName { get; set; }

        [Display(Name = "Modified")]
        public DateTime DateTimeModified { get; set; }

        [Display(Name = "Added")]
        public DateTime DateTimeAdded { get; set; }
    }

    public partial class SenderView
    {
        public int id { get; set; }

        [Display(Name = "Sender")]
        public string SenderName { get; set; }

        [Display(Name = "Provider")]
        public int ProviderID { get; set; }

        [Display(Name = "Unit")]
        public string Unit { get; set; }

        [Display(Name = "Address Line 1")]
        public string AddressLine1 { get; set; }

        [Display(Name = "Address Line 2")]
        public string AddressLine2 { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "Country")]
        public string Country { get; set; }

        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }

        [Display(Name = "Director First Name")]
        public string DirectorFirstName { get; set; }

        [Display(Name = "Director Last Name")]
        public string DirectorLastName { get; set; }

        [Display(Name = "Modified")]
        public DateTime DateTimeModified { get; set; }

        [Display(Name = "Added")]
        public DateTime DateTimeAdded { get; set; }

        [Display(Name = "Provider")]
        public string ProviderName { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace trading.Models
{
    public partial class Client
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Field required")]
        [Remote("IsExist", "Client", AdditionalFields = "id", ErrorMessage = "Client exist already")]
        [Display(Name = "Client")]
        public string ClientName { get; set; }

        [Display(Name = "Legal Full Name")]
        public string ClientLegalName { get; set; }

        [Required(ErrorMessage = "Field required")]
        [Display(Name = "Client Type")]
        public byte ClientTypeID { get; set; }

        [Display(Name = "Modified")]
        public DateTime DateTimeModified { get; set; }

        [Display(Name = "Added")]
        public DateTime DateTimeAdded { get; set; } 
    }

    public partial class ClientView
    {
        public int id { get; set; }
         
        [Display(Name = "Client")]
        public string ClientName { get; set; }

        [Display(Name = "Legal Full Name")]
        public string ClientLegalName { get; set; }
         
        [Display(Name = "Client Type")]
        public byte ClientTypeID { get; set; }

        [Display(Name = "Modified")]
        public DateTime DateTimeModified { get; set; }

        [Display(Name = "Added")]
        public DateTime DateTimeAdded { get; set; }

        [Display(Name = "Client Type")]
        public string ClientTypeName { get; set; }

        [Display(Name = "Trading Profile Info")]
        public string ClientTradingProfileInfo { get; set; }
        
    }
}

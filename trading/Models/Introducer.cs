using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace trading.Models
{
    public partial class Introducer
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Field required")] 
        [Remote("IsExist", "Introducer", AdditionalFields = "id", ErrorMessage = "Introducer exist already")]
        [Display(Name = "Introducer")]
        public string IntroducerName { get; set; }

        [Display(Name = "Legal Name")]
        public string IntroducerLegalName { get; set; }

        [Display(Name = "Modified")]
        public DateTime DateTimeModified { get; set; }

        [Display(Name = "Added")]
        public DateTime DateTimeAdded { get; set; }
    }

    public partial class IntroducerView
    {
        public int id { get; set; }

        [Display(Name = "Introducer")]
        public string IntroducerName { get; set; }

        [Display(Name = "Legal Name")]
        public string IntroducerLegalName { get; set; }

        [Display(Name = "Modified")]
        public DateTime DateTimeModified { get; set; }

        [Display(Name = "Added")]
        public DateTime DateTimeAdded { get; set; }

        [Display(Name = "Associated Client")]
        public string AssociatedClient { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace trading.Models
{
    public partial class ProviderTradingProfileIntroducerMap
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Field required")]
        [Display(Name = "Provider Trading Profile")]
        public int ProviderTradingProfileID { get; set; }
         
        [Required(ErrorMessage = "Field required")]
        [Remote("IsExist", "ProviderTradingProfileIntroducerMap", AdditionalFields = "id,ProviderTradingProfileID", ErrorMessage = "Introducer exist already")]
        [Display(Name = "Introducer")]
        public int IntroducerID { get; set; }
          
        [Display(Name = "Introducer Commission Type")]
        public byte? IntroducerCommissionTypeID { get; set; }

        [Display(Name = "Introducer Commission Rate")]
        [DisplayFormat(DataFormatString = "{0:n4}", ApplyFormatInEditMode = true)]
        public decimal? IntroducerCommissionRate { get; set; }

        [Display(Name = "Modified")]
        public DateTime DateTimeModified { get; set; }

        [Display(Name = "Added")]
        public DateTime DateTimeAdded { get; set; }
    }

    public partial class ProviderTradingProfileIntroducerMapView
    {
        public int id { get; set; }
         
        [Display(Name = "Provider Trading Profile")]
        public int ProviderTradingProfileID { get; set; }

        [Display(Name = "Introducer")]
        public int IntroducerID { get; set; }

        [Display(Name = "Introducer Commission Type")]
        public byte? IntroducerCommissionTypeID { get; set; }

        [Display(Name = "Introducer Commission Rate")]
        [DisplayFormat(DataFormatString = "{0:n4}", ApplyFormatInEditMode = true)]
        public decimal? IntroducerCommissionRate { get; set; }

        [Display(Name = "Modified")]
        public DateTime DateTimeModified { get; set; }

        [Display(Name = "Added")]
        public DateTime DateTimeAdded { get; set; }

        [Display(Name = "Provider Trading Profile")]
        public string ProviderTradingProfileName { get; set; }

        [Display(Name = "Introducer")] 
        public string IntroducerName { get; set; }

        [Display(Name = "Introducer Commission Type")]
        public string IntroducerCommissionTypeName { get; set; }
    }
}

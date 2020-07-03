using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace trading.Models
{
    public partial class ClientTradingProfileIntroducerMap
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Field required")]
        [Display(Name = "Client Trading Profile")]
        public int ClientTradingProfileID { get; set; }

        [Required(ErrorMessage = "Field required")]
        [Remote("IsExist", "ClientTradingProfileIntroducerMap", AdditionalFields = "id,ClientTradingProfileID", ErrorMessage = "Introducer exist already")]
        [Display(Name = "Introducer")]
        public int IntroducerID { get; set; }

        [Display(Name = "Introducer Commission Type")]
        public byte? IntroducerCommissionTypeID { get; set; }

        [Display(Name = "Introducer Commission Rate (%)")]
        [DisplayFormat(DataFormatString = "{0:n4}", ApplyFormatInEditMode = true)]
        public decimal? IntroducerCommissionRate { get; set; }

        [Display(Name = "Modified")]
        public DateTime DateTimeModified { get; set; }

        [Display(Name = "Added")]
        public DateTime DateTimeAdded { get; set; }
    }

    public partial class ClientTradingProfileIntroducerMapView
    {
        public int id { get; set; }

        [Display(Name = "Client Trading Profile")]
        public int ClientTradingProfileID { get; set; }

        [Display(Name = "Introducer")]
        public int IntroducerID { get; set; }

        [Display(Name = "Introducer Commission Type")]
        public byte? IntroducerCommissionTypeID { get; set; }

        [Display(Name = "Introducer Commission Rate (%)")]
        [DisplayFormat(DataFormatString = "{0:n4}", ApplyFormatInEditMode = true)]
        public decimal? IntroducerCommissionRate { get; set; }

        [Display(Name = "Modified")]
        public DateTime DateTimeModified { get; set; }

        [Display(Name = "Added")]
        public DateTime DateTimeAdded { get; set; }


        [Display(Name = "Client Trading Profile")]
        public string ClientTradingProfileName { get; set; }

        [Display(Name = "Introducer")]
        public string IntroducerName { get; set; }

        [Display(Name = "Introducer Commission Type")]
        public string IntroducerCommissionTypeName { get; set; }
    }
}

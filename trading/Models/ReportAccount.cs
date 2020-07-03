using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace trading.Models
{
    public partial class ReportAccount
    {
        public int id { get; set; }

        [Display(Name = "Account")]
        public string AccountName { get; set; }

        [Display(Name = "Total Client Out Amount")]
        public decimal? TotalClientAmountOut { get; set; }
    }
}

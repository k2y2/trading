using System;
using System.Collections.Generic;

namespace trading.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime DateTimeModified { get; set; }
        public DateTime DateTimeAdded { get; set; }
    }
}

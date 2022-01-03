using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineTechnicalHiring.Models
{
    public class TechnicianViewModel
    {
        public int TID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public Nullable<int> TType { get; set; }
        public Nullable<decimal> Latitude { get; set; }
        public Nullable<decimal> Longitude { get; set; }
        public int WorkCount { get; set; }
       // public Nullable<bool> IsApproved { get; set; }
        // public string Password { get; set; }
        public byte[] Photo { get; set; }
        public string Password { get; set; }
      
    }
}
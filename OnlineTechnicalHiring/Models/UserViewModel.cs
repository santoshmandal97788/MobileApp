using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineTechnicalHiring.Models
{
    public class UserViewModel
    {
        public int UId { get; set; }

        [Required(ErrorMessage = "Full Name is Required")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Gender is Required")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        // [Remote("EmailExists", "StudentDB", HttpMethod = "POST", ErrorMessage = "Email address already registered.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone Number is required")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Invalid Phone number")]
        public string PhoneNumber { get; set; }

        // [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        public string Password { get; set; }
        public Nullable<bool> IsApproved { get; set; }
        public Nullable<decimal> Latitude { get; set; }
        public Nullable<decimal> Longitude { get; set; }
        public byte[] Photo { get; set; }

        public string Country { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }
        public string District { get; set; }
        public string Locality { get; set; }
    }
}
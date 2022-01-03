using Newtonsoft.Json;
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
        [JsonProperty("FullName")]
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

        [JsonProperty("Photo")]
        public byte[] Photo { get; set; }
        public int TType { get; set; }

    }
}
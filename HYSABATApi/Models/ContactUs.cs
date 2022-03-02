using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HYSABATApi.Models
{
    public class ContactUs
    {
        [Key]
        public int Id { get; set; }
        [Required]


        public string FullName { get; set; }
        [Required(ErrorMessage = "Please enter email address")]
        [Display(Name = "Email Address")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "You must provide a phone number")]
        [Display(Name = "Home Phone")]
        [DataType(DataType.PhoneNumber)]


        public string MobileNumber { get; set; }

        [Display(Name = "Country")]
        [Required]
        public string Country { get; set; }


        [Display(Name = "Subject")]
        public string Subject { get; set; }


        [Required]
        [Display(Name = "Message")]
        public string Message { get; set; }
    }
}

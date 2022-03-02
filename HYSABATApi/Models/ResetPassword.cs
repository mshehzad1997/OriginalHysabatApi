using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HYSABATApi.Models
{
    public class ResetPassword
    {
        [Required(ErrorMessage = "User Name is required")]
        public string UserName { get; set; }
        
        [Required(ErrorMessage = "New Password ")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Confirm Password ")]
        public string ConfirmPassword { get; set; }
    }
}

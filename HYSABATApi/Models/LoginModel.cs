using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HYSABATApi.Models
{
    public class LoginModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        
        public string UserName { get; set; }
       
        [Required(ErrorMessage = "Password is required")]

        public string Password { get; set; }
    }
}

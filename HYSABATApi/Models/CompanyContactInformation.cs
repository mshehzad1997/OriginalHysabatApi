using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HYSABATApi.Models
{
    public class CompanyContactInformation
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name ="Comany Name")]
        public string CompanyName { get; set; }
        [Required]
        [Display(Name = "Comany Location")]
        public string CompanyLocation { get; set; }
        [Required]
        [Display(Name = "Comany Phone Number")]
        public string CompanyPhone { get; set; }
        [Required]
        [Display(Name = "Comany Email")]
        public string Email { get; set; }
    }
}

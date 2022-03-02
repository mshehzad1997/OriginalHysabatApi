using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HYSABATApi.Models.PricingPlanViewModel
{
    public class PricingPlanVM
    {
        [Key]
        public int? Id { get; set; }
        [Required]
        [Display(Name = "Give Name to Plan ")]

        public string Title { get; set; }
        [Required]
        [Display(Name = "Monthly Price")]
        public int MonthlyPrice { get; set; }
        [Required]
        [Display(Name = "Yearly Price")]
        public int YearlyPrice { get; set; }
        [Required]
        [Display(Name = "Give Image of Plan")]
        public IFormFile FileImage { get; set; }
        [InverseProperty("Features")]
        public string[] PricingPlanFeatures { get; set; }
        
    }
   
}

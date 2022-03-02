using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HYSABATApi.Models.FeatureViewModel
{
    public class FeatureVM
    {
        [Key]
        public int? Id { get; set; }
        [Required]
        [Display(Name = "Feature Name ")]
        public string Title { get; set; }
        [Required]
        [Display(Name = " Feature Description  ")]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Feature Image  ")]
        public IFormFile ImageFile { get; set; }
        [Required]
        [Display(Name = "Select DashBoard Feature")]
        public DashBoardFeatures boardFeatures { get; set; }
        public enum DashBoardFeatures
        {
            KPI,
            BI,
            Fleet
        }
    }
}

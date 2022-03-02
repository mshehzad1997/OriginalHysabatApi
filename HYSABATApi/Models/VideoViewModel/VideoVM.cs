using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HYSABATApi.Models.VideoViewModel
{
    public class VideoVM
    {
        [Key]
        public int? Id { get; set; }
        [Required]
        [Display(Name = "Video Name")]
        public string VideoName { get; set; }
        [Required]
        [Display(Name = "Video File")]
        public IFormFile VideoFile { get; set; }
    }
}

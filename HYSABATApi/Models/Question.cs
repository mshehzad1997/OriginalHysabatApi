using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HYSABATApi.Models
{
    public class Question
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Question ")]
        public string question { get; set; }
        [Required]
        [Display(Name = "Answer")]
        public string Answer { get; set; }
    }
}

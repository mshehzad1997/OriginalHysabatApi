using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HYSABATApi.Models
{
    public class PricingPlan
    {
        [Key]
        public int Id { get; set; }
       
        public string PricingPlanTitle { get; set; }

        public int PricingPlanMonthlyPrice { get; set; }

        public int PricingPlanYearlyPrice { get; set; }


        public string PricingPlanImagePath { get; set; }
       
        public string PricingPlanFeatures { get; set; }
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HYSABATApi.Models
{
    public class Feature
    {
        public int Id { get; set; }
        public string FeatureTitle { get; set; }
        public string FeatureDescription { get; set; }
        public string FeatureImagePath { get; set; }
        public string FeatureType { get; set; }
    }
}

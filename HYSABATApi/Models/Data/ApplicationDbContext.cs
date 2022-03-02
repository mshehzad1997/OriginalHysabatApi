using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace HYSABATApi.Models.Data
{
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }
        public DbSet<Feature> features { get; set; }
        public DbSet<PricingPlan> pricingPlans { get; set; }
        public DbSet<Question> questions { get; set; }
        public DbSet<Video> videos { get; set; }
        public DbSet<PurchasePlan> contactInformation { get; set; }
        public DbSet<ContactUs> contactUs { get; set; }
        public DbSet<CompanyContactInformation> companies { get; set; }
      

    }
}

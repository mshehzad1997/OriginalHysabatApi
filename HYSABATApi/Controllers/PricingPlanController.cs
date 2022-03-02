using HYSABATApi.Models;
using HYSABATApi.Models.Data;
using HYSABATApi.Models.PricingPlanViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HYSABATApi.Controllers
{
  
    [Route("api/[controller]")]
    [ApiController]
    public class PricingPlanController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHost;
        public PricingPlanController(ApplicationDbContext db, IWebHostEnvironment webHost)
        {
            this._db = db;
            this._webHost = webHost;
        }
        [HttpGet]
        [Route("GetPlan")]
        public async Task<IActionResult> GetPlan()
        {
            string imagePath = "/PricingPlan/";
            var plan = await _db.pricingPlans.Select(x => new PricingPlan()
            {
                PricingPlanTitle = x.PricingPlanTitle,

                PricingPlanMonthlyPrice = x.PricingPlanMonthlyPrice,
                PricingPlanYearlyPrice = x.PricingPlanYearlyPrice,
                PricingPlanFeatures = x.PricingPlanFeatures,
                PricingPlanImagePath = string.Concat(imagePath,x.PricingPlanImagePath),
                

            }).ToListAsync();

            var plans =  plan.Select(x => new
            {
                x.PricingPlanTitle,
                x.PricingPlanMonthlyPrice,
                x.PricingPlanYearlyPrice,
                x.PricingPlanImagePath,
                Features = string.IsNullOrEmpty(x.PricingPlanFeatures) ? new string[] { } : x.PricingPlanFeatures.Split(",", StringSplitOptions.RemoveEmptyEntries)
            });



            return Ok(plans);
            
        }
        [Authorize(Roles = UserRoles.Admin)]

        [HttpPost]
        [Route("CreatePlan")]
        public async Task<IActionResult> CreatePlan([FromForm]PricingPlanVM model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                string planFeature = null;
                string ImageExtension = Path.GetExtension(model.FileImage.FileName);
                if(ImageExtension.ToLower() == ".jpg" || ImageExtension.ToLower() == ".jpeg")
                {
                    long size = (model.FileImage).Length;
                    if (size <= 1000000)
                    {

                        string uploadFolder = Path.Combine(_webHost.WebRootPath, "PricingPlan");
                        uniqueFileName = Guid.NewGuid().ToString() + "_" + model.FileImage.FileName;
                        string filePath = Path.Combine(uploadFolder, uniqueFileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                           await model.FileImage.CopyToAsync(stream);
                        }
                    }
                    else
                    {
                        return BadRequest("File Too large");
                    }
                }
                else
                {
                    return BadRequest("Cannot Add Image");
                }
               
               
                foreach (var item in model.PricingPlanFeatures)
                {
                    planFeature += item+"," ;
                }
                var obj = new PricingPlan()
                {
                    PricingPlanTitle = model.Title,
                    PricingPlanMonthlyPrice = model.MonthlyPrice,
                    PricingPlanYearlyPrice = model.YearlyPrice,
                   
                    PricingPlanFeatures =  planFeature,
                    PricingPlanImagePath = uniqueFileName
                };
             
                _db.pricingPlans.Add(obj);
               await _db.SaveChangesAsync();
               
            }
            return Ok();
        }
        [Authorize(Roles = UserRoles.Admin)]
        [HttpDelete]
        [Route("DeletePlan")]
        public async Task<IActionResult> DeletePlan([FromForm]int id)
        {
            
            var plan =await _db.pricingPlans.Where(x => x.Id == id).FirstOrDefaultAsync();
            if(plan == null)
            {
                return NotFound();
            }
            var imagePath = Path.Combine(_webHost.WebRootPath, "pricingPlan", plan.PricingPlanImagePath);
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
            _db.pricingPlans.Remove(plan);
           await _db.SaveChangesAsync();
            return Ok();
        }
        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        [Route("UpdatePlan")]
        public async Task<IActionResult> UpdatePlan([FromForm]PricingPlanVM model)
        {
            if (ModelState.IsValid)
            {
                string planFeature = null;   
                var plan =await _db.pricingPlans.FindAsync(model.Id);
                if(plan == null)
                {
                    return NotFound("Not Found");
                }
               
               
                if (model.FileImage != null)
                {
                    if (plan.PricingPlanImagePath != null)
                    {
                        var image = Path.Combine(_webHost.WebRootPath, "PricingPlan", plan.PricingPlanImagePath);
                        System.IO.File.Delete(image);
                    }
                    string uniqueFileName = null;
                    string extension = Path.GetExtension(model.FileImage.FileName);
                    long size = (model.FileImage).Length;
                    if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg")
                    {
                        if (size <= 1000000)
                        {
                            string uploadFolder = Path.Combine(_webHost.WebRootPath, "PricingPlan");
                            uniqueFileName = Guid.NewGuid().ToString() + "_" + model.FileImage.FileName;
                            string filePath = Path.Combine(uploadFolder, uniqueFileName);

                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await model.FileImage.CopyToAsync(stream);
                            }
                            plan.PricingPlanImagePath = uniqueFileName;
                        }
                        else
                        {
                            return BadRequest("File Too large");
                        }
                    }
                    else
                    {
                        return BadRequest("Invalid File");
                    }
                    

                }
                plan.PricingPlanTitle = model.Title;
                plan.PricingPlanMonthlyPrice = model.MonthlyPrice;
                plan.PricingPlanYearlyPrice = model.YearlyPrice;
                foreach (var item in model.PricingPlanFeatures)
                {
                    planFeature += item + ",";
                }
                plan.PricingPlanFeatures = planFeature;
                _db.pricingPlans.Update(plan);
               await _db.SaveChangesAsync();
            }
            return Ok();
         
        }
      
    }
}

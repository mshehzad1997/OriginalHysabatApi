using HYSABATApi.Models;
using HYSABATApi.Models.Data;
using HYSABATApi.Models.FeatureViewModel;
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
    //[Authorize(Roles = UserRoles.Admin)]
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FeaturesController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHost;
        public FeaturesController(ApplicationDbContext db, IWebHostEnvironment webHost)
        {
            this._db = db;
            this._webHost = webHost;
        }
        [HttpGet]
        [Route("GetFeature")]
        public async Task<IActionResult> GetFeature()
        {
            string featureImagePath = "/Feature/";
            var feature = await _db.features.Select(x => new Feature()
            {
                FeatureTitle = x.FeatureTitle,
                FeatureDescription = x.FeatureDescription,
                FeatureImagePath = string.Concat(featureImagePath, x.FeatureImagePath),
                FeatureType = x.FeatureType
            }).ToListAsync();
            return Ok(feature);
        }
        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        [Route("CreateFeature")]
        public async Task<IActionResult> CreateFeature([FromForm]FeatureVM model)
        {
            if (ModelState.IsValid)
            {
                
                string uniqueFileName = null;
                string extension = Path.GetExtension(model.ImageFile.FileName);
                 if(extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg")
                {
                    long size = (model.ImageFile).Length;
                    if(size <= 1000000)
                    {
                        string uploadFolder = Path.Combine(_webHost.WebRootPath, "Feature");
                        uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImageFile.FileName;
                        string filePath = Path.Combine(uploadFolder, uniqueFileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await model.ImageFile.CopyToAsync(stream);

                        }
                    }
                    else
                    {
                        return NotFound("File too large");
                    }
                }
                else
                {
                    return NotFound("Invalid File");
                }
               

                var featureModel = new Feature()
                {
                    FeatureTitle = model.Title,
                    FeatureDescription = model.Description,
                    FeatureImagePath = uniqueFileName,
                  
                    FeatureType = (model.boardFeatures).ToString()
                };
                _db.features.Add(featureModel);
              await  _db.SaveChangesAsync();

            }
            return Ok();
        }
        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        [Route("DeleteFeature")]
        public async Task<IActionResult> DeleteFeature([FromForm]int? id)
         {
            var feature = _db.features.Where(x => x.Id == id).FirstOrDefault();
            if(feature == null)
            {
                return NotFound();
            }

            var imagePath = Path.Combine(_webHost.WebRootPath, "Feature", feature.FeatureImagePath);
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
            _db.features.Remove(feature);
           await _db.SaveChangesAsync();
            return Ok();
        }
        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        [Route("UpdateFeature")]
        public async Task<IActionResult> UpdateFeature([FromForm]FeatureVM model)
        {
            if (ModelState.IsValid)
            {
                var features = await _db.features.FindAsync(model.Id);
                if(features == null)
                {
                    return NotFound();
                }
                features.FeatureTitle = model.Title;
                features.FeatureDescription = model.Description;
                features.FeatureType = (model.boardFeatures).ToString();

                if (model.ImageFile != null)
                {
                    if (features.FeatureImagePath != null)
                    {
                        string filePath = Path.Combine(_webHost.WebRootPath, "Feature", features.FeatureImagePath);
                        System.IO.File.Delete(filePath);
                    }
                   
                    string uniqueFileName = null;

                    string extension = Path.GetExtension(model.ImageFile.FileName);
                    if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg")
                    {
                        long size = (model.ImageFile).Length;
                        if (size <= 1000000)
                        {
                            string uploadFolder = Path.Combine(_webHost.WebRootPath, "Feature");
                            uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImageFile.FileName;
                            string filePath = Path.Combine(uploadFolder, uniqueFileName);

                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await model.ImageFile.CopyToAsync(stream);
                            }
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

                    features.FeatureImagePath =  uniqueFileName;
                }

              _db.features.Update(features);

              await _db.SaveChangesAsync();
               
            }
            return Ok();
        }
       
    }
}

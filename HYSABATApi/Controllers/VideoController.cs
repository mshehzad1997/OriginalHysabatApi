using HYSABATApi.Models;
using HYSABATApi.Models.Data;
using HYSABATApi.Models.VideoViewModel;
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

    //[Authorize]

    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    { 
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHost;
        public VideoController(ApplicationDbContext db, IWebHostEnvironment webHost)
        {
            this._db = db;
            this._webHost = webHost;
        }
        [HttpGet]
        [Route("GetVideo")]
        public async Task<IActionResult> GetVideo()
        {
            string videoPath = "/Video/";

            var video = await _db.videos.Select(x => new Video()
            {
                VideoName = x.VideoName,
                VideoPath = string.Concat(videoPath, x.VideoPath)
            }).ToListAsync();
           
            
            return Ok(video);

        }
        [Authorize(Roles = UserRoles.Admin)]
        [Route("CreateVideo")]
        [HttpPost]
        public async Task<IActionResult> CreateVideo([FromForm]VideoVM model)
        {
            if (ModelState.IsValid)
            {
           
                string uniqueFileName = null;
                string extension = Path.GetExtension(model.VideoFile.FileName);
                if(extension.ToLower() == ".mp4")
                {
                    string uploadFolder = Path.Combine(_webHost.WebRootPath, "Video");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.VideoFile.FileName;
                    string filePath = Path.Combine(uploadFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.VideoFile.CopyToAsync(stream);
                    }
                }
                else
                {
                    return BadRequest("Not Allowed");
                }
                var videoModel = new Video()
                {
                    VideoName = model.VideoName,

                    VideoPath = uniqueFileName,

                };
                _db.videos.Add(videoModel);
               await _db.SaveChangesAsync();
            }
            return Ok();
        }
        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        [Route("DeleteVideo")]
        public async Task<IActionResult> DeleteVideo([FromForm] int? id)
        {
            var video =await _db.videos.Where(x => x.Id == id).FirstOrDefaultAsync();
            if(video == null)
            {
                return NotFound();
            }
            if(video.VideoPath != null)
            {
                var image = Path.Combine(_webHost.WebRootPath, "Video", video.VideoPath);
                if (System.IO.File.Exists(image))
                {
                    System.IO.File.Delete(image);
                }
               
            }
            _db.videos.Remove(video);
           await _db.SaveChangesAsync();
            return Ok();
        }
        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        [Route("UpdateVideo")]
        public async Task<IActionResult> UpdateVideo([FromForm]VideoVM model)
        {
            if (ModelState.IsValid)
            {
                var video =await _db.videos.FindAsync(model.Id);
                if(video == null)
                {
                    return NotFound();
                }
                video.VideoName = model.VideoName;
                if (model.VideoFile != null)
                {
                    if(video.VideoPath != null)
                    {
                        var image = Path.Combine(_webHost.WebRootPath, "Video", video.VideoPath);
                        System.IO.File.Delete(image);
                    }
                   
                    
                    string uniqueFileName = null;
                    string extension = Path.GetExtension(model.VideoFile.FileName);
                    if (extension.ToLower() == ".mp4")
                    {
                        string uploadFolder = Path.Combine(_webHost.WebRootPath, "video");
                        uniqueFileName = Guid.NewGuid().ToString() + "_" + model.VideoFile.FileName;
                        string filePath = Path.Combine(uploadFolder, uniqueFileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await model.VideoFile.CopyToAsync(stream);
                        }
                        video.VideoPath = uniqueFileName;
                    }
                    else
                    {
                       return BadRequest("Not Allowed");
                    }
                   
                }
                
                _db.videos.Update(video);
                await _db.SaveChangesAsync();
            }
              
            
            return Ok();
        }
   
    }
}

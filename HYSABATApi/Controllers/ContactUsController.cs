using HYSABATApi.Models;
using HYSABATApi.Models.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HYSABATApi.Controllers
{
    //[Authorize(Roles = UserRoles.Admin)]
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ContactUsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public ContactUsController(ApplicationDbContext db)
        {
            this._db = db;
        }
        [HttpGet]
        [Route("GetContact")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> GetContact()
        {
            var contact =await _db.contactUs.ToListAsync();
            return Ok(contact);
        }
        [HttpPost]
        [Route("CreateContact")]
        public async Task<IActionResult> CreateContact([FromBody]ContactUs model)
        {
            if (ModelState.IsValid)
            {
                _db.contactUs.Add(model);
               await _db.SaveChangesAsync();
            }
            return Ok();
        }
        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        [Route("DeleteContact")]
        public async Task<IActionResult> DeleteContact([FromBody]int id)
        {
            var contacts =await _db.contactUs.Where(x => x.Id == id).SingleOrDefaultAsync();
            if(contacts == null)
            {
                return NotFound();
            }
            _db.contactUs.Remove(contacts);
          await  _db.SaveChangesAsync();
            return Ok();
        }
      
    }
}

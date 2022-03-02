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
   
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyContactController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public CompanyContactController(ApplicationDbContext db)
        {
            this._db = db;
        }
        [HttpGet]
        public async Task<IActionResult> GetCompanyContact()
        {
            var companyContacts = await _db.companies.Select(x => new CompanyContactInformation() { 
            CompanyName = x.CompanyName,
            CompanyLocation = x.CompanyLocation,
            CompanyPhone = x.CompanyPhone,
            Email = x.Email
            }).ToListAsync();

            return Ok(companyContacts);
        }
        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        [Route("CreateCompanyContact")]
        public async Task<IActionResult> CreateCompanyContact([FromForm]CompanyContactInformation model)
        {
            if (ModelState.IsValid)
            {
             
             if(_db.companies.Count() <= 0)
                {
                    _db.companies.Add(model);
                   await _db.SaveChangesAsync();
                    return Ok(new CommonResponse { status = "success", Message = "Successfully Added" });
                }
                
              else if(_db.companies.Count() >= 1)
                {
                  
                    var company = _db.companies.Find(model.Id);
                    if (company == null)
                    {
                        return NotFound();
                    }
                    company.CompanyName = model.CompanyName;
                    company.CompanyPhone = model.CompanyPhone;
                    company.CompanyLocation = model.CompanyLocation;
                    company.Email = model.Email;
                    _db.companies.Update(company);
                    await _db.SaveChangesAsync();
                    return Ok(new CommonResponse { status = "success", Message = "Successfully Updated" });
                }
             
            }
            return Ok();
        }
      
    }
}

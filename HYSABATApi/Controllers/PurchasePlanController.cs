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
    
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PurchasePlanController : ControllerBase
    {
        private ApplicationDbContext _db;
        public PurchasePlanController(ApplicationDbContext db)
        {
            this._db = db;
        }
        [HttpGet]
        [Route("GetPurchasingPlan")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> GetPurchasingPlan()
        {
            var info = await (from x in _db.contactInformation select x).ToListAsync();
            return Ok(info);
        }
        [HttpPost]
        [Route("CreatePurchasingPlan")]
        public async Task<IActionResult> CreatePurchasingPlan([FromBody]PurchasePlan model)
        {
            if (ModelState.IsValid)
            {
                _db.contactInformation.Add(model);
               await _db.SaveChangesAsync();
            }
            return Ok();
        }
        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        [Route("DeletePurchasingPlan")]
        public async Task<IActionResult> DeletePurchasingPlan([FromForm]int id)
        {
            var contactInfo =await _db.contactInformation.Where( x => x.Id == id).FirstOrDefaultAsync();
            if(contactInfo == null)
            {
                return NotFound();
            }
            _db.contactInformation.Remove(contactInfo);
           await _db.SaveChangesAsync();
            return Ok();
        }
        //[HttpPost]
        //[Route("UpdateContactInfo")]
        //public async Task<IActionResult> UpdateContactInfo([FromBody]ContactInformation model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var contactInfo =await _db.contactInformation.FindAsync(model.Id);
        //        if(contactInfo == null)
        //        {
        //            return NotFound();
        //        }
        //        contactInfo.FirstName = model.FirstName;
        //        contactInfo.LastName = model.LastName;
        //        contactInfo.Email = model.Email;
        //        contactInfo.NumberOfBranches = model.NumberOfBranches;
        //        contactInfo.NumberOfCustomer = model.NumberOfCustomer;
        //        contactInfo.NumberOfSupliers = model.NumberOfSupliers;
        //        contactInfo.NumberOfUser = model.NumberOfUser;
        //        contactInfo.Postal_ZipCode = model.Postal_ZipCode;
        //        contactInfo.Postion = model.Postion;
        //        contactInfo.Company = model.Company;
        //        contactInfo.City = model.City;
        //        contactInfo.CRNumber = model.CRNumber;
        //        contactInfo.PricingPlanId = model.PricingPlanId;
        //        contactInfo.State = model.State;
        //        contactInfo.ExpiryDate = model.ExpiryDate;
        //        contactInfo.CVC = model.CVC;
        //        contactInfo.CreditCardNumber = model.CreditCardNumber;
        //        contactInfo.Country = model.Country;
        //        contactInfo.AutoRenew = model.AutoRenew;
        //        _db.contactInformation.Update(contactInfo);
        //       await _db.SaveChangesAsync();
        //    }
        //    return Ok();

        //}
    }
}

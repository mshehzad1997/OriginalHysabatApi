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
    public class QuestionController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public QuestionController(ApplicationDbContext db)
        {
            this._db = db;
        }
        [Route("GetQuestion")]
        [HttpGet]
        public async Task<IActionResult> GetQuestion()
        {
            var question =await _db.questions.Select(x => new Question() { 
            question = x.question,
            Answer = x.Answer
            }).ToListAsync();
            return Ok(question);
        }
        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        [Route("CreateQuestion")]
        public async Task<IActionResult> CreateQuestion([FromBody]Question model)
        {
            if (ModelState.IsValid)
            {
                _db.questions.Add(model);
               await _db.SaveChangesAsync();
            }
            return Ok();
        }
        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        [Route("DeleteQuestion")]
        public async Task<IActionResult> DeleteQuestion([FromForm] int? id)
        {
            var question =await _db.questions.Where(x => x.Id == id).FirstOrDefaultAsync();
            if(question == null)
            {
                return NotFound();
            }
            _db.questions.Remove(question);
           await _db.SaveChangesAsync();
            return Ok();
        }
        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        [Route("UpdateQuestion")]
        public async Task<IActionResult> UpdateQuestion(Question model)
        {
            if (ModelState.IsValid)
            {
                var questionAnswer =await _db.questions.FindAsync(model.Id);
                if(questionAnswer == null)
                {
                    return NotFound();
                }
                questionAnswer.question = model.question;
                questionAnswer.Answer = model.Answer;
                _db.questions.Update(questionAnswer);
               await _db.SaveChangesAsync();
            }
            return Ok();
        }
    }
}

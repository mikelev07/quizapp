using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EQuiz.MobileAppService.Models;

namespace EQuiz.MobileAppService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserTestsApiController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public UserTestsApiController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/UserTestsApi
        [HttpGet]
        public IEnumerable<UserTest> GetUserTests()
        {
            return _context.UserTests.Include(c => c.Answers);
        }

        // GET: api/UserTestsApi/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserTest([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userTest = await _context.UserTests.FindAsync(id);

            if (userTest == null)
            {
                return NotFound();
            }

            return Ok(userTest);
        }

        // PUT: api/UserTestsApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserTest([FromRoute] int id, [FromBody] UserTest userTest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userTest.Id)
            {
                return BadRequest();
            }

            _context.Entry(userTest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserTestExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/UserTestsApi
        [HttpPost]
        public async Task<IActionResult> PostUserTest([FromBody] UserTest userTest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.UserTests.Add(userTest);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserTest", new { id = userTest.Id }, userTest);
        }

        // DELETE: api/UserTestsApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserTest([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userTest = await _context.UserTests.FindAsync(id);
            if (userTest == null)
            {
                return NotFound();
            }

            _context.UserTests.Remove(userTest);
            await _context.SaveChangesAsync();

            return Ok(userTest);
        }

        private bool UserTestExists(int id)
        {
            return _context.UserTests.Any(e => e.Id == id);
        }
    }
}
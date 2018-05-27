using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GoodFoodAPI.Data;
using GoodFoodAPI.Models;

namespace GoodFoodAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Codes")]
    public class CodesController : Controller
    {
        private readonly GoodFoodContext _context;

        public CodesController(GoodFoodContext context)
        {
            _context = context;
        }

        // GET: api/Codes
        [HttpGet]
        public IEnumerable<Code> GetCode()
        {
            return _context.Code;
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> GetCode([FromRoute] string code)
        {
            var result = await _context.Code.SingleOrDefaultAsync(c => c.code.Equals(code));
            
            if(result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // GET: api/Codes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCode([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var code = await _context.Code.SingleOrDefaultAsync(m => m.codeId == id);

            if (code == null)
            {
                return NotFound();
            }

            return Ok(code);
        }

        // PUT: api/Codes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCode([FromRoute] int id, [FromBody] Code code)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != code.codeId)
            {
                return BadRequest();
            }

            _context.Entry(code).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CodeExists(id))
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

        // POST: api/Codes
        [HttpPost]
        public async Task<IActionResult> PostCode([FromBody] Code code)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Code.Add(code);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCode", new { id = code.codeId }, code);
        }

        // POST: api/Codes
        [HttpPost]
        public async Task<IActionResult> PostCode([FromBody] int userId, string code)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _context.Code.SingleOrDefaultAsync(c => c.code.Equals(code));

            if (result == null)
            {
                return NotFound();
            }

            var user = await _context.User.SingleOrDefaultAsync(u => u.userId == userId);
            user.stamps++;
            if (user.stamps == 10)
            {
                user.freeDishes++;
                user.stamps = 0;
            }

            _context.Update(user);

            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/Codes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCode([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var code = await _context.Code.SingleOrDefaultAsync(m => m.codeId == id);
            if (code == null)
            {
                return NotFound();
            }

            _context.Code.Remove(code);
            await _context.SaveChangesAsync();

            return Ok(code);
        }

        private bool CodeExists(int id)
        {
            return _context.Code.Any(e => e.codeId == id);
        }
    }
}
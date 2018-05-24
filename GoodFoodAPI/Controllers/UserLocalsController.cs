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
    [Route("api/UserLocals")]
    public class UserLocalsController : Controller
    {
        private readonly GoodFoodContext _context;

        public UserLocalsController(GoodFoodContext context)
        {
            _context = context;
        }

        // GET: api/UserLocals/{userId}
        [HttpGet("{userId}")]
        public IActionResult GetFavouriteLocalsByUserId([FromRoute] int userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var locals = _context.Local.AsNoTracking().Include(l => l.userLocals).Where(m => m.userLocals.Any(ul => ul.userId == userId));

            if (locals == null)
            {
                return NotFound();
            }

            return Ok(locals);
        }

        // GET: api/UserLocals?userId={id}&localId={id}
        [HttpGet]
        public IActionResult IsFavouriteLocal(int userId, int localId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //var local = _context.Local.AsNoTracking().Include(l => l.userLocals).Where(m => m.userLocals.Any(ul => ul.userId == userId) && m.localId == localId);
            var result = from local in _context.Local
                         join userLocals in _context.UserLocals on local.localId equals userLocals.localId
                         where local.localId == localId
                         select local.localId;
            if (result.ToList().Count() == 0)
            {
                return NotFound();
            }

            return Ok();
        }

        // PUT: api/UserLocals/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserLocals([FromRoute] int id, [FromBody] UserLocals userLocals)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userLocals.userId)
            {
                return BadRequest();
            }

            _context.Entry(userLocals).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserLocalsExists(id))
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

        // POST: api/UserLocals
        [HttpPost]
        public async Task<IActionResult> PostUserLocals([FromBody] UserLocals userLocals)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.UserLocals.Add(userLocals);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserLocalsExists(userLocals.userId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUserLocals", new { id = userLocals.userId }, userLocals);
        }

        // DELETE: api/UserLocals/5
        [HttpDelete]
        public async Task<IActionResult> DeleteUserLocals([FromBody] UserLocals userLocal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userLocals = await _context.UserLocals.SingleOrDefaultAsync(m => m.userId == userLocal.userId && m.localId == userLocal.localId);
            if (userLocals == null)
            {
                return NotFound();
            }

            _context.UserLocals.Remove(userLocals);
            await _context.SaveChangesAsync();

            return Ok(userLocals);
        }

        private bool UserLocalsExists(int id)
        {
            return _context.UserLocals.Any(e => e.userId == id);
        }
    }
}
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
    [Route("api/UserDishes")]
    public class UserDishesController : Controller
    {
        private readonly GoodFoodContext _context;

        public UserDishesController(GoodFoodContext context)
        {
            _context = context;
        }

        // GET: api/Users/{userId}
        [HttpGet("{userId}")]
        public IActionResult GetFavouriteDishesByUserId([FromRoute] int userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dishes = _context.Dish.AsNoTracking().Include(d => d.userDishes).Where(m => m.userDishes.Any(ul => ul.userId == userId));

            if (dishes == null)
            {
                return NotFound();
            }

            return Ok(dishes);
        }

        // GET: api/Users/UserDishes?userId={id}&dishId={id}
        [HttpGet]
        public IActionResult IsFavouriteDish(int userId, int dishId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //var local = _context.Local.AsNoTracking().Include(l => l.userLocals).Where(m => m.userLocals.Any(ul => ul.userId == userId) && m.localId == localId);
            var result = from dish in _context.Dish
                         join userDishes in _context.UserDishes on dish.dishId equals userDishes.dishId
                         where dish.dishId == dishId
                         select dish.dishId;
            if (result.ToList().Count() == 0)
            {
                return NotFound();
            }

            return Ok();
        }

        // PUT: api/UserDishes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserDishes([FromRoute] int id, [FromBody] UserDishes userDishes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userDishes.userId)
            {
                return BadRequest();
            }

            _context.Entry(userDishes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserDishesExists(id))
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

        // POST: api/UserDishes
        [HttpPost]
        public async Task<IActionResult> PostUserDishes([FromBody] UserDishes userDishes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.UserDishes.Add(userDishes);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserDishesExists(userDishes.userId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUserDishes", new { id = userDishes.userId }, userDishes);
        }

        // DELETE: api/UserDishes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserDishes([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userDishes = await _context.UserDishes.SingleOrDefaultAsync(m => m.userId == id);
            if (userDishes == null)
            {
                return NotFound();
            }

            _context.UserDishes.Remove(userDishes);
            await _context.SaveChangesAsync();

            return Ok(userDishes);
        }

        private bool UserDishesExists(int id)
        {
            return _context.UserDishes.Any(e => e.userId == id);
        }
    }
}
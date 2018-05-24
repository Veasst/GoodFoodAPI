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

        // GET: api/userDishes/{userId}
        [HttpGet("{userId}")]
        public IActionResult GetFavouriteDishesByUserId([FromRoute] int userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dishes = from dish in _context.Dish
                         join dishType in _context.DishType on dish.dishType equals dishType
                         join userDish in _context.UserDishes on dish.dishId equals userDish.dishId
                         where userDish.userId == userId
                         select new { dish.dishId, dish.dishName, dish.description, dish.ingredients, dish.logoPath, dish.price, dishType.dishType };

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

            return Ok();
        }

        // DELETE: api/userDishes/
        [HttpDelete]
        public async Task<IActionResult> DeleteUserDishes([FromBody] UserDishes userDishes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userDish = await _context.UserDishes.SingleOrDefaultAsync(m => m.userId == userDishes.userId && m.dishId == userDishes.dishId);
            if (userDish == null)
            {
                return NotFound();
            }

            _context.UserDishes.Remove(userDish);
            await _context.SaveChangesAsync();

            return base.Ok((object)userDish);
        }

        private bool UserDishesExists(int id)
        {
            return _context.UserDishes.Any(e => e.userId == id);
        }
    }
}
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GoodFoodAPI.Data;
using GoodFoodAPI.Models;
using Microsoft.AspNetCore.Http;

namespace GoodFoodAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Menu")]
    public class MenuController : Controller
    {
        private readonly GoodFoodContext _context;

        public MenuController(GoodFoodContext context)
        {
            _context = context;
        }

        // GET: api/Menu?localId=5
        [HttpGet]
        public IActionResult GetMenuByLocalId(int localId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = from local in _context.Local
                         join localDish in _context.LocalDishes on local.localId equals localDish.localId
                         join dish in _context.Dish on localDish.dishId equals dish.dishId
                         join dishType in _context.DishType on dish.dishType equals dishType
                         where local.localId == localId
                         select new { dish.dishId, dish.dishName, dish.description, dish.ingredients, dish.logoPath, dish.price, dishType.dishType };

            if (result.Count() == 0)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // GET: api/Menu?localId=5&dishType=KOLACJA
        [HttpGet("bydishType")]
        public IActionResult GetMenuByLocalIdAndDishType(int localId, string dishType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = from local in _context.Local
                         join localDish in _context.LocalDishes on local.localId equals localDish.localId
                         join dish in _context.Dish on localDish.dishId equals dish.dishId
                         join dType in _context.DishType on dish.dishType equals dType
                         where local.localId == localId && dType.dishType.Equals(dishType, System.StringComparison.InvariantCultureIgnoreCase)
                         select new { dish.dishId, dish.dishName, dish.description, dish.ingredients, dish.logoPath, dish.price, dType.dishType };

            if (result.Count() == 0)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // POST: api/Menu
        [HttpPost]
        public async Task<IActionResult> PostLocalDishes([FromBody] LocalDishes localDishes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.LocalDishes.Add(localDishes);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (LocalDishesExists(localDishes.localId, localDishes.dishId))
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

        private bool LocalDishesExists(int localId, int dishId)
        {
            return _context.LocalDishes.Any(e => e.localId == localId && e.dishId == dishId);
        }
    }
}
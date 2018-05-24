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
    [Route("api/Locals")]
    public class LocalsController : Controller
    {
        private readonly GoodFoodContext _context;

        public LocalsController(GoodFoodContext context)
        {
            _context = context;
        }

        // GET: api/Locals
        [HttpGet]
        public ActionResult GetLocal()
        {
            var list = _context.Local.Include(l => l.localDishes).ThenInclude(dish => dish.dish).ThenInclude(dish => dish.dishType).ToList();
            return Ok(list);
        }

        // GET: api/Locals/5
        [HttpGet("{id}")]
        public IActionResult GetLocal([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = from local in _context.Local
                         join localDish in _context.LocalDishes on local.localId equals localDish.localId
                         join dish in _context.Dish on localDish.dishId equals dish.dishId
                         join dishType in _context.DishType on dish.dishType equals dishType
                         where local.localId == id
                         select new { dish.dishId, dish.dishName, dish.description, dish.ingredients, dish.logoPath, dish.price, dishType.dishType};

            if (result.Count() == 0)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
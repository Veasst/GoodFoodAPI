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

        // GET: api/Locals/1
        [HttpGet("{id}")]
        public ActionResult GetLocal(int id)
        {
            var local = _context.Local.Include(l => l.localDishes).ThenInclude(dish => dish.dish).ThenInclude(dish => dish.dishType).SingleOrDefault(l => l.localId == id);
            return Ok(local);
        }

        // GET: api/Locals/bypattern?pattern={pattern}
        [HttpGet("bypattern")]
        public IActionResult GetLocalByPattern(string pattern)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var locals = from local in _context.Local
                         where local.name.StartsWith(pattern) || local.address.StartsWith(pattern)
                         select new { local.localId, local.name, local.address, local.description, local.logoPath };

            if (locals.Count() == 0)
            {
                return NotFound();
            }

            return Ok(locals);
        }

        // GET: api/Locals/dishId?dishId={dishId}
        [HttpGet("dishId")]
        public IActionResult GetLocalsByDish(int dishId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var locals = from local in _context.Local
                         join localDish in _context.LocalDishes on local.localId equals localDish.localId
                         where localDish.dishId == dishId
                         select new { local.localId, local.name, local.address, local.description, local.logoPath };

            if (locals.Count() == 0)
            {
                return NotFound();
            }

            return Ok(locals);
        }
    }
}
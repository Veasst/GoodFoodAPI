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
    [Route("api/Dishes")]
    public class DishesController : Controller
    {
        private readonly GoodFoodContext _context;

        public DishesController(GoodFoodContext context)
        {
            _context = context;
        }

        // GET: api/Dishes
        [HttpGet]
        public List<Dish> GetDish()
        {
            return _context.Dish.ToList();
        }
    }
}
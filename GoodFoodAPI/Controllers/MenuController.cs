using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GoodFoodAPI.Data;
using GoodFoodAPI.Models;

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
    }
}
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GoodFoodAPI.Data;
using GoodFoodAPI.Models;
using System.Collections.Generic;

namespace GoodFoodAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Users")]
    public class UsersController : Controller
    {
        private readonly GoodFoodContext _context;

        public UsersController(GoodFoodContext context)
        {
            _context = context;
        }

        // GET: api/Users?username=?&password=?
        [HttpGet]
        public IActionResult GetUser(string username, string password)
        {
            foreach (User u in _context.User)
            {
                if (u.username.Equals(username) && u.password.Equals(password))
                {
                    return Ok(u);
                }
            }

            return NotFound();
        }

        // GET: api/Users/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.User.SingleOrDefaultAsync(m => m.userId == id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // GET: api/Users/favourite?username={name}
        [HttpGet("favourite/")]
        public async Task<IActionResult> GetFavouriteById(int userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.User.Include(u => u.userDishes).ThenInclude(ud => ud.dish).Include(u => u.userLocals).ThenInclude(ud => ud.local).SingleOrDefaultAsync(m => m.userId == userId);            

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser([FromRoute] int id, [FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.userId)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            foreach (User u in _context.User)
            {
                if (u.username.Equals(user.username))
                {
                    return BadRequest();
                }
            }

            _context.User.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.userId }, user);
        }

        private bool UserLocalsExists(int id)
        {
            return _context.UserLocals.Any(e => e.userId == id);
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.userId == id);
        }
    }
}
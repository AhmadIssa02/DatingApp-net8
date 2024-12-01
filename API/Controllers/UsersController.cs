using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{

    public class UsersController(DataContext context) : BaseApiController
    {

        //private readonly DataContext _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            var users = await context.Users.ToListAsync();

            // Loop through the users and print their details
            foreach (var user in users)
            {
                Console.WriteLine($"User ID: {user.Id}, Username: {user.Username}");
            }

            return users;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<AppUser>> GetUsers(int id)
        {
            var user = await context.Users.FindAsync(id);

            if (user == null) { return NotFound(); }

            return user;
        }
    }
}

using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{

    public class UsersController : BaseApiController
    {
        private readonly DataContext _context;

        public UsersController(DataContext context)
        {
            _context = context;
        }

        //ng [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<appUser>>> GetUsers()
        {
            var Users = await _context.Users.ToListAsync();
            return Users;
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<appUser>> GetUser(int id)
        {
            var User = await _context.Users.FindAsync(id);
            return User;
        }
    }
}
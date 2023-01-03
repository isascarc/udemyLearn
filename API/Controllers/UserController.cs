using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")] //  /api/Users
    public class UsersController
    {
        private readonly DataContext _context;

        public UsersController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task< ActionResult<IEnumerable<appUser>>> GetUsers()
        {
            var Users = await _context.Users.ToListAsync();
            return Users;
        }

        [HttpGet("{id}")]
        public async Task< ActionResult<appUser>> GetUser(int id)
        {
            var User =await _context.Users.FindAsync(id);
            return User;
        }
    }
}
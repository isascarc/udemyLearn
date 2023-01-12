using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        public DataContext _context { get; }
        public ITokenService _tokenService { get; }

        public AccountController(DataContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost("register")] // post:  api/account/register
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await UserExist(registerDto.userName))
                return BadRequest("username is taken");
            //string username, string password
            using var hmac = new HMACSHA512();
            var user = new AppUser
            {
                UserName = registerDto.userName.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.password)),
                PasswordSalt = hmac.Key
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new UserDto
            {
                UserName=user.UserName,
                Token= _tokenService.CreateToken(user)
            };
        }

        [HttpPost("login")] // post:  api/account/register
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            //var allUsers = _context.Users.ToList();
            //var user = await _context.Users.SingleOrDefaultAsync(x=>x.UserName==loginDto.Username.ToLower());
            var user = await _context.Users.SingleOrDefaultAsync(x=>x.UserName==loginDto.Username);
            if (user==null)
                return Unauthorized();
            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.password));
            for(int i=0; i<computedHash.Length;i++)
            {
                if (computedHash[i] != user.PasswordHash[i])
                return Unauthorized("invalid password!!");
            }
            return new UserDto
            {
                UserName=user.UserName,
                Token=  _tokenService.CreateToken(user)
            };
        }

        public async Task<bool> UserExist(string username)
        {
            return await _context.Users.AnyAsync(x => x.UserName == username.ToLower());
        }
    }
}
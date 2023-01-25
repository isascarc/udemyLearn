using System.Security.Cryptography;
using System.Text;
using API.Interfaces;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        public DataContext _context { get; }
        public ITokenService _tokenService { get; }
        private readonly IMapper _mapper;

        public AccountController(DataContext context, ITokenService tokenService, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost("register")] // post:  api/account/register
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {

            if (await UserExist(registerDto.username))
                return BadRequest("username is taken");

            var user = _mapper.Map<AppUser>(registerDto);
            //string username, string password
            using var hmac = new HMACSHA512();

            user.Username = registerDto.username.ToLower();
            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.password));
            user.PasswordSalt = hmac.Key;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new UserDto
            {
                Username = user.Username,
                Token = _tokenService.CreateToken(user),
                KnownAs=user.KnownAs
            };
        }

        [HttpPost("login")] // post:  api/account/register
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            //var allUsers = _context.Users.ToList();
            //var user = await _context.Users.SingleOrDefaultAsync(x=>x.UserName==loginDto.Username.ToLower());
            var user = await _context.Users.Include(x => x.Photos).
                FirstOrDefaultAsync(x => x.Username == loginDto.Username);
            if (user == null)
                return Unauthorized();
            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.password));
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i])
                    return Unauthorized("invalid password!!");
            }
            return new UserDto
            {
                Username = user.Username,
                Token = _tokenService.CreateToken(user),
                PhotoUrl = user.Photos.FirstOrDefault(x => x.isMain)?.url,
                KnownAs=user.KnownAs
            };
        }

        public async Task<bool> UserExist(string username)
        {
            return await _context.Users.AnyAsync(x => x.Username == username.ToLower());
        }
    }
}
using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController(DataContext context, ITokenService tokenService) : BaseApiController
    {
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if(await UserExists(registerDto.username)) return BadRequest("username already exists");
            return Ok();
            //using var hmac = new HMACSHA512();

            //var user = new AppUser
            //{
            //    Username = registerDto.username,
            //    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.password)),
            //    PasswordSalt = hmac.Key
            //};
            //context.Users.Add(user);
            //await context.SaveChangesAsync();
            //return new UserDto
            //{
            //    username = registerDto.username,
            //    token = tokenService.CreateToken(user)
            //};
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {

            var user = await context.Users.FirstOrDefaultAsync(x=>
            x.Username == loginDto.username.ToLower());
            if (user == null) return Unauthorized("Invalid username");

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.password));
            Console.WriteLine("username and password submitted: " + loginDto.username + "   " + loginDto.password);
            Console.WriteLine("username and password in db: " +  user.Username + "   " + user.PasswordHash.ToString());

            if (computedHash.Length != user.PasswordHash.Length) return Unauthorized("Invalid Password");
            for (int i = 0; i< computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid Password") ;
            }

            return new UserDto
            {
                username = loginDto.username,
                token = tokenService.CreateToken(user)
            };
        }
        private async Task<bool> UserExists(string username)
        {
            return await context.Users.AnyAsync(x=> x.Username.ToLower() == username.ToLower());
        }
    }
    
}

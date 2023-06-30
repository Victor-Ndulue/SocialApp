using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialAppApi.Data;
using SocialAppApi.Dtos;
using SocialAppApi.Entities;
using SocialAppApi.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace SocialAppApi.Controllers
{
    public class AccountController: BaseController 
    {
        private readonly DataContext _dataContext;
        private readonly ITokenService _tokenService;

        public AccountController(DataContext dataContext, ITokenService tokenService)
        {
            _dataContext = dataContext;
            _tokenService = tokenService;
        }



        [HttpPost("register")]
        public async Task <ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await UserExists(registerDto.UserName)) { return BadRequest("Username is taken"); }
            
            using var hmac = new HMACSHA512();
            //Concrete instantiation to be replaced with AutoMapper mapping and methods implemented in Dto constructor
            var user = new AppUser
            {   
                UserName = registerDto.UserName.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };
            _dataContext.Users.Add(user);
            await _dataContext.SaveChangesAsync();

            return new UserDto
            {
                UserName = user.UserName,
                Token = _tokenService.CreateToken(user),
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _dataContext.Users.SingleOrDefaultAsync(u => u.UserName == loginDto.UserName.ToLower());
            if (user == null)
            {
                return Unauthorized("Invalid Username or Password");
            }

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
            for (int i = 0; i < PasswordHash.Length; i++)
            {
                if (user.PasswordHash[i] != PasswordHash[i])
                {
                    return Unauthorized("Invalid Username or Password");
                }
            }

            return new UserDto
            {
                UserName = user.UserName,
                Token = _tokenService.CreateToken(user),
            };
        }

        private async Task<bool> UserExists(string userName)
        {
            return await _dataContext.Users.AnyAsync(user => user.UserName == userName.ToLower());
        }
    }
}

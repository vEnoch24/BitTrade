using BitTrade.Data;
using BitTrade.Dto;
using BitTrade.Model;
using BitTrade.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using Vonage.Users;

namespace BitTrade.Controller
{
    public class AccountController : ControllerBase
    {
        private readonly NavigationManager _navigationManager;
        private readonly BitTradeDbContext _dbContext;
        private readonly TokenService _tokenService;

        public AccountController(BitTradeDbContext dbContext, TokenService tokenService, NavigationManager navigationManager)
        {
            _dbContext = dbContext;
            _tokenService = tokenService;
            _navigationManager = navigationManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto, CancellationToken cancellationToken)
        {
            var usernameExist = await _dbContext.Users.AsNoTracking().AnyAsync(u => u.Email == dto.Email, cancellationToken);

            if (usernameExist)
            {
                return BadRequest($"{nameof(dto.Email)} already exist");
            }

            CreatePasswordHash(dto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new ApplicationUser
            {
                Name = dto.Name,
                Email = dto.Email,
                passwordhash = passwordHash,
                passwordSalt = passwordSalt,
            };

            await _dbContext.Users.AddAsync(user, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Ok(GenerateToken(user));
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (user == null)
            {
                return BadRequest("User not found");
            }
            if (!VerifyPasswordHash(dto.Password, user.passwordhash, user.passwordSalt))
            {
                return BadRequest("Wrong Password!");
            }

            return Ok(GenerateToken(user));
        }


        private AuthResponseDto GenerateToken(ApplicationUser user)
        {
            var token = _tokenService.CreateToken(user);
            return new AuthResponseDto(new UserDto(user.Id, user.Name, user.Email), token);
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Backend.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Backend.Services
{
    public interface IAuthService
    {
        Task<bool> Register(string username, string password);
        Task<string?> Login(string username, string password);
        Task<Guid?> GetUserIdFromToken(string token);
    }

    public class AuthService(Db dbContext, IConfiguration configuration) : IAuthService
    {
        private readonly Db _dbContext = dbContext;
        private readonly IConfiguration _configuration = configuration;

        public async Task<bool> Register(string username, string password)
        {
            if (await _dbContext.Users.AnyAsync(u => u.Username == username))
            {
                return false; // User already exists
            }

            var user = new User(username, password);

            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<string?> Login(string username, string password)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
            if (user == null)
            {
                return null; // Invalid credentials
            }
           
            return generateJwtToken(user.Id.ToString(), user.Username);
        }

        public async Task<Guid?> GetUserIdFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token.Replace("Bearer ", string.Empty));
            var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            
            if (userIdClaim == null)
            {
                return null; // Invalid token
            }
            if (!Guid.TryParse(userIdClaim.Value, out var userId))
            {
                return null; // Invalid user ID
            }
            if (!await _dbContext.Users.AnyAsync(u => u.Id == userId))
            {
                return null; // User not found
            }

            return userId;
        }

        private string generateJwtToken(string userId, string username)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Name, username)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpiresInMinutes"])),
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

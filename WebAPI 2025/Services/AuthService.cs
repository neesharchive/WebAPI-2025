using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI_2025.DTOs.UserDTO;
using WebAPI_2025.Models.Entities;
using WebAPI_2025.Repositories;

namespace WebAPI_2025.Services
{
    public class AuthService : IAuthService
    {
        private readonly IGetUser _repo;
        private readonly IConfiguration _configuration;
        public AuthService(IGetUser getUser, IConfiguration configuration)
        {
            _repo = getUser;
            _configuration = configuration;
        }

        public async Task<UserResponseDTO?> Login(LoginDTO loginDTO)
        {
            var user = await _repo.GetUserByUsernamePassword(loginDTO.Username, loginDTO.Password);
            if (user == null) return null;

            var token = GenerateJwt(user);
            return new UserResponseDTO
            {
                Token = token,
                Role = user.role.ToString(),
                UserID=user.UserID

            };
        }

        private string GenerateJwt(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.role.ToString()),
                new Claim("userID", user.UserID.ToString())

            };
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

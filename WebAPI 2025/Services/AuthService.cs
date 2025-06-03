using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI_2025.DTOs.UserDTO;
using WebAPI_2025.Models.Entities;
using WebAPI_2025.Models.Wrappers;
using WebAPI_2025.Repositories;

namespace WebAPI_2025.Services
{
    public class AuthService : IAuthService
    {
        private readonly IGetUserRepositoroy _repo;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;

        public AuthService(IGetUserRepositoroy repo, IConfiguration configuration, IEmailService emailService)
        {
            _repo = repo;
            _configuration = configuration;
            _emailService = emailService;

        }

        public async Task<UserResponseDTO?> Login(LoginDTO loginDTO)
        {
            var user = await _repo.GetUserByUsername(loginDTO.Username);
            if (user == null) return null;

            var hasher = new PasswordHasher<User>();
            var result = hasher.VerifyHashedPassword(user, user.Password, loginDTO.Password);
            if (result != PasswordVerificationResult.Success) return null;

            var token = GenerateJwt(user);
            return new UserResponseDTO
            {
                Token = token,
                Role = user.role.ToString(),
                UserID = user.UserID
            };
        }

        public async Task<bool> RequestPasswordReset(string email)
        {
            var user = await _repo.GetUserByEmail(email); // ✅ you'll need this new method
            if (user == null) return false;

            user.PasswordResetToken = Guid.NewGuid().ToString();
            user.TokenExpiryTime = DateTime.UtcNow.AddHours(1);
            await _repo.SaveChangesAsync();

            var resetLink = $"http://localhost:4200/login/reset-password?token={user.PasswordResetToken}";
            var emailBody = $@"
        <p>Hello {user.UserName},</p>
        <p>Click <a href='{resetLink}'>here</a> to reset your password.</p>";

            await _emailService.SendEmailAsync(user.Email, "Reset Your Password", emailBody);
            return true;
        }


        public async Task<bool> ResetPassword(string token, string newPassword)
        {
            var user = await _repo.GetUserByResetToken(token);
            if (user == null) return false;

            var hasher = new PasswordHasher<User>();
            user.Password = hasher.HashPassword(user, newPassword);
            user.PasswordResetToken = null;
            user.TokenExpiryTime = null;

            return await _repo.SaveChangesAsync();
        }

        public async Task<bool> ChangePassword(int userId, string currentPassword, string newPassword)
        {
            var user = await _repo.GetUserById(userId);
            if (user == null) return false;

            var hasher = new PasswordHasher<User>();
            var result = hasher.VerifyHashedPassword(user, user.Password, currentPassword);
            if (result != PasswordVerificationResult.Success) return false;

            user.Password = hasher.HashPassword(user, newPassword);
            return await _repo.SaveChangesAsync();
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

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}

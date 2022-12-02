using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PhloxAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PhloxAPI.Services.AccountsService.JwtProvider
{
    public class JwtProvider : IJwtProvider
    {
        private readonly JwtOptions _options;
        public JwtProvider(IOptions<JwtOptions> options)
        {
            _options = options.Value;
        }

        public string GenerateToken(User user)
        {
            var claims = new Claim[] {
                new Claim(ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("AS5H68&2aAssgh9209*&lkasdADASDfnDASDasyd"));

            var signingCredentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256
            );

            var token = new JwtSecurityToken(
                "https://joydipkanjilal.com/",
                "https://localhost:7257",
                claims,
                null,
                DateTime.UtcNow.AddDays(1),
                signingCredentials
            );

            string tokenVal = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenVal;
        }
    }
}

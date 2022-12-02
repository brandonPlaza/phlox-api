using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PhloxAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PhloxAPI.Services.AccountsService.JwtProvider
{
    public class JwtProvider : IJwtProvider
    {
        public string GenerateToken(User user)
        {
            var claims = new Claim[] {
                new Claim(ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("ASJDHBD43764*#&$^@*hdkwhre239847gdfgdfASD9847iu"));

            var signingCredentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256
            );

            var token = new JwtSecurityToken(
                "issuer",
                "audience",
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

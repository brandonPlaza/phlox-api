using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PhloxAPI.Data;
using PhloxAPI.Models;
using PhloxAPI.Services.AccountsService.JwtProvider;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace PhloxAPI.Services.AccountsService
{
    public class AccountsService : IAccountsService
    {
        private readonly PhloxDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IJwtProvider _jwtProvider;

        public AccountsService(PhloxDbContext context, IConfiguration configuration, IJwtProvider jwtProvider)
        {
            _context = context;
            _configuration = configuration;
            _jwtProvider = jwtProvider;
        }

        public string Login(string username, string password)
        {
            var user = _context.Users.SingleOrDefault(u => u.Username == username);
            if (user != null && MatchPasswords(user.Password, password, Convert.FromBase64String(user.Salt)))
            {
                var jwt = _jwtProvider.GenerateToken(user);

                return jwt;
            }
            return "";
        }

        public string RegisterUser(string firstname, string lastname, string username, string email, string password)
        {
            //var newUser = new User { FirstName = firstname, LastName = lastname, Username = username, Email=email, Password = password, Salt = ""};

            if (_context.Users.FirstOrDefault(u => u.Email == email) != null) 
            {
                return "Error: A user with that email already exists";
            }
            
            byte[] salt = new byte[128 / 8];
            while (true)
            {
                using (var rngCsp = new RNGCryptoServiceProvider())
                {
                    rngCsp.GetNonZeroBytes(salt);
                }
                if (_context.Users.SingleOrDefault(user => user.Salt == Convert.ToBase64String(salt)) == null)
                {
                    break;
                }
            }

            var hashedPass = HashPassword(password, salt);

            var newUser = new User { 
                FirstName = firstname, 
                LastName = lastname, 
                Username = username,
                Email = email, 
                Password = hashedPass, 
                Salt = Convert.ToBase64String(salt) 
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();
            return "User created";
        }

        private static string HashPassword(string password, byte[] salt)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8
            ));
        }

        private static bool MatchPasswords(string hashedUserPass, string plaintextUserPass, byte[] salt)
        {
            string tempHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: plaintextUserPass,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8
            ));
            return hashedUserPass.Equals(tempHash);
        }
    }
}

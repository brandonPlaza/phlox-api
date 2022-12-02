using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using PhloxAPI.Data;
using PhloxAPI.Models;
using System.Security.Cryptography;

namespace PhloxAPI.Services.AccountsService
{
    public class AccountsService : IAccountsService
    {
        private readonly PhloxDbContext _context;

        public AccountsService(PhloxDbContext context)
        {
            _context = context;
        }

        public string Login(string username, string password)
        {
            var user = _context.Users.SingleOrDefault(u => u.Username == username && u.Password == password);
            if (user != null)
                return user.Username;
            else
                return string.Empty;
        }

        public void RegisterUser(string firstname, string lastname, string username, string email, string password)
        {
            //var newUser = new User { FirstName = firstname, LastName = lastname, Username = username, Email=email, Password = password, Salt = ""};
            
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

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
            var user = _context.Users.SingleOrDefault(u => u.Username == username);
            if (user != null && MatchPasswords(user.Password, password, Convert.FromBase64String(user.Salt)))
            {
                return user.Username;
            }
            return "Username or password is incorrect";
        }

        public string RegisterUser(string firstname, string lastname, string username, string email, string password)
        {
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

            var newUser = new User
            {
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

        public void AddFavAmenity(Amenity amenity, string username)
        {
            var user = _context.Users.Include(u => u.FavouriteAmenities).FirstOrDefault(u => u.Username == username);
            if(user.FavouriteAmenities == null)
            {
                user.FavouriteAmenities = new List<Amenity>{
                    amenity
                };
                return;
            }
            user.FavouriteAmenities.Add(amenity);
        }

        public List<Amenity> GetFavAmenities(string username)
        {
            var user = _context.Users.Include(u => u.FavouriteAmenities).FirstOrDefault(u => u.Username == username);
            return user.FavouriteAmenities;
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
            string tempHash = HashPassword(plaintextUserPass, salt);
            return hashedUserPass.Equals(tempHash);
        }
    }
}

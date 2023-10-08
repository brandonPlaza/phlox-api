using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using PhloxAPI.Data;
using PhloxAPI.Models.DTOs;
using PhloxAPI.Models.Entities;
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

        public string Login(UserLoginDTO userLoginDTO)
        {
            var user = _context.Users.SingleOrDefault(u => u.Username == userLoginDTO.Username);
            if (user != null && MatchPasswords(user.Password, userLoginDTO.Password, Convert.FromBase64String(user.Salt)))
            {
                return user.Username;
            }
            return "Username or password is incorrect";
        }

        public string RegisterUser(UserDTO user)
        {
            if (_context.Users.FirstOrDefault(u => u.Email == user.Email) != null)
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

            var hashedPass = HashPassword(user.Password, salt);

            var newUser = new User
            {
                FirstName = user.Firstname,
                LastName = user.Lastname,
                Username = user.Username,
                Email = user.Email,
                Password = hashedPass,
                Salt = Convert.ToBase64String(salt)
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();
            return "User created";
        }

        public void AddFavAmenity(NodeDTO amenityDTO, string username)
        {
            var user = _context.Users.Include(u => u.FavouriteAmenities).FirstOrDefault(u => u.Username == username);

            var amenity = _context.Amenities.FirstOrDefault(a => a.Name == amenityDTO.Name);
            if(user.FavouriteAmenities == null)
            {
                user.FavouriteAmenities = new List<Node>{
                    amenity
                };
                return;
            }
            user.FavouriteAmenities.Add(amenity);
            _context.SaveChanges();
        }

        public List<Node> GetFavAmenities(string username)
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

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

    public UserDTO Login(UserLoginDTO userLoginDTO)
    {
      var user = _context.Users.SingleOrDefault(u => u.Username == userLoginDTO.Username);

      if (user != null && MatchPasswords(user.Password, userLoginDTO.Password, Convert.FromBase64String(user.Salt)))
      {
        var userDto = new UserDTO
        {
          Email = user.Email,
          Firstname = user.FirstName,
          Lastname = user.LastName,
          Username = user.Username,
          FavouriteAmenities = user.FavouriteAmenities,
        };

        return userDto;
      }
      return null;
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

    public string AddFavouriteAmenity(string amenityName, string username)
    {
      var user = _context.Users.Include(u => u.FavouriteAmenities).FirstOrDefault(u => u.Username == username);
      var amenity = _context.Nodes.Include(a => a.Reports).Include(a => a.OutOfServiceHistory).FirstOrDefault(a => a.Name == amenityName);

      if (user == null)
      {
        return "User does not exist";
      }

      if (amenity == null)
      {
        return "Amenity does not exist";
      }

      if (user.FavouriteAmenities == null)
      {
        user.FavouriteAmenities = new List<Node>{
                    amenity
                };
        _context.SaveChanges();
        return "User favourite list created and favourite added";
      }
      else
      {
        user.FavouriteAmenities.Add(amenity);
        _context.SaveChanges();
        return "Favourite added";
      }
    }

    public string RemoveFavouriteAmenity(string amenityName, string username)
    {
      var user = _context.Users.Include(u => u.FavouriteAmenities).FirstOrDefault(u => u.Username == username);
      var amenity = _context.Nodes.FirstOrDefault(a => a.Name == amenityName);

      if (user == null)
      {
        return "User does not exist";
      }

      if (amenity == null)
      {
        return "Amenity does not exist";
      }

      if (user.FavouriteAmenities == null)
      {
        return "User has no favourite amenities";
      }
      else
      {
        user.FavouriteAmenities.Remove(amenity);
        _context.SaveChanges();
        return "Favourite removed";
      }
    }


    public List<Node> GetFavouriteAmenities(string username)
    {
      var user = _context.Users.Include(u => u.FavouriteAmenities).FirstOrDefault(u => u.Username == username);
      if (user != null)
      {
        return user.FavouriteAmenities;
      }
      else
      {
        return null;
      }
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

    public User? GetUserByEmail(string email)
    {
      var user = _context.Users.FirstOrDefault(r => r.Email.Equals(email));
      return user;
    }
  }
}

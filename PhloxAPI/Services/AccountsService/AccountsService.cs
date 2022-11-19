using Microsoft.EntityFrameworkCore;
using PhloxAPI.Data;
using PhloxAPI.Models;

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
            var newUser = new User { FirstName = firstname, LastName = lastname, Username = username, Email=email, Password = password, Salt = ""};
            _context.Users.Add(newUser);
            _context.SaveChanges();
        }
    }
}

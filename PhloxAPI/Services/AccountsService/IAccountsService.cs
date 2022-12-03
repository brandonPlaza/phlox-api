using PhloxAPI.DTOs;
using PhloxAPI.Models;

namespace PhloxAPI.Services.AccountsService
{
    public interface IAccountsService
    {
        string RegisterUser(UserDTO user);
        string Login(UserLoginDTO userLoginDTO);
    }
}

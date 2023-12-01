using PhloxAPI.Models.DTOs;
using PhloxAPI.Models.Entities;

namespace PhloxAPI.Services.AccountsService
{
    public interface IAccountsService
    {
        string RegisterUser(UserDTO user);
        UserDTO Login(UserLoginDTO userLoginDTO);
        List<Node> GetFavouriteAmenities(string username);
        List<Node> GetAllAmenities();
        string AddFavouriteAmenity(string amenityName, string username);
        string RemoveFavouriteAmenity(string amenityName, string username);

        User? GetUserByEmail(string email);
    }
}

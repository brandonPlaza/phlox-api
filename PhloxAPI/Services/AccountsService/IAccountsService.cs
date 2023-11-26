using PhloxAPI.Models.DTOs;
using PhloxAPI.Models.Entities;

namespace PhloxAPI.Services.AccountsService
{
    public interface IAccountsService
    {
        string RegisterUser(UserDTO user);
        UserDTO Login(UserLoginDTO userLoginDTO);
        List<Node> GetFavAmenities(string username);
        void AddFavAmenity(NodeDTO amenityDTO, string username);

        User? GetUserByEmail(string email);
    }
}

using PhloxAPI.Models.DTOs;
using PhloxAPI.Models.Entities;

namespace PhloxAPI.Services.AccountsService
{
    public interface IAccountsService
    {
        string RegisterUser(UserDTO user);
        string Login(UserLoginDTO userLoginDTO);
        List<Node> GetFavAmenities(string username);
        void AddFavAmenity(NodeDTO amenityDTO, string username);
    }
}

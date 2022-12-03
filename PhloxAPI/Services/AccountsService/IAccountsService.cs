using PhloxAPI.DTOs;
using PhloxAPI.Models;

namespace PhloxAPI.Services.AccountsService
{
    public interface IAccountsService
    {
        string RegisterUser(UserDTO user);
        string Login(UserLoginDTO userLoginDTO);
        List<Amenity> GetFavAmenities(string username);
        void AddFavAmenity(AmenityDTO amenityDTO, string username);
    }
}

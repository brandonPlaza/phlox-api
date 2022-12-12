using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhloxAPI.DTOs;
using PhloxAPI.Models;
using PhloxAPI.Services.AccountsService;
using PhloxAPI.DTOs;

namespace PhloxAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountsService _accountsService;

        public AccountsController(IAccountsService accountsService)
        {
            _accountsService = accountsService;
        }

        /// <summary>
        /// Retrieve a list of the users favourite amenities
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetFavouriteAmenities(string username)
        {
            var favAmenities = _accountsService.GetFavAmenities(username);

            var favAmenitiesStrings = new List<string>();

            foreach(Amenity amenity in favAmenities)
            {
                favAmenitiesStrings.Add(amenity.Name);
            }

            return Ok(favAmenitiesStrings);
        }

        [HttpPost]
        public async Task<IActionResult> AddFavouriteAmenity(AmenityDTO amenity, string username)
        {
            _accountsService.AddFavAmenity(amenity, username);
            return Ok("Amenity Added");
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDTO userLogin)
        {
            string user = _accountsService.Login(userLogin);
            return Ok(user);
        }

        [HttpPost]
        public IActionResult Register(UserDTO user)
        {
            string result = _accountsService.RegisterUser(user);
            return Ok(result);
        }
    }
}

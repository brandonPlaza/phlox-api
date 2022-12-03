using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhloxAPI.DTOs;
using PhloxAPI.Models;
using PhloxAPI.Services.AccountsService;

namespace PhloxAPI.Controllers
{
    [Route("api/[controller]")]
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
        [HttpGet("/getfavamenities")]
        public async Task<IActionResult> GetFavouriteAmenities(string username)
        {
            var favAmenities = _accountsService.GetFavAmenities(username);

            var favAmenitiesStrings = new List<string>();

            foreach(Amenity amenity in favAmenities)
            {
                favAmenitiesStrings.Add(amenity.Name);
            }

            return Ok(favAmenities);
        }

        [HttpPost("/addfavamenities")]
        public async Task<IActionResult> AddFavouriteAmenity(AmenityDTO amenity, string username)
        {
            _accountsService.AddFavAmenity(amenity, username);
            return Ok("Amenity Added");
        }
    }
}

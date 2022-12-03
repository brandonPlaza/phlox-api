using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public async Task<IActionResult> GetFavouriteAmenities(string username)
        {
            var favAmenities = _accountsService.GetFavAmenities(username);
            return Ok(favAmenities);
        }

        [HttpPost]
        public async Task<IActionResult> AddFavouriteAmenity(Amenity amenity, string username)
        {
            _accountsService.AddFavAmenity(amenity, username);
            return Ok("Amenity Added");
        }
    }
}

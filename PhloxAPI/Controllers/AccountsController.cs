using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhloxAPI.Models.DTOs;
using PhloxAPI.Models.Entities;
using PhloxAPI.Services.AccountsService;


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
            var favAmenities = _accountsService.GetFavouriteAmenities(username);
            if (favAmenities != null)
            {
				return Ok(favAmenities);
			} else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddFavouriteAmenity(string amenity, string username)
        {
            var message = _accountsService.AddFavouriteAmenity(amenity, username);
		    return Ok(message);
		}

		[HttpPost]
		public async Task<IActionResult> RemoveFavouriteAmenity(string amenity, string username)
		{
			var message = _accountsService.RemoveFavouriteAmenity(amenity, username);
			return Ok(message);
		}

		[HttpPost]
        public async Task<IActionResult> Login(UserLoginDTO userLogin)
        {
            UserDTO user = _accountsService.Login(userLogin);

            if(user != null)
            {
                return Ok(user);
            } else
            {
				return BadRequest(new { message = "Login failed. Invalid username or password." });
			}
        }

        [HttpPost]
        public IActionResult Register(UserDTO user)
        {
            string result = _accountsService.RegisterUser(user);
            return Ok(result);
        }

        [HttpGet]
        public IActionResult GetUserByEmail(string email)
        {
            var result = _accountsService.GetUserByEmail(email);
            return Ok(result);
        }
    }
}

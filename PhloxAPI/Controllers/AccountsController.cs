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

        [HttpPost("/login")]
        public IActionResult Login(UserLoginDTO userLogin)
        {
            var token = _accountsService.Login(userLogin.Username, userLogin.Password);
            return Ok(token);
        }

        [HttpPost("/register")]
        public IActionResult Register(UserDTO user)
        {
            string result = _accountsService.RegisterUser(user.FirstName, user.LastName, user.Username, user.Email, user.Password);
            return Ok(result);
        }
    }
}

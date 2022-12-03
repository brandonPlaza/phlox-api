using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhloxAPI.Services.AccountsService;
using PhloxAPI.DTOs;

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
        public async Task<IActionResult> Login(UserLoginDTO userLogin)
        {
            string user = _accountsService.Login(userLogin);
            return Ok(user);
        }

        [HttpPost("/register")]
        public IActionResult Register(UserDTO user)
        {
            string result = _accountsService.RegisterUser(user);
            return Ok(result);
        }
    }
}

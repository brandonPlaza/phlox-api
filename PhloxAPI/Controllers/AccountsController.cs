using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    }
}

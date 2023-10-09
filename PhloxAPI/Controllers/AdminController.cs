using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhloxAPI.Models;
using PhloxAPI.Models.Entities;
using PhloxAPI.Services.AdministrationService;

namespace PhloxAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdministrationService _administrationService;

        public AdminController(IAdministrationService administrationService)
        {
            _administrationService = administrationService;
        }

        [HttpPost]
        public async Task<IActionResult> AddNode(char buildingLetter)
        {
            return Ok("Building Added");
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhloxAPI.Models;
using PhloxAPI.Services.AdministrationService;

namespace PhloxAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhloxController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetTest()
        {
            return Ok("Hello World!");
        }
    }
}

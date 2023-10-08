using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhloxAPI.Models;
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
        public async Task<IActionResult> AddAmenity(string name, int type, char building, char connectedBuilding)
        {
            _administrationService.AddAmenity(name, type, building, connectedBuilding);
            return Ok("Amenity Added");
        }

        // [HttpPost]
        // public async Task<IActionResult> AddBuilding(char buildingLetter)
        // {
        //     Building building = new Building { Letter = buildingLetter, ConnectedBuilding = ' ' };
        //     _administrationService.AddBuilding(building);
        //     return Ok("Building Added");
        // }

        [HttpPost]
        public async Task<IActionResult> ConnectBuildings(char buildingOne, char buildingTwo)
        {
            _administrationService.ConnectBuildings(buildingOne, buildingTwo);
            return Ok("Buildings connected");
        }

        [HttpGet]
        public async Task<IActionResult> GetBuildings()
        {
            return Ok(_administrationService.GetBuildings());
        }
        [HttpGet]
        public async Task<IActionResult> GetAmenities()
        {
            return Ok(_administrationService.GetAmenities());
        }
    }
}

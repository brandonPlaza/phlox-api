using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhloxAPI.Models;
using PhloxAPI.Services.AdministrationService;

namespace PhloxAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhloxAdminController : ControllerBase
    {
        private readonly IAdministrationService _administrationService;

        public PhloxAdminController(IAdministrationService administrationService)
        {
            _administrationService = administrationService;
        }

        [HttpPost]
        public async Task<IActionResult> AddAmenity(Amenity newAmenity)
        {
            _administrationService.AddAmenity(newAmenity);
            return Ok("Amenity Added");
        }

        [HttpGet]
        public async Task<IActionResult> GenerateAmenities()
        {
            var amenities = new List<Amenity> 
            { 
                new Amenity{Id = Guid.NewGuid(), Building = 'C', Floor = 1, IsOutOfService = false, Type=AmenityType.Ramp},

            };

            return Ok();
        }
    }
}

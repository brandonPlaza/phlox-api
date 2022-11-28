using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhloxAPI.Models;
using PhloxAPI.Services.RoutingService;

namespace PhloxAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoutingController : ControllerBase
    {
        private readonly IRoutingService _routingService;

        public RoutingController(IRoutingService routingService)
        {
            _routingService = routingService;
        }

        [HttpGet("/requestroute")]
        public async Task<IActionResult> GetRoute(char currBuilding, char destBuilding)
        {
            var route = _routingService.RequestRoute(currBuilding, destBuilding);
            List<string> routeInstructions = new List<string>();
            foreach(Amenity amenity in route)
            {
                routeInstructions.Add(amenity.Name);
            }
            return Ok(routeInstructions);
        }
    }
}

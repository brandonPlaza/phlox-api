using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpGet("/requestroute")]
        public async Task<IActionResult> GetRoute(char currBuilding, char destBuilding)
        {
            var route = _routingService.RequestRoute(currBuilding, destBuilding);

            if (route == null)
                return Ok("You are already in this building");

            List<string> routeInstructions = new List<string>();
            foreach(Amenity amenity in route)
            {
                routeInstructions.Add(amenity.Name);
            }
            return Ok(routeInstructions);
        }
    }
}

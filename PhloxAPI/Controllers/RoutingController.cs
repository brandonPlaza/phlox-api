using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    }
}

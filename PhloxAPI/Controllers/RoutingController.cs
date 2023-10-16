using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhloxAPI.Models;
using PhloxAPI.Services.RoutingService;

namespace PhloxAPI.Controllers
{
  [Route("api/[controller]/[action]")]
  [ApiController]
  public class RoutingController : ControllerBase
  {
    private readonly IRoutingService _routingService;

    public RoutingController(IRoutingService routingService)
    {
      _routingService = routingService;
    }

    [HttpGet]
    public async Task<IActionResult> GetRoute(string source, string dest)
    {
      var results = await _routingService.RequestRoute(source, dest);
      return Ok(results);
    }
    [HttpGet]
    public async Task<IActionResult> GetNodes(){
      return Ok(_routingService.GetNodes());
    }
  }
}

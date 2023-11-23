using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhloxAPI.Models;
using PhloxAPI.Models.DTOs;
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
    public async Task<IActionResult> AddNode(string name, int type)
    {
      _administrationService.AddNode(name, (NodeTypes)type);
      return Ok(name);
    }

    [HttpPost]
    public async Task<IActionResult> AddConnection(string firstNodeId, string secondNodeId, int weight, int cardinality)
    {
      _administrationService.AddConnection(firstNodeId, secondNodeId, weight, cardinality);
      return Ok("Connection Added");
    }

    [HttpPost]
    public async Task<IActionResult> RemoveNode(string nodeName){
      var response = await _administrationService.RemoveNode(nodeName);
      return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> GetNeighbors(){
      var response = _administrationService.GetNeighbors();
      return Ok(response);
    }
  }
}

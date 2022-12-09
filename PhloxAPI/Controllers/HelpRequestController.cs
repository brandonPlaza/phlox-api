using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhloxAPI.DTOs;
using PhloxAPI.Models;
using PhloxAPI.Services.HelpRequestService;
using PhloxAPI.DTOs;

namespace PhloxAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class HelpRequestController : ControllerBase
  {
    private readonly IHelpRequestService _helpRequestService;

    public HelpRequestController(IHelpRequestService helpRequestService)
    {
      _helpRequestService = helpRequestService;
    }

    /// <summary>
    /// Add a new help request.
    /// </summary>
    /// <param name="helpRequestDTO"></param>
    /// <returns></returns>
    [HttpPost("/add")]
    public async Task<IActionResult> AddHelpRequest(HelpRequestDTO helpRequestDTO)
    {
      var newId = _helpRequestService.PostHelpRequest(helpRequestDTO);
      return Ok(newId);
    }

    /// <summary>
    /// Update the status of an existing help request.
    /// </summary>
    /// <param name="statusHelpRequestDTO"></param>
    /// <returns></returns>
    [HttpPost("/updatestatus")]
    public async Task<IActionResult> UpdateHelpRequestStatus(StatusHelpRequestDTO statusHelpRequestDTO)
    {
      Guid guid = new Guid(statusHelpRequestDTO.Id);
      HelpRequestStatus status = (HelpRequestStatus) Enum.Parse(typeof(HelpRequestStatus), statusHelpRequestDTO.Status, true);

      _helpRequestService.UpdateHelpRequestStatus(guid, status);
      return Ok("Status updated.");
    }

    /// <summary>
    /// Get an existing help request by its ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("/getbyid")]
    public async Task<IActionResult> GetHelpRequestById(string id)
    {
      Guid guid = new Guid(id);
      var helpRequest = _helpRequestService.GetHelpRequestById(guid);
      return Ok(helpRequest);
    }

    /// <summary>
    /// Get all help requests in the database.
    /// </summary>
    /// <param></param>
    /// <returns></returns>
    [HttpGet("/getall")]
    public async Task<IActionResult> GetAllHelpRequests()
    {
      var helpRequests = _helpRequestService.GetHelpRequests();
      return Ok(helpRequests);
    }

    /// <summary>
    /// Get all active (AKA not completed or cancelled) help requests
    /// </summary>
    /// <param></param>
    /// <returns></returns>
    [HttpGet("/getactive")]
    public async Task<IActionResult> GetActiveHelpRequests()
    {
      var activeHelpRequests = _helpRequestService.GetActiveHelpRequests();
      return Ok(activeHelpRequests);
    }

  }
}
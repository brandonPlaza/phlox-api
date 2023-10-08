using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhloxAPI.Models.DTOs;
using PhloxAPI.Models.Entities;
using PhloxAPI.Services.HelpRequestService;

namespace PhloxAPI.Controllers
{
  [Route("api/[controller]/[action]")]
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
    [HttpPost]
    public async Task<IActionResult> Add(HelpRequestDTO helpRequestDTO)
    {
      var statusHelpRequestDTO = _helpRequestService.PostHelpRequest(helpRequestDTO);
      return Ok(statusHelpRequestDTO);
    }

    /// <summary>
    /// Update the status of an existing help request.
    /// </summary>
    /// <param name="statusHelpRequestDTO"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> UpdateStatus(StatusHelpRequestDTO statusHelpRequestDTO)
    {
      Guid guid = new Guid(statusHelpRequestDTO.Id);
      HelpRequestStatus status = (HelpRequestStatus)Enum.Parse(typeof(HelpRequestStatus), statusHelpRequestDTO.Status, true);

      _helpRequestService.UpdateHelpRequestStatus(guid, status);
      return Ok("Status updated.");
    }

    /// <summary>
    /// Get an existing help request by its ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetById(string id)
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
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      var helpRequests = _helpRequestService.GetHelpRequests();
      return Ok(helpRequests);
    }

    /// <summary>
    /// Get all active (AKA not completed or cancelled) help requests
    /// </summary>
    /// <param></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetActive()
    {
      var activeHelpRequests = _helpRequestService.GetActiveHelpRequests();
      return Ok(activeHelpRequests);
    }

  }
}
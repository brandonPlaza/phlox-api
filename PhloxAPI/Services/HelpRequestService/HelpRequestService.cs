using PhloxAPI.Models;
using PhloxAPI.DTOs;
using PhloxAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace PhloxAPI.Services.HelpRequestService
{
  public class HelpRequestService : IHelpRequestService
  {

    private readonly PhloxDbContext _context;

    public HelpRequestService(PhloxDbContext context)
    {
      _context = context;
    }

    public StatusHelpRequestDTO PostHelpRequest(HelpRequestDTO helpRequest)
    {
      var newHelpRequest = new HelpRequest
      {
        UserEmail = helpRequest.UserEmail,
        Status = HelpRequestStatus.Waiting.ToString(),
        Latitude = Convert.ToDouble(helpRequest.Latitude),
        Longitute = Convert.ToDouble(helpRequest.Longitude),
        TimeCreated = DateTime.Now
      };

      _context.HelpRequests.Add(newHelpRequest);
      _context.SaveChanges();

      //at this point position is null because the status is waiting, it hasnt been accepted yet so user is not in queue yet
      var statusHelpRequest = new StatusHelpRequestDTO
      {
        Id = newHelpRequest.Id.ToString(),
        Status = newHelpRequest.Status,
      };

      return statusHelpRequest;
    }

    public void UpdateHelpRequestStatus(Guid id, HelpRequestStatus newStatus)
    {
      var helpRequest = GetHelpRequestById(id);

      helpRequest.Status = newStatus.ToString();

      switch (newStatus)
      {
        case HelpRequestStatus.Waiting:
          helpRequest.TimeAccepted = null;
          break;

        case HelpRequestStatus.Accepted:
          helpRequest.TimeAccepted = DateTime.Now;
          break;

        case HelpRequestStatus.Completed:
          helpRequest.TimeCompleted = DateTime.Now;
          break;

        case HelpRequestStatus.Cancelled:
          helpRequest.TimeCancelled = DateTime.Now;
          break;

      }

      _context.SaveChanges();

      UpdateQueuePositions();
    }

    // /* TODO: Following three methods can almost definitely be made more modular to reduce code reuse in the future */
    // public void AcceptHelpRequest(Guid id)
    // {
    //   var helpRequest = GetHelpRequestById(id);
    //   /* TODO: Reject if status is not waiting */
    //   if (helpRequest.Status == HelpRequestStatus.Waiting)
    //   {
    //     helpRequest.Status = HelpRequestStatus.Accepted;
    //     helpRequest.TimeAccepted = DateTime.Now;
    //     _context.SaveChanges();

    //     UpdateQueuePositions();
    //   }
    // }

    // public void CompleteHelpRequest(Guid id)
    // {
    //   var helpRequest = GetHelpRequestById(id);
    //   /* TODO: Reject if status is not waiting or accepted */
    //   if (helpRequest.Status == HelpRequestStatus.Waiting || helpRequest.Status == HelpRequestStatus.Accepted)
    //   {
    //     helpRequest.Status = HelpRequestStatus.Completed;
    //     helpRequest.TimeCompleted = DateTime.Now;
    //     _context.SaveChanges();

    //     UpdateQueuePositions();
    //   }
    // }

    // public void CancelHelpRequest(Guid id)
    // {
    //   var helpRequest = GetHelpRequestById(id);

    //   helpRequest.Status = HelpRequestStatus.Cancelled;
    //   helpRequest.TimeCancelled = DateTime.Now;
    //   _context.SaveChanges();

    //   UpdateQueuePositions();
    // }

    public List<HelpRequest> GetHelpRequests()
    {
      return _context.HelpRequests.ToList();
    }

    public List<HelpRequest> GetActiveHelpRequests()
    {
      var activeRequests = _context.HelpRequests
        .Where(r => r.Status.Equals(HelpRequestStatus.Waiting.ToString()) || r.Status.Equals(HelpRequestStatus.Accepted.ToString()))
        .OrderByDescending(o => o.TimeCreated);
      return activeRequests.ToList();
    }

    public HelpRequest GetHelpRequestById(Guid id)
    {
      var helpRequest = _context.HelpRequests.FirstOrDefault(r => r.Id == id);

      return helpRequest;
    }

    private void UpdateQueuePositions()
    {
      // TODO: Get all accepted requests and update their position in queue based on time accepted
    }

  }
}
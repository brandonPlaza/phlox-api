
using PhloxAPI.Models.DTOs;
using PhloxAPI.Models.Entities;

namespace PhloxAPI.Services.HelpRequestService
{
  public interface IHelpRequestService
  {
    StatusHelpRequestDTO PostHelpRequest(HelpRequestDTO helpRequest);
    void UpdateHelpRequestStatus(Guid id, HelpRequestStatus newstatus);
    HelpRequest GetHelpRequestById(Guid id);
    List<HelpRequest> GetHelpRequests();
    List<HelpRequest> GetActiveHelpRequests();
    void DeleteHelpRequestById(Guid id);
  }
}
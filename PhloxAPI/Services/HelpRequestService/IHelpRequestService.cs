using PhloxAPI.Models;
using PhloxAPI.DTOs;

namespace PhloxAPI.Services.HelpRequestService
{
    public interface IHelpRequestService
    {
      // TODO: POTENTIALLY CHANGE TYPE OF GET'S TO HelpRequestWithTimeDTO?
        Guid PostHelpRequest(HelpRequestDTO helpRequest);
        void UpdateHelpRequestState(Guid id);
        List<HelpRequest> GetHelpRequests();
    }
}
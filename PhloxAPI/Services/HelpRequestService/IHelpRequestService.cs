using PhloxAPI.Models;
using PhloxAPI.DTOs;

namespace PhloxAPI.Services.HelpRequestService
{
    public interface IHelpRequestService
    {
        Guid PostHelpRequest(HelpRequestDTO helpRequest);
        void AcceptHelpRequest(Guid id);
        void CompleteHelpRequest(Guid id);
        void CancelHelpRequest(Guid id);
        HelpRequest GetHelpRequestById(Guid id);
        List<HelpRequest> GetHelpRequests();
        List<HelpRequest> GetActiveHelpRequests();
    }
}
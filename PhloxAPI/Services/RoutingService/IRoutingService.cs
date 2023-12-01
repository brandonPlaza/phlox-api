using PhloxAPI.Models.Entities;
using PhloxAPI.Services.RoutingService.Classes;

namespace PhloxAPI.Services.RoutingService
{
    public interface IRoutingService
    {
        Task<List<string>> RequestRoute(string source, string dest, string disability);
        List<string> GetNodes();
    }
}

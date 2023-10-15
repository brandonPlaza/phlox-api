using PhloxAPI.Models.Entities;
using PhloxAPI.Services.RoutingService.Classes;

namespace PhloxAPI.Services.RoutingService
{
    public interface IRoutingService
    {
        Task<List<int>> RequestRoute(string source, string dest);
    }
}

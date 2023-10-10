using PhloxAPI.Models.Entities;

namespace PhloxAPI.Services.RoutingService
{
    public interface IRoutingService
    {
        void RequestRoute(string source, string dest);
    }
}

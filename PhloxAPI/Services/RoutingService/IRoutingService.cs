using PhloxAPI.Models;

namespace PhloxAPI.Services.RoutingService
{
    public interface IRoutingService
    {
        List<Node> RequestRoute(string source, string dest);
    }
}

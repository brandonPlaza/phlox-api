using PhloxAPI.Models.Entities;
using PhloxAPI.Services.RoutingService.Classes;

namespace PhloxAPI.Services.RoutingService
{
    public interface IRoutingService
    {
        Task<(Dictionary<GraphNode, int>, Dictionary<GraphNode, GraphNode>)> RequestRoute(string source, string dest);
    }
}

using PhloxAPI.Models;

namespace PhloxAPI.Services.RoutingService
{
    public interface IRoutingService
    {
        List<Amenity> RequestRoute(string currentBuilding, string destinationBuilding);
    }
}

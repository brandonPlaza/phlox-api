using PhloxAPI.Models;

namespace PhloxAPI.Services.RoutingService
{
    public interface IRoutingService
    {
        List<Amenity> RequestRoute(char currentBuilding, char destinationBuilding);
    }
}

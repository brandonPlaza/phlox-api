using PhloxAPI.Data;
using PhloxAPI.Models;

namespace PhloxAPI.Services.RoutingService
{
    public class RoutingService : IRoutingService
    {
        private readonly PhloxDbContext _context;

        public RoutingService(PhloxDbContext context)
        {
            _context = context;
        }

        public List<Amenity> RequestRoute(string currentBuilding, string destinationBuilding)
        {
            throw new NotImplementedException();
        }
    }
}


using PhloxAPI.Models.Entities;

namespace PhloxAPI.Services.RoutingService
{
  public class RoutingService : IRoutingService
  {
    //private readonly PhloxDbContext _context;

    public RoutingService()
    { 
      //_context = context;
    }

    public List<Node> RequestRoute(string source, string dest)
    {
      throw new NotImplementedException();
    }
  }
}

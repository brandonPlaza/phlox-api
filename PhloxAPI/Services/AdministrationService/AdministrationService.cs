using Microsoft.EntityFrameworkCore;
using PhloxAPI.Data;
using PhloxAPI.Models;
using PhloxAPI.Models.Entities;

namespace PhloxAPI.Services.AdministrationService
{
  public class AdministrationService : IAdministrationService
  {
    private readonly PhloxDbContext _context;

    public AdministrationService(PhloxDbContext context) {
      _context = context;
    }

    public void AddNode(string name, NodeTypes type)
    {
      var newNode = new Node
      {
        Name = name,
        Type = type,
        Reports = new List<Report>(),
        CardinalConnections = new List<Node>()
      };

      _context.Nodes.Add(newNode);
      _context.SaveChanges();
    }

    public Node UpdateAmenity()
    {
      throw new NotImplementedException();
    }
  }
}

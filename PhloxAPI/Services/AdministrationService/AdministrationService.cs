using Microsoft.EntityFrameworkCore;
using PhloxAPI.Data;
using PhloxAPI.Models;
using PhloxAPI.Models.DTOs;
using PhloxAPI.Models.Entities;

namespace PhloxAPI.Services.AdministrationService
{
  public class AdministrationService : IAdministrationService
  {
    private readonly PhloxDbContext _context;

    public AdministrationService(PhloxDbContext context) {
      _context = context;
    }

    public async Task AddEdge(string nodeOne, string nodeTwo, int weight, CardinalDirection direction)
    {
      // Gets node corresponding to the name in nodeOne and stores it in nOne
      // Specifically includes the cyclical components like its list of nodes
      var nOne = await _context.Nodes.Include(x => x.CardinalConnections).Select(x => new NodeDTO()
      {
        Id = x.Id,
        NodeType = x.Type,
        Name = x.Name,
        CardinalConnections = x.CardinalConnections,
        Reports = x.Reports
      }).SingleOrDefaultAsync(x => x.Name == nodeOne);

      // This does the same thing as above but for the other node in the edge
      var nTwo = await _context.Nodes.Include(x => x.CardinalConnections).Select(x => new NodeDTO()
      {
        Id = x.Id,
        NodeType = x.Type,
        Name = x.Name,
        CardinalConnections = x.CardinalConnections,
        Reports = x.Reports
      }).SingleOrDefaultAsync(x => x.Name == nodeOne);

      if(nOne != null && nTwo != null){
        var nodeOneUpdated = new Node()
        {
          Id = nOne.Id,
          Type = nOne.NodeType,
          Name = nOne.Name,
          CardinalConnections = nOne.CardinalConnections,
          Reports = nOne.Reports
        };
        
        var nodeTwoUpdated = new Node()
        {
          Id = nTwo.Id,
          Type = nTwo.NodeType,
          Name = nTwo.Name,
          CardinalConnections = nTwo.CardinalConnections,
          Reports = nTwo.Reports
        };

        flipCardinals(ref nodeOneUpdated, ref nodeTwoUpdated, direction);
        var weightedEdge = new WeightedEdge()
        {
          Nodes = new List<Node>(){ nodeOneUpdated, nodeTwoUpdated },
          Weight = weight
        };
        _context.Update(nodeOneUpdated);
        _context.Update(nodeTwoUpdated);
        _context.Add(weightedEdge);
        await _context.SaveChangesAsync();
      }
    }

    /// <summary>
    /// Takes two nodes with a given cardinality from the first node to the second,
    /// then flips the cardinality to get cardinality from second to first since the graph
    /// is not directed
    /// </summary>
    /// <param name="nodeOneUpdated"></param>
    /// <param name="nodeTwoUpdated"></param>
    /// <param name="direction"></param>
    private void flipCardinals(ref Node nodeOneUpdated, ref Node nodeTwoUpdated, CardinalDirection direction){
      // Assumption: Cardinal direction provided is from nodeOne to nodeTwo
      nodeOneUpdated.CardinalConnections[(int)direction] = nodeTwoUpdated;

      // This section flips cardinal directions for nodeTwo -> nodeOne
      if((int)direction <= 8){
        // Flips cardinal direction
        int flippedDirection = (((int)direction + 5)%8)-1;
        nodeTwoUpdated.CardinalConnections[flippedDirection] = nodeOneUpdated;
      }
      else{
        // This would be so much nicer w the ternary op but C# wont let me :(
        if(direction == CardinalDirection.Up){
          nodeTwoUpdated.CardinalConnections[(int)CardinalDirection.Down] = nodeOneUpdated;
        }
        else{
          nodeTwoUpdated.CardinalConnections[(int)CardinalDirection.Up] = nodeOneUpdated;
        }
      }
    }

    public void AddNode(string name, NodeTypes type)
    {
      var existingNode = _context.Nodes.SingleOrDefault(x => x.Name == name);
      if(existingNode != null){
        return;
      }
      
      var newNode = new Node
      {
        Name = name,
        Type = type,
        Reports = new List<Report>(),
        CardinalConnections = new List<Node>(10)
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

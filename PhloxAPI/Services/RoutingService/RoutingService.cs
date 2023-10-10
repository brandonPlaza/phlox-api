
using Microsoft.EntityFrameworkCore;
using PhloxAPI.Data;
using PhloxAPI.Models.DTOs;
using PhloxAPI.Models.Entities;

namespace PhloxAPI.Services.RoutingService
{
  public class RoutingService : IRoutingService
  {
    private readonly PhloxDbContext _context;

    public RoutingService(PhloxDbContext context)
    { 
      _context = context;
    }

    public async void RequestRoute(string source, string dest)
    {
      var graph = await _context.WeightedEdges.Include(x => x.Nodes).Select(x => new WeightedEdgeDTO(){
        NodeOne = x.Nodes[0],
        NodeTwo = x.Nodes[1],
        NodeOneToTwoCardinal = x.FirstNodeToSecondCardinal,
        NodeTwoToOneCardinal = x.SecondNodeToFirstCardinal,
        Weight = x.Weight
      }).ToListAsync();

      var nodes = await _context.Nodes.Select(x => new NodeRoutingDTO(){
        Name = x.Name,
        NodeType = x.Type,
        IsOutOfService = x.IsOutOfService,
        Visited = false,
      }).ToListAsync();
    }

    private void Dijkstras(List<WeightedEdgeDTO> graph, List<NodeRoutingDTO> nodes, Node source){
      // Start with graph and source node

      // List to keep track of visited nodes; I.e starting list is {}
      List<Node> visited = new();
      // List to keep track of non visited nodes; I.e starting list is {<every node>}
      

      // At the beginning all distances are set to infinity
      // Start with distance from source to source; I.e 0
      // Start for loop
        //Visit the unvisited node with the smallest distance from start (at the beginning it will be the start node)
        //For the current node we look at its unvisited neighbors
        //Calculate the distance of each neighbor from the start node. For example from start node to node with a weight of 1 it will be 0 + 1 = 1
        //If the calculated distance of a node is less than the known distance, update it
        //Store current node under previous node for the adjacent nodes
        //Add node to visited nodes

    }
  }
}

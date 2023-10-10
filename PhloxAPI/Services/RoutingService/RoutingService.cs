
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
        NodeOne = new NodeRoutingDTO(){ 
          Name = x.Nodes[0].Name,
          NodeType = x.Nodes[0].Type,
          IsOutOfService = x.Nodes[0].IsOutOfService,
          Visited = false,
        },
        NodeTwo = new NodeRoutingDTO(){
          Name = x.Nodes[1].Name,
          NodeType = x.Nodes[1].Type,
          IsOutOfService = x.Nodes[1].IsOutOfService,
          Visited = false,
        },
        NodeOneToTwoCardinal = x.FirstNodeToSecondCardinal,
        NodeTwoToOneCardinal = x.SecondNodeToFirstCardinal,
        Weight = x.Weight
      }).ToListAsync();

    }

    private void Dijkstras(List<WeightedEdgeDTO> graph, List<NodeRoutingDTO> nodes, NodeRoutingDTO source){
      // Start with graph and source node

      //Maps nodes to shortest length from source
      Dictionary<NodeRoutingDTO, int> totalCosts = new();
      //Records prev nodes
      Dictionary<NodeRoutingDTO, NodeRoutingDTO> prevNodes = new();
      List<NodeRoutingDTO> visited = new();
      
      // Set source node distance to 0
      totalCosts.Add(source, 0);
      // At the beginning all distances are set to infinity
      foreach(WeightedEdgeDTO edge in graph){
        if(edge.NodeOne.Name != source.Name){
          node.DistanceFromStart = int.MaxValue;
        }
      }
      // Start with distance from source to source; I.e 0
      // Start loop
      while(true){
        //Visit the unvisited node with the smallest distance from start (at the beginning it will be the start node)
        foreach(NodeRoutingDTO node in nodes){

        }
        //For the current node we look at its unvisited neighbors
        List<WeightedEdgeDTO> neighbors;
        //Calculate the distance of each neighbor from the start node. For example from start node to node with a weight of 1 it will be 0 + 1 = 1
        //If the calculated distance of a node is less than the known distance, update it
        //Store current node under previous node for the adjacent nodes
        //Add node to visited nodes
      }
    }
  }
}

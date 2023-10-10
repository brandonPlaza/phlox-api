
using Microsoft.AspNetCore.Routing.Internal;
using Microsoft.EntityFrameworkCore;
using PhloxAPI.Data;
using PhloxAPI.Models.DTOs;
using PhloxAPI.Models.Entities;
using PhloxAPI.Services.RoutingService.Classes;

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
      var weightedEdgeDTOGraph = await _context.WeightedEdges.Include(x => x.Nodes).Select(x => new WeightedEdgeDTO(){
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

      Graph graph = new Graph();
      graph.LoadGraph(weightedEdgeDTOGraph);

      Dijkstras(graph, graph.Nodes.Find(x => x.Name == source));
    }

    private void Dijkstras(Graph graph, GraphNode source){
      // Start with graph and source node
      // Maps nodes to shortest length from source
      Dictionary<GraphNode, int> totalCosts = new();

      // Records prev nodes
      Dictionary<GraphNode, GraphNode> prevNodes = new();

      // Holds visited nodes
      List<GraphNode> visited = new();

      // Priority Queue to set what nodes to visit
      PriorityQueue<GraphNode, int> priorityQueue = new();

      // Set source node distance to 0
      totalCosts.Add(source, 0);

      // Queue up the first node
      priorityQueue.Enqueue(source, 0);

      // At the beginning all distances are set to infinity
      foreach(GraphNode node in graph.Nodes){
        if(!node.Equals(source)){
          totalCosts.Add(node, int.MaxValue);
        }
      }

      // Start with distance from source to source; I.e 0
      // Start loop
      while(priorityQueue.Count != 0){
        //Visit the unvisited node with the smallest distance from start (at the beginning it will be the start node)
        GraphNode closestNode = priorityQueue.Dequeue();
        //For the current node we look at its unvisited neighbors
        foreach(GraphNode neighbor in closestNode.Neighbors.Keys){
          if(!visited.Contains(neighbor)){
            int closestNodeDistance;
            int closestNodeToNeighborDistance;
            totalCosts.TryGetValue(closestNode, out closestNodeDistance);
            closestNode.Neighbors.TryGetValue(neighbor, out closestNodeToNeighborDistance);
            int path = closestNodeDistance + closestNodeToNeighborDistance;
            
            int neighborDistance;
            totalCosts.TryGetValue(neighbor, out neighborDistance);
            if(path < neighborDistance){
              totalCosts[neighbor] = path;
              if(prevNodes.Keys.Contains(neighbor)){
                prevNodes[neighbor] = closestNode;
              }
              else{
                prevNodes.Add(neighbor, closestNode);
              }
              
            }
          }
        }
      }

    }
  }
}

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

    public async Task<(Dictionary<GraphNode, int>, Dictionary<GraphNode, GraphNode>)> RequestRoute(string source, string dest)
    {
      var nodes = await _context.Nodes.Include(x => x.Neighbors).Include(x => x.Cardinality).ToListAsync();

      Graph graph = new Graph();
      graph.LoadGraph(nodes);

      var results = Dijkstras(graph, graph.Nodes.Find(x => x.Name == source));
      return results;
    }

    public List<GraphNode> ConvertNodeToGraphNode(List<Node> nodes){
      List<GraphNode> convertedNodes = new();
      foreach(Node node in nodes){
        if(node.Neighbors == null){
          node.Neighbors = new Dictionary<Node, int>();
          node.Cardinality = new Dictionary<Node, int>();
          continue;
        }
        List<Node> neighborKeys = node.Neighbors.Keys.ToList();
        List<Node> cardinalityKeys = node.Cardinality.Keys.ToList();
        for(int i = 0 ; i < neighborKeys.Count ; i++){
          int neighborWeight = node.Neighbors[neighborKeys[i]];
          int cardinalityValue = node.Cardinality[cardinalityKeys[i]];
        }
      }
    }
    public Dictionary<GraphNode, int> ParseNodeToGraphNodeDict(Dictionary<Node, int> keyValues){
      Dictionary<GraphNode, int> parsedGraph = new();
      List<Node> nodes = keyValues.Keys.ToList();
      List<GraphNode> graphNodes = new();
      List<int> values = new();
      foreach(Node node in nodes){
        values.Add(keyValues[node]);
      }
    }

    private (Dictionary<GraphNode, int>, Dictionary<GraphNode, GraphNode>) Dijkstras(Graph graph, GraphNode source){
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
              priorityQueue = UpdatePriorityQueue(priorityQueue, neighbor, path);
            }
          }
        }
      }
      return(totalCosts, prevNodes);
    }

    // This has to exist thanks to microsoft not having a separate implementation of PQ with updatable priorities
    private PriorityQueue<GraphNode, int> UpdatePriorityQueue(PriorityQueue<GraphNode, int> priorityQueue, GraphNode updatedGraphNode, int updatedPriority){
      List<GraphNode> graphNodes = new();
      List<int> priorities = new();

      for(int i = 0; i<priorityQueue.Count; i++){
        GraphNode tempGraphNode;
        int tempPriority;

        priorityQueue.TryPeek(out tempGraphNode, out tempPriority);
        priorityQueue.Dequeue();

        if(tempGraphNode.Equals(updatedGraphNode)){
          priorityQueue.Enqueue(updatedGraphNode, updatedPriority);
        }
        else{
          graphNodes.Add(tempGraphNode);
          priorities.Add(tempPriority);
        }
      }
      PriorityQueue<GraphNode, int> newPriorityQueue = new();
      for(int i = 0; i < graphNodes.Count; i++){
        newPriorityQueue.Enqueue(graphNodes[i], priorities[i]);
      }

      return newPriorityQueue;
    }
  }
}

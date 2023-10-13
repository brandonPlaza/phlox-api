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
      var nodes = await _context.Nodes.Include(x => x.Neighbors).Include(x => x.Cardinalities).ToListAsync();
      var graphNodes = ConvertNodeToGraphNode(nodes);
      Graph graph = new Graph();
      graph.LoadGraph(graphNodes);

      var results = Dijkstras(graph, graph.Nodes.Find(x => x.Name == source));
      return results;
    }

    // I will optimize this later
    public List<GraphNode> ConvertNodeToGraphNode(List<Node> nodes){
      // Goal, covert nodes -> convertedNodes
      List<GraphNode> convertedNodes = new();
      foreach(Node node in nodes){
        // Node w no neighbors gets new dicts
        if(node.Neighbors == null){
          node.Neighbors = new List<Neighbor>();
          continue;
        }
        // Get all neighbors to current node
        List<Node> neighbors = new();
        List<int> neighborsWeights = new();
        List<Node> cardinalityNodes = new();
        List<int> cardinalities = new();

        foreach(Neighbor neighborNode in node.Neighbors){
          neighbors.Append(neighborNode.Node);
          neighborsWeights.Append(neighborNode.Weight);
        }
        foreach(Cardinality cardinality in node.Cardinalities){
          cardinalityNodes.Append(cardinality.Neighbor);
          cardinalities.Append((int)cardinality.CardinalDirection);
        }
        GraphNode newNode = new(node.Name);
        // Find them in nodes so that they carry their data and dont lose it due to cyclical issues
        for(int i = 0 ; i < neighbors.Count ; i++){
          // Get the values
          int neighborWeight = neighborsWeights[i];
          int cardinalityValue = cardinalities[i];

          newNode.AddNeighbor(new GraphNode(neighbors[i].Name), neighborWeight, cardinalityValue);
        }
        convertedNodes.Add(newNode);
      }
      return convertedNodes;
    }

    // private (List<Node>, List<int>) ExtractValues(Node nodeData, int count){
    //   List<Node> node = new();
    //   List<int> value = new();
    //   for(int i = 0 ; i < count ; i++){
        
    //   }
    //   return (node, value);
    // }

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
          break;
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

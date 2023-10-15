using Microsoft.EntityFrameworkCore;
using PhloxAPI.Data;
using PhloxAPI.Models.DTOs;
using PhloxAPI.Models.Entities;
using PhloxAPI.Services.RoutingService.Classes;
using Priority_Queue;

namespace PhloxAPI.Services.RoutingService
{
  public class RoutingService : IRoutingService
  {
    private readonly PhloxDbContext _context;

    public RoutingService(PhloxDbContext context)
    { 
      _context = context;
    }

    public async Task<List<int>> RequestRoute(string source, string dest)
    {
      // var nodes = await _context.Nodes.Include(x => x.Neighbors).Include(x => x.Cardinalities).ToListAsync();
      // var unlinkedGraphNodes = ConvertNodesToUnlinkedGraphNodes(nodes);
      // var linkedGraphNodes = LinkGraphNodes(nodes, unlinkedGraphNodes);

      var dbLessGraphNodes = DbLessGraphNodes();
      Graph graph = new Graph();
      graph.LoadGraph(dbLessGraphNodes);

      var results = Dijkstra(graph, dbLessGraphNodes.Find(x => x.Name == source), dbLessGraphNodes.Find(x => x.Name == dest));
      return results;
    }

    public List<GraphNode> DbLessGraphNodes(){
      List<GraphNode> graphNodes = new List<GraphNode>(){
        new GraphNode("SCAET Entrance"),
        new GraphNode("Front of Stair B"),
        new GraphNode("2nd Floor Stair B"),
        new GraphNode("Front of open glass stairs"),
        new GraphNode("Bottom of open glass stairs"),
        new GraphNode("SCAET seating area"),
        new GraphNode("Bruins Coffee Shop"),
        new GraphNode("Past coffee shop hallway"),
        new GraphNode("Open stairs down to SCAET Floor 1"),
        new GraphNode("Front of stair C"),
        new GraphNode("2nd floor stair C"),
        new GraphNode("C bridge entrance"),
        new GraphNode("Front of elevator"),
        new GraphNode("Front of stair A"),
        new GraphNode("Front of open stairs"),
        new GraphNode("Top of open stairs"),
        new GraphNode("Entrance hall door junction"),
        new GraphNode("Entrance to corridor near open glass stairs"),
        new GraphNode("S205"),
        new GraphNode("S206"),
        new GraphNode("S203"),
        new GraphNode("S207"),
        new GraphNode("S207A"),
        new GraphNode("S202"),
      };
      ConnectNodes(ref graphNodes);
      return graphNodes;
    }

    public void ConnectNodes(ref List<GraphNode> graphNodes){
      graphNodes[0].AddNeighbor(graphNodes[1], 2, (int)CardinalDirection.SouthEast);
      graphNodes[1].AddNeighbor(graphNodes[2], 2, (int)CardinalDirection.SouthWest);
      graphNodes[1].AddNeighbor(graphNodes[3], 3, (int)CardinalDirection.SouthEast);
      graphNodes[3].AddNeighbor(graphNodes[5], 4, (int)CardinalDirection.SouthWest);
      graphNodes[5].AddNeighbor(graphNodes[6], 5, (int)CardinalDirection.SouthEast);
      graphNodes[6].AddNeighbor(graphNodes[7], 3, (int)CardinalDirection.SouthWest);
      graphNodes[7].AddNeighbor(graphNodes[11], 3, (int)CardinalDirection.NorthWest);
      graphNodes[7].AddNeighbor(graphNodes[8], 3, (int)CardinalDirection.SouthEast);
      graphNodes[7].AddNeighbor(graphNodes[9], 3, (int)CardinalDirection.SouthEast);
      graphNodes[11].AddNeighbor(graphNodes[13], 1, (int)CardinalDirection.NorthWest);
      graphNodes[13].AddNeighbor(graphNodes[12], 1, (int)CardinalDirection.NorthWest);
      graphNodes[12].AddNeighbor(graphNodes[14], 1, (int)CardinalDirection.North);
      graphNodes[14].AddNeighbor(graphNodes[15], 1, (int)CardinalDirection.SouthEast);
      graphNodes[3].AddNeighbor(graphNodes[17], 1, (int)CardinalDirection.South);
      graphNodes[18].AddNeighbor(graphNodes[1], 1, (int)CardinalDirection.SouthEast);
      graphNodes[0].AddNeighbor(graphNodes[1], 1, (int)CardinalDirection.SouthEast);
      graphNodes[0].AddNeighbor(graphNodes[1], 1, (int)CardinalDirection.SouthEast);
      graphNodes[0].AddNeighbor(graphNodes[1], 1, (int)CardinalDirection.SouthEast);
      graphNodes[0].AddNeighbor(graphNodes[1], 1, (int)CardinalDirection.SouthEast);
      graphNodes[0].AddNeighbor(graphNodes[1], 1, (int)CardinalDirection.SouthEast);
      graphNodes[0].AddNeighbor(graphNodes[1], 1, (int)CardinalDirection.SouthEast);
      graphNodes[0].AddNeighbor(graphNodes[1], 1, (int)CardinalDirection.SouthEast);
      graphNodes[0].AddNeighbor(graphNodes[1], 1, (int)CardinalDirection.SouthEast);
      graphNodes[0].AddNeighbor(graphNodes[1], 1, (int)CardinalDirection.SouthEast);
      graphNodes[0].AddNeighbor(graphNodes[1], 1, (int)CardinalDirection.SouthEast);
      graphNodes[0].AddNeighbor(graphNodes[1], 1, (int)CardinalDirection.SouthEast);
      graphNodes[0].AddNeighbor(graphNodes[1], 1, (int)CardinalDirection.SouthEast);
      graphNodes[0].AddNeighbor(graphNodes[1], 1, (int)CardinalDirection.SouthEast);
    }

    // I will optimize this later
    // public List<GraphNode> ConvertNodeToGraphNode(List<Node> nodes){
    //   // Goal, covert nodes -> convertedNodes
    //   List<GraphNode> convertedNodes = new();
    //   foreach(Node node in nodes){
    //     // Node w no neighbors gets new dicts
    //     if(node.Neighbors == null){
    //       node.Neighbors = new List<Neighbor>();
    //       continue;
    //     }
    //     // Get all neighbors to current node
    //     List<Node> neighbors = new();
    //     List<int> neighborsWeights = new();
    //     List<Node> cardinalityNodes = new();
    //     List<int> cardinalities = new();

    //     foreach(Neighbor neighborNode in node.Neighbors){
    //       neighbors.Append(neighborNode.Node);
    //       neighborsWeights.Append(neighborNode.Weight);
    //     }
    //     foreach(Cardinality cardinality in node.Cardinalities){
    //       cardinalityNodes.Append(cardinality.Neighbor);
    //       cardinalities.Append((int)cardinality.CardinalDirection);
    //     }
    //     GraphNode newNode = new(node.Name);
    //     // Find them in nodes so that they carry their data and dont lose it due to cyclical issues
    //     for(int i = 0 ; i < neighbors.Count ; i++){
    //       // Get the values
    //       int neighborWeight = neighborsWeights[i];
    //       int cardinalityValue = cardinalities[i];

    //       newNode.AddNeighbor(new GraphNode(neighbors[i].Name), neighborWeight, cardinalityValue);
    //     }
    //     convertedNodes.Add(newNode);
    //   }
    //   return convertedNodes;
    // }

    public List<GraphNode> ConvertNodesToUnlinkedGraphNodes(List<Node> nodes){
      List<GraphNode> unlinkedGraphNodes = new();
      foreach(Node node in nodes){
        unlinkedGraphNodes.Add(new GraphNode(node.Name));
      }
      return unlinkedGraphNodes;
    }

    // public List<GraphNode> LinkGraphNodes(List<Node> nodes, List<GraphNode> graphNodes){
    //   List<GraphNode> linkedGraphNodes = graphNodes;
    //   for(int i = 0; i < nodes.Count; i++){
    //     if(nodes[i].Neighbors != null){
    //       foreach(Neighbor neighbor in nodes[i].Neighbors){
    //         var tempGraphNode = linkedGraphNodes.Find(x => x.Name == neighbor.Node.Name);
    //         linkedGraphNodes[i].Neighbors.Add(tempGraphNode, neighbor.Weight);
    //       }
    //     }
    //     else if(nodes[i].Cardinalities != null){
    //       foreach(Cardinality cardinality in nodes[i].Cardinalities){
    //         var tempGraphNode = linkedGraphNodes.Find(x => x.Name == cardinality.Neighbor.Name);
    //         linkedGraphNodes[i].Cardinality.Add(tempGraphNode, (int)cardinality.CardinalDirection);
    //       }
    //     }
    //   }
    //   return linkedGraphNodes;
    // }

    // private (Dictionary<GraphNode, int>, Dictionary<GraphNode, GraphNode>) Dijkstras(Graph graph, GraphNode source){
    //   // Start with graph and source node
    //   // Maps nodes to shortest length from source
    //   Dictionary<GraphNode, int> totalCosts = new();

    //   // Records prev nodes
    //   Dictionary<GraphNode, GraphNode> prevNodes = new();

    //   // Holds visited nodes
    //   List<GraphNode> visited = new();

    //   // Priority Queue to set what nodes to visit
    //   PriorityQueue<GraphNode, int> priorityQueue = new();

    //   // Set source node distance to 0
    //   totalCosts.Add(source, 0);

    //   // Queue up the first node
    //   priorityQueue.Enqueue(source, 0);

    //   // At the beginning all distances are set to infinity
    //   foreach(GraphNode node in graph.Nodes){
    //     if(!node.Equals(source)){
    //       totalCosts.Add(node, int.MaxValue);
    //       break;
    //     }
    //   }

    //   // Start with distance from source to source; I.e 0
    //   // Start loop
    //   while(priorityQueue.Count != 0){
    //     //Visit the unvisited node with the smallest distance from start (at the beginning it will be the start node)
    //     GraphNode closestNode = priorityQueue.Dequeue();
    //     //For the current node we look at its unvisited neighbors
    //     foreach(GraphNode neighbor in closestNode.Neighbors.Keys){
    //       if(!visited.Contains(neighbor)){
    //         int closestNodeDistance;
    //         int closestNodeToNeighborDistance;
    //         totalCosts.TryGetValue(closestNode, out closestNodeDistance);
    //         closestNode.Neighbors.TryGetValue(neighbor, out closestNodeToNeighborDistance);
    //         int path = closestNodeDistance + closestNodeToNeighborDistance;
            
    //         int neighborDistance;
    //         totalCosts.TryGetValue(neighbor, out neighborDistance);
    //         if(path < neighborDistance){
    //           totalCosts[neighbor] = path;
    //           if(prevNodes.Keys.Contains(neighbor)){
    //             prevNodes[neighbor] = closestNode;
    //           }
    //           else{
    //             prevNodes.Add(neighbor, closestNode);
    //           }
    //           priorityQueue = UpdatePriorityQueue(priorityQueue, neighbor, path);
    //         }
    //       }
    //     }
    //   }
    //   return(totalCosts, prevNodes);
    // }

    private List<int> Dijkstra(Graph graph,GraphNode source, GraphNode dest){
      int[] prev = new int[graph.Nodes.Count];
      for(int i = 0; i < prev.Count(); i++){
        prev[i] = -1;
      }

      int[] distances = new int[graph.Nodes.Count];
      for(int i = 0; i < distances.Count(); i++){
        distances[i] = int.MaxValue;
      }

      for(int i = 0; i < graph.Nodes.Count; i++){
        if(graph.Nodes[i] == source){
          distances[i] = 0;
          break;
        }
      }

      SimplePriorityQueue<GraphNode> nodes = new SimplePriorityQueue<GraphNode>();
      for(int i = 0; i < graph.Nodes.Count; i++){
        nodes.Enqueue(graph.Nodes[i], distances[i]);
      }

      while(nodes.Count != 0){
        GraphNode node = nodes.Dequeue();
        for(int i = 0; i < node.Neighbors.Count; i++){
          GraphNode neighbor = node.Neighbors[i];
          int weight = i < node.NeighborWeights.Count ? node.NeighborWeights[i] : 0;
          int totWeight = distances[node.Index] + weight;
          if(distances[neighbor.Index] > totWeight){
            distances[neighbor.Index] = totWeight;
            prev[neighbor.Index] = node.Index;
            nodes.UpdatePriority(neighbor, distances[neighbor.Index]);
          }
        }
      }

      List<int> indexes = new();
      int destIndex = dest.Index;
      while(destIndex >= 0){
        indexes.Add(destIndex);
        destIndex = prev[destIndex];
      }
      indexes.Reverse();
      return indexes;
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;
using PhloxAPI.Models.Entities;

namespace PhloxAPI.Services.RoutingService.Classes
{
  public class GraphNode
  {
    // Holds neighbors and their weights from the node

    // Holds cardinality of this node -> neighbor nodes where key is the node and cardinality is an int corresponding to
    private Dictionary<GraphNode, int> _cardinality;
    public List<GraphNode> Neighbors { get; set; }
    public List<int> NeighborWeights { get; set; }
    public Dictionary<GraphNode, int> Cardinality { get{ return _cardinality; }}
    public string Name { get; set; }
    public NodeTypes Type { get; set; }
    public int Index { get; set; }

    public GraphNode(string name){
      Neighbors = new();
      NeighborWeights = new();
      _cardinality = new();
      Name = name;
    }
    
    public void AddNeighbor(GraphNode newNeighbor, int weight, int cardinality){
      Neighbors.Add(newNeighbor);
      NeighborWeights.Add(weight);
      _cardinality.Add(newNeighbor, cardinality);
      if (!newNeighbor.Neighbors.Contains(this))
      {
        var flippedCardinal = FlipCardinals((CardinalDirection)cardinality);
        newNeighbor.AddNeighbor(this, weight, flippedCardinal);
      }
    }

    private static int FlipCardinals(CardinalDirection direction){
      if((int)direction < 8){
        // Flips cardinal direction
        return ((int)direction + 4)%8;
      }
      else{
        // This would be so much nicer w the ternary op but C# wont let me :(
        if(direction == CardinalDirection.Up){
          return (int)CardinalDirection.Down;
        }
        else{
          return (int)CardinalDirection.Up;
        }
      }
    }
/*    public override bool Equals(object? obj)
    {
      return this.Equals(obj as GraphNode);
    }

    private bool Equals(GraphNode node){
      return this.Name == node.Name;
    }*/

  }
}
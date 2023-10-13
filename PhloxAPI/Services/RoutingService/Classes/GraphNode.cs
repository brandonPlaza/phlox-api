using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhloxAPI.Services.RoutingService.Classes
{
  public class GraphNode
  {
    // Holds neighbors and their weights from the node
    private Dictionary<GraphNode, int> _neighbors;
    // Holds cardinality of this node -> neighbor nodes where key is the node and cardinality is an int corresponding to
    private Dictionary<GraphNode, int> _cardinality;
    private string _name;
    public Dictionary<GraphNode, int> Neighbors { get{ return _neighbors; }}
    public Dictionary<GraphNode, int> Cardinality { get{ return _cardinality; }}
    public string Name { get {return _name; } }

    public GraphNode(string name){
      _neighbors = new();
      _cardinality = new();
      _name = name;
    }
    
    public void AddNeighbor(GraphNode newNeighbor, int weight, int cardinality){
      _neighbors.Add(newNeighbor, weight);
      _cardinality.Add(newNeighbor, cardinality);
    }

    public override bool Equals(object? obj)
    {
      return this.Equals(obj as GraphNode);
    }

    private bool Equals(GraphNode node){
      return this.Name == node.Name;
    }
  }
}
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
    private string _name;
    public List<GraphNode> Neighbors { get; set; }
    public List<int> NeighborWeights { get; set; }
    public Dictionary<GraphNode, int> Cardinality { get{ return _cardinality; }}
    public string Name { get {return _name; } }
    public NodeTypes Type { get; set; }
    public int Index { get; set; }

    public GraphNode(string name){
      Neighbors = new();
      NeighborWeights = new();
      _cardinality = new();
      _name = name;
    }
    
    public void AddNeighbor(GraphNode newNeighbor, int weight, int cardinality){
      Neighbors.Add(newNeighbor);
      NeighborWeights.Add(weight);
      _cardinality.Add(newNeighbor, cardinality);
      if (!newNeighbor.Neighbors.Contains(this))
      {
        newNeighbor.AddNeighbor(this, weight, cardinality);
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
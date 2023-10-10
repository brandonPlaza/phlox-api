using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PhloxAPI.Models.DTOs;

namespace PhloxAPI.Services.RoutingService.Classes
{
  public class Graph
  {
    private List<GraphNode> _nodes;
    public List<GraphNode> Nodes { get{ return _nodes; }}

    public Graph(){
      _nodes = new();
    }

    public void LoadGraph(List<WeightedEdgeDTO> edges){
      foreach(WeightedEdgeDTO edge in edges){
        // Check dupes and give both an existing instance of the node or a new one if it is new
        GraphNode graphNodeOne = PopulateGraphNode(edge.NodeOne);
        GraphNode graphNodeTwo = PopulateGraphNode(edge.NodeTwo);
        // Take new or existing nodes and add them as neighbors
        graphNodeOne.AddNeighbor(graphNodeTwo, edge.Weight, edge.NodeOneToTwoCardinal);
        graphNodeTwo.AddNeighbor(graphNodeOne, edge.Weight, edge.NodeTwoToOneCardinal);
        // Add newly created nodes to _nodes
        _nodes.Add(graphNodeOne);
        _nodes.Add(graphNodeTwo);
      }
    }

    private GraphNode PopulateGraphNode(NodeRoutingDTO nodeDTO){
      if(!IsDuplicateNode(nodeDTO.Name)){
        return new GraphNode(nodeDTO.Name);
      }
      else{
        return _nodes.Find(x => x.Name == nodeDTO.Name);
      }
    }

    private bool IsDuplicateNode(string name){
      return _nodes.Find(x => x.Name == name) != null;
    }
  }
}
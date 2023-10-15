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

    public void LoadGraph(List<GraphNode> graphNodes){
      for(int i = 0; i < graphNodes.Count; i++){
        graphNodes[i].Index = i;
      }
      _nodes = graphNodes;
    }

    private bool IsDuplicateNode(string name){
      return _nodes.Find(x => x.Name == name) != null;
    }
  }
}
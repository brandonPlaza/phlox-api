using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PhloxAPI.Models.DTOs;
using PhloxAPI.Models.Entities;

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

    // Temporary solution until I find a more sophisticated way
    public static (List<CardinalDirection>, List<CardinalDirection>) ParseDirection(CardinalDirection cardinal){
      List<CardinalDirection> lefts = new();
      List<CardinalDirection> rights = new();

      switch(cardinal){
        case CardinalDirection.North:
          lefts.Add(CardinalDirection.NorthWest);
          lefts.Add(CardinalDirection.West);
          lefts.Add(CardinalDirection.SouthWest);

          rights.Add(CardinalDirection.NorthEast);
          rights.Add(CardinalDirection.East);
          rights.Add(CardinalDirection.SouthEast);
          break;
        case CardinalDirection.NorthEast:
          lefts.Add(CardinalDirection.North);
          lefts.Add(CardinalDirection.NorthWest);
          lefts.Add(CardinalDirection.West);

          rights.Add(CardinalDirection.East);
          rights.Add(CardinalDirection.SouthEast);
          rights.Add(CardinalDirection.South);
          break;
        case CardinalDirection.East:
          lefts.Add(CardinalDirection.NorthEast);
          lefts.Add(CardinalDirection.North);
          lefts.Add(CardinalDirection.NorthWest);

          rights.Add(CardinalDirection.SouthEast);
          rights.Add(CardinalDirection.South);
          rights.Add(CardinalDirection.SouthWest);
          break;
        case CardinalDirection.SouthEast:
          lefts.Add(CardinalDirection.East);
          lefts.Add(CardinalDirection.NorthEast);
          lefts.Add(CardinalDirection.North);

          rights.Add(CardinalDirection.South);
          rights.Add(CardinalDirection.SouthWest);
          rights.Add(CardinalDirection.West);
          break;
        case CardinalDirection.South:
          lefts.Add(CardinalDirection.SouthEast);
          lefts.Add(CardinalDirection.East);
          lefts.Add(CardinalDirection.NorthEast);

          rights.Add(CardinalDirection.SouthWest);
          rights.Add(CardinalDirection.West);
          rights.Add(CardinalDirection.NorthWest);
          break;
        case CardinalDirection.SouthWest:
          lefts.Add(CardinalDirection.South);
          lefts.Add(CardinalDirection.SouthEast);
          lefts.Add(CardinalDirection.East);

          rights.Add(CardinalDirection.West);
          rights.Add(CardinalDirection.NorthWest);
          rights.Add(CardinalDirection.North);
          break;
        case CardinalDirection.West:
          lefts.Add(CardinalDirection.SouthWest);
          lefts.Add(CardinalDirection.South);
          lefts.Add(CardinalDirection.SouthEast);

          rights.Add(CardinalDirection.NorthWest);
          rights.Add(CardinalDirection.North);
          rights.Add(CardinalDirection.NorthEast);
          break;
        case CardinalDirection.NorthWest:
          lefts.Add(CardinalDirection.West);
          lefts.Add(CardinalDirection.SouthWest);
          lefts.Add(CardinalDirection.South);

          rights.Add(CardinalDirection.North);
          rights.Add(CardinalDirection.NorthEast);
          rights.Add(CardinalDirection.East);
          break;    
      }
      return (lefts, rights);
    }
  }
}
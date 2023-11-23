using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PhloxAPI.Models.DTOs;
using PhloxAPI.Models.Entities;
using PhloxAPI.Services.RoutingService.Classes;

namespace PhloxAPI.Helpers
{
  public class GraphingHelper
  {
    public static List<GraphNode> GenerateGraph(Dictionary<string, NodeCacheDTO> nodes, Dictionary<string, ConnectionCacheDTO> connections, DisabilityType disability){
      // Filter connections by disability
      FilterConnectionsByDisability(nodes, ref connections, disability);

      // List of graph nodes to hold and return the filtered graph
      List<GraphNode> graph = new();

      // Get list of keys
      var connectionIds = connections.Keys.ToList();
      foreach(string id in connectionIds){
        string[] nodeIds = id.Split('|');
        GraphNode firstNode = GenerateGraphNode(ref nodes, ref graph, nodeIds[0], nodes[nodeIds[0]].Name);
        GraphNode secondNode = GenerateGraphNode(ref nodes, ref graph, nodeIds[1], nodes[nodeIds[1]].Name);
        firstNode.AddNeighbor(secondNode, connections[id].Weight, connections[id].Cardinality);
      }

      return graph;
    }

    private static GraphNode GenerateGraphNode(ref Dictionary<string, NodeCacheDTO> nodes, ref List<GraphNode> graph, string nodeId, string nodeName){
        GraphNode node;
        if(nodes.Keys.Contains(nodeId)){ 
          node = new GraphNode(nodeName);
          nodes.Remove(nodeId); 
          graph.Add(node);
        }
        else{
          node = graph.Find(x => x.Name == nodeName)!;
        }
        return node;
    }

    private static void FilterConnectionsByDisability(Dictionary<string, NodeCacheDTO> nodes, ref Dictionary<string, ConnectionCacheDTO> connections, DisabilityType disability){
      var connectionIds = connections.Keys.ToList();
      foreach(string id in connectionIds){
        string[] nodeIds = id.Split('|');
        if((IsAmenityCompatibleWithDisability((NodeTypes)nodes[nodeIds[0]].Type, disability) == false) || 
            IsAmenityCompatibleWithDisability((NodeTypes)nodes[nodeIds[1]].Type, disability) == false
        ){
          connections.Remove(id);
        }
        else if(nodes[nodeIds[0]].IsOutOfService || nodes[nodeIds[1]].IsOutOfService){
          connections.Remove(id);
        }
      }
    }

    private static bool IsAmenityCompatibleWithDisability(NodeTypes nodeType, DisabilityType disability){
      switch(disability){
        case DisabilityType.Physical:
          return nodeType==NodeTypes.Stairs ? false : true;
        case DisabilityType.Auditory:
          return true;
        case DisabilityType.Visual:
          return true;
        default:
          return true;
      }
    }
  }
}
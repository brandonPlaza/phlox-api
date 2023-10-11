using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PhloxAPI.Models.Entities;
using PhloxAPI.Services.RoutingService.Classes;

namespace PhloxAPI.Models.DTOs
{
    public class NodeRoutingDTO
    {
      public string? Name { get; set; }
      public NodeTypes NodeType { get; set; }
      public bool IsOutOfService { get; set; } = false;
      public Dictionary<GraphNode, int>? Neighbors { get; set; }
      public Dictionary<GraphNode, int>? Cardinality { get; set; }
      public int DistanceFromStart { get; set; }
    }
}
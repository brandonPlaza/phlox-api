using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PhloxAPI.Models.Entities;

namespace PhloxAPI.Models.DTOs
{
    public class NodeRoutingDTO
    {
      public string? Name { get; set; }
      public NodeTypes NodeType { get; set; }
      public bool IsOutOfService { get; set; } = false;
      public bool Visited { get; set; }
      public Node? VisitedBy { get; set; }
    }
}
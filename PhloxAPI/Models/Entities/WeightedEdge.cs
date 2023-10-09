using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhloxAPI.Models.Entities
{
  public class WeightedEdge
  {
    public Guid Id { get; set; }
    public List<Node> Nodes { get; set; }
    public int Weight { get; set; }
  }
}
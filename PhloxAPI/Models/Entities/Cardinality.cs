using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PhloxAPI.Models.Entities
{
  public class Cardinality
  {
    public Guid Id { get; set; }
    public Node Neighbor { get; set; }
    public CardinalDirection CardinalDirection { get; set; }
  }
}
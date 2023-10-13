using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PhloxAPI.Models.Entities
{
  public class Neighbor
  {
    public Guid Id { get; set; }
    public Node Node { get; set; }
    public int Weight { get; set; }
  }
}
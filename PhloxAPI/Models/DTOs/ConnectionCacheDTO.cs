using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhloxAPI.Models.DTOs
{
  public class ConnectionCacheDTO
  {
    public string Names { get; set; }
    public int Weight { get; set; }
    public int Cardinality { get; set; }
  }
}
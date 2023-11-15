using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PhloxAPI.Models.DTOs;

namespace PhloxAPI.Services.RoutingService.Classes
{
  public class MapCache
  {
    public DateTime LastUpdate { get; set; }
    public Dictionary<string, NodeCacheDTO> Nodes { get; set; }
    public Dictionary<string, ConnectionCacheDTO> Connections { get; set; }
  }
}
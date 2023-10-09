using Microsoft.EntityFrameworkCore;

namespace PhloxAPI.Models.Entities
{
  public class Node
  {
    public Guid Id { get; set; }
    public NodeTypes Type { get; set; }
    public string Name { get; set; }
    public List<Report> Reports { get; set; }
    public List<Node> CardinalConnections { get; set; }
    public bool IsOutOfService { get; set; } = false;
  }
}

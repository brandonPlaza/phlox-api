using PhloxAPI.Models.Entities;

namespace PhloxAPI.Models.DTOs
{
  public class NodeDTO
  {
    public Guid Id { get; set; }  
    public NodeTypes NodeType { get; set; }
    public string Name { get; set; }
    public List<Report> Reports { get; set; }
    public List<Node> CardinalConnections { get; set; }
    public bool IsOutOfService { get; set; } = false;
  }
}

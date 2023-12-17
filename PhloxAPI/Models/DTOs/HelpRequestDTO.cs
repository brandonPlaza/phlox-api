namespace PhloxAPI.Models.DTOs
{
  public class HelpRequestDTO
  {
    public String UserEmail { get; set; }
    public String NodeId { get; set; }
    public String? Description { get; set; }
  }

  public class NodeHelpRequestDTO
  {
    public String UserEmail { get; set; }
    public String Status { get; set; }
    public String? Description { get; set; }
    public int? Position { get; set; }
    public NodeSimpleDTO Node { get; set; }
  }

  public class StatusHelpRequestDTO
  {
    public string Id { get; set; }
    public string Status { get; set; }
    public int? Position { get; set; }
  }
}

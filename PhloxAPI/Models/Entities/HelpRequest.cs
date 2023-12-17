namespace PhloxAPI.Models.Entities
{
  public enum HelpRequestStatus
  {
    Waiting,
    Accepted,
    Completed,
    Cancelled
  }
  public class HelpRequest
  {
    public Guid Id { get; set; }
    public string UserEmail { get; set; }
    public string Status { get; set; }
    public int? Position { get; set; }
    public string? Description { get; set; }
    public Node Node { get; set; } = null!; //required reference navigation
    public DateTime TimeCreated { get; set; }
    public DateTime? TimeAccepted { get; set; }
    public DateTime? TimeCompleted { get; set; }
    public DateTime? TimeCancelled { get; set; }
  }
}

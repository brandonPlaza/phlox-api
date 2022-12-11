namespace PhloxAPI.DTOs
{
  public class HelpRequestDTO
  {
    public string UserEmail { get; set; }
    public string Latitude { get; set; }
    public string Longitude { get; set; }
  }

  public class StatusHelpRequestDTO
  {
    public string Id { get; set; }
    public string Status { get; set; }
    public int? Position { get; set; }
  }
}

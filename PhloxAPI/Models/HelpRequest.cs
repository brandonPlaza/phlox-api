namespace PhloxAPI.Models
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
        public User User { get; set; }
        public HelpRequestStatus Status { get; set; }
        public int? Position { get; set; }
        public Latitude Latitude { get; set; }
        public Longitute Longitute { get; set; }
        public DateTime TimeCreated { get; set; }
        public DateTime? TimeAccepted { get; set; }
        public DateTime? TimeCompleted { get; set; }
        public DateTime? TimeCancelled { get; set; }
    }
}

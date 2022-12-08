

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
        public string UserEmail { get; set; }
        public HelpRequestStatus Status { get; set; }
        public int? Position { get; set; }
        public double Latitude { get; set; }
        public double Longitute { get; set; }
        public DateTime TimeCreated { get; set; }
        public DateTime? TimeAccepted { get; set; }
        public DateTime? TimeCompleted { get; set; }
        public DateTime? TimeCancelled { get; set; }
    }
}

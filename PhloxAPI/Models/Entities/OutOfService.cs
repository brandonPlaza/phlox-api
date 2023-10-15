namespace PhloxAPI.Models.Entities
{
    public class OutOfService
    {
        public Guid Id { get; set; }
        public Node AffectedNode { get; set; }
        public DateTime ReportedAt { get; set; }
        public DateTime? ResolvedAt { get; set; }
    }
}

using PhloxAPI.Models.DTOs;

namespace PhloxAPI.Models.Entities
{
    public class Report
    {
        public Guid Id { get; set; }
        public Node NodeAffected { get; set; }
		public string? UserMessage { get; set; }
		public DateTime ReportedAt { get; set; }
		public DateTime? ResolvedAt { get; set; }
		public bool Resolved { get; set; }
	}
}

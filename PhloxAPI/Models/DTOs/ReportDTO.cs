namespace PhloxAPI.Models.Entities
{
	public class ReportDTO
	{
		public Guid Id { get; set; }
		public string? UserMessage { get; set; }
		public DateTime ReportedAt { get; set; }
		public DateTime? ResolvedAt { get; set; }
		public bool Resolved { get; set; }
	}
}

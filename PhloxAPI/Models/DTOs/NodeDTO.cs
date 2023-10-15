using PhloxAPI.Models.Entities;

namespace PhloxAPI.Models.DTOs
{
    public class NodeDTO
	{
		public Guid Id { get; set; }
		public NodeTypes NodeType { get; set; }
		public string Name { get; set; }
		public List<ReportDTO> Reports { get; set; }
		public bool IsOutOfService { get; set; } = false;
		public List<OutOfService> OutOfServiceHistory { get; set; }
	}
}

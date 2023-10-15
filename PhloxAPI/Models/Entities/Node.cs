using Microsoft.EntityFrameworkCore;

namespace PhloxAPI.Models.Entities
{
    public class Node
	{
		public Guid Id { get; set; }
		public NodeTypes Type { get; set; }
		public string Name { get; set; }
		public List<Report> Reports { get; set; } = new List<Report>();
		public List<Neighbor> Neighbors { get; set; }
		public List<Cardinality> Cardinalities { get; set; }
		public bool IsOutOfService { get; set; } = false;
		public List<OutOfService> OutOfServiceHistory { get; set; } = new List<OutOfService>();

	}
}

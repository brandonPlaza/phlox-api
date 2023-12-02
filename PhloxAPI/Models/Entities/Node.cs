using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhloxAPI.Models.Entities
{
    public class Node
	{
		public Guid Id { get; set; }

		public char Building {get; set;} = 's';
		public NodeTypes Type { get; set; }
		public string Name { get; set; }
		public List<Report> Reports { get; set; } = new List<Report>();
		public List<Neighbor> Neighbors { get; set; }
		public List<Cardinality> Cardinalities { get; set; }
		public bool IsOutOfService { get; set; } = false;
		public List<OutOfService> OutOfServiceHistory { get; set; } = new List<OutOfService>();

	}
}

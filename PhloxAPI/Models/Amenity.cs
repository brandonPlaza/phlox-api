using Microsoft.EntityFrameworkCore;

namespace PhloxAPI.Models
{
    public enum AmenityType
    {
        Elevator,
        Ramp,
        AutomaticDoor
    }
    public class Amenity
    {
        public Guid Id { get; set; }
        public AmenityType Type { get; set; }
        public string Name { get; set; }
        public Building ConnectedBuilding { get; set; }
        public char Building { get; set; }
        public int Floor { get; set; }
        public List<Report> Reports { get; set; }
        public bool IsOutOfService { get; set; } = false;
    }
}

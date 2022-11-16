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
        public List<Amenity> ConnectedAmenities { get; set; }
        public bool IsOutOfService { get; set; } = false;
    }
}

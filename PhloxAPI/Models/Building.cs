namespace PhloxAPI.Models
{
    public class Building
    {
        public Guid Id { get; set; }
        public char Letter { get; set; }
        public List<Building> ConnectedBuildings { get; set; }
    }
}

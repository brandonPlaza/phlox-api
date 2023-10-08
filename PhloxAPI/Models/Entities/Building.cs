namespace PhloxAPI.Models.Entities
{
    public class Building
    {
        public Guid? Id { get; set; }
        public char Letter { get; set; }
        public char ConnectedBuilding { get; set; }
    }
}

namespace PhloxAPI.Models.Entities
{
    public enum ReportType
    {
        ElevatorDown,
        RampClosed,
        EntranceBlocked,
    }
    public class Report
    {
        public Guid Id { get; set; }
        public ReportType Type { get; set; }
        public Node Amenity { get; set; }
    }
}

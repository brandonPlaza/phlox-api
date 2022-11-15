namespace Phlox_API.Models
{
    public enum ReportType
    {
        DoorBlockedOff,
        ElevatorDown,
        RampClosed,
    }
    public class Report
    {
        public Guid Id { get; set; }
        public ReportType ReportType { get; set; }
        public string Description { get; set; }
        public char Building { get; set; }
        public int Floor { get; set; }
    }
}

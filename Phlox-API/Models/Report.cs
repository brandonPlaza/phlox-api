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
        public ReportType ReportType { get; set; }
        public string Description { get; set; }
    }
}

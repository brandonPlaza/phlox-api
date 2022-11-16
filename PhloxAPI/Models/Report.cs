namespace PhloxAPI.Models
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
        public char Building { get; set; }
        public int Floor { get; set; }
    }
}

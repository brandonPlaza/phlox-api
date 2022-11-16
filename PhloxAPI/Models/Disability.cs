namespace PhloxAPI.Models
{
    public enum DisabilityType
    {
        Phisical,
        Auditory,
        Visual
    }
    public class Disability
    {
        public Guid Id { get; set; }
        public DisabilityType Type { get; set; }
    }
}

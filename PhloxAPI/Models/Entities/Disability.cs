namespace PhloxAPI.Models.Entities
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

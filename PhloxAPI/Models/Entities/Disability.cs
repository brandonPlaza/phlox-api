namespace PhloxAPI.Models.Entities
{
    public enum DisabilityType
    {
        Physical,
        Auditory,
        Visual,
        NoDisability
    }
    public class Disability
    {
        public Guid Id { get; set; }
        public DisabilityType Type { get; set; }
    }
}

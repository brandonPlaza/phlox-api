namespace Phlox_API.Models
{
    public enum DissabilityType{
        Auditory,
        Visual,
        Physical
    }
    public class Dissability
    {
        public DissabilityType Type { get; set; }
    }
}

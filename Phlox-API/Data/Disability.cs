namespace Phlox_API.Data
{
    public enum TypeOfDisability
    {
        Visual,
        Auditory,
        Physical
    }
    public class Disability
    {
        public TypeOfDisability Type { get; set; }
        public string Description { get; set; }

    }
}

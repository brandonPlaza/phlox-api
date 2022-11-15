using Microsoft.EntityFrameworkCore;

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
        public Disability() { }

        public Guid Id { get; set; }
        public TypeOfDisability Type { get; set; }
        public string Description { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using PhloxAPI.Data;
using PhloxAPI.Models;

namespace PhloxAPI.Services.AdministrationService
{
    public class AdministrationService : IAdministrationService
    {
        private readonly PhloxDbContext _context;

        public AdministrationService(PhloxDbContext context) {
            _context = context;
        }

        public void AddAmenity(Amenity amenity)
        {
            _context.Amenities.Add(amenity);
            _context.SaveChanges();
        }

        public List<Amenity> GetAmenities()
        {
            throw new NotImplementedException();
        }

        public Amenity UpdateAmenity()
        {
            throw new NotImplementedException();
        }
    }
}

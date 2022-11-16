using PhloxAPI.Models;

namespace PhloxAPI.Services.AdministrationService
{
    public interface IAdministrationService
    {
        void AddAmenity(Amenity amenity);
        List<Amenity> GetAmenities();
        Amenity UpdateAmenity();
    }
}

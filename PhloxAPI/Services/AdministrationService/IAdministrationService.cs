using PhloxAPI.Models;

namespace PhloxAPI.Services.AdministrationService
{
    public interface IAdministrationService
    {
        void AddAmenity(Amenity amenity);
        void AddBuilding(Building building);
        void ConnectBuildings(char buildingOne, char buidingTwo);
        List<Amenity> GetAmenities();
        Amenity UpdateAmenity();
    }
}

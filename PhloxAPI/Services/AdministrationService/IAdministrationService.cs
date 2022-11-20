using PhloxAPI.Models;

namespace PhloxAPI.Services.AdministrationService
{
    public interface IAdministrationService
    {
        void AddAmenity(string name, int type, char building, char connectedBuildin);
        void AddBuilding(Building building);
        void ConnectBuildings(char buildingOne, char buidingTwo);
        List<Building> GetBuildings();
        List<Amenity> GetAmenities();
        Amenity UpdateAmenity();
    }
}

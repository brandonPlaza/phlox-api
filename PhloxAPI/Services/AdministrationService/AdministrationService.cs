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

        public void AddAmenity(string name, int type, char building, char connectedBuilding)
        {
            var newAmenity = new Amenity { Floor = 0, Building = building, ConnectedBuilding = _context.Buildings.First(x => x.Letter == connectedBuilding), Name = name, Type = (AmenityType)type};
            _context.Amenities.Add(newAmenity);
            _context.SaveChanges();
        }

        public void AddBuilding(Building building)
        {
            _context.Buildings.Add(building);
            _context.SaveChanges();
        }

        public void ConnectBuildings(char buildingOne, char buildingTwo)
        {
            var building1 = _context.Buildings.Include(b1 => b1.ConnectedBuildings).First(b1 => b1.Letter == buildingOne);
            var building2 = _context.Buildings.Include(b2 => b2.ConnectedBuildings).First(b2 => b2.Letter == buildingTwo);

            building1.ConnectedBuildings.Add(building2);

            building2.ConnectedBuildings.Add(building1);

            _context.SaveChanges();
        }

        public List<Amenity> GetAmenities()
        {
            throw new NotImplementedException();
        }

        public List<Building> GetBuildings()
        {
            return _context.Buildings.Include(building => building.ConnectedBuildings).ToList();
        }

        public Amenity UpdateAmenity()
        {
            throw new NotImplementedException();
        }
    }
}

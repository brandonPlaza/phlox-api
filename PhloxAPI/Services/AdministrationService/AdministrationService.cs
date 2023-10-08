using Microsoft.EntityFrameworkCore;
using PhloxAPI.Data;
using PhloxAPI.Models;
using PhloxAPI.Models.Entities;

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
            var newAmenity = new Node {Name = name, Type = (NodeTypes)type};
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
            var building1 = _context.Buildings.First(b1 => b1.Letter == buildingOne);
            var building2 = _context.Buildings.First(b2 => b2.Letter == buildingTwo);

            var building2Connected = _context.Buildings.First(x => x.Letter == building1.Letter);

            building2Connected.ConnectedBuilding = building2.Letter;

            _context.SaveChanges();
        }

        public List<Node> GetAmenities()
        {
            return _context.Amenities.ToList();
        }

        public List<Building> GetBuildings()
        {
            return _context.Buildings.ToList();
        }

        public Node UpdateAmenity()
        {
            throw new NotImplementedException();
        }
    }
}

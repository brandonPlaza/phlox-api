using Microsoft.EntityFrameworkCore;
using PhloxAPI.Data;
using PhloxAPI.Models;

namespace PhloxAPI.Services.RoutingService
{
    public class RoutingService : IRoutingService
    {
        private readonly PhloxDbContext _context;

        public RoutingService(PhloxDbContext context)
        {
            _context = context;
        }

        public List<Amenity> RequestRoute(char currentBuilding, char destinationBuilding)
        {
            Building currBuilding = _context.Buildings.Include(currBuild => currBuild.ConnectedBuildings).First(currBuild => currBuild.Letter == currentBuilding);
            Building destBuilding = _context.Buildings.Include(dBuild => dBuild.ConnectedBuildings).First(dBuild => dBuild.Letter == destinationBuilding);

            var buildingRoute = ConstructBuildingRoute(currBuilding, destBuilding);
            var amenityRoute = ConstructAmenityRoute(currentBuilding, buildingRoute);

            return amenityRoute;
        }

        private List<char> ConstructBuildingRoute(Building currBuilding, Building destBuilding)
        {
            List<char> buildingRoute = new List<char>();

            if (currBuilding.Letter == destBuilding.Letter)
                return new List<char>();

            var nextHop = currBuilding.ConnectedBuildings[0];
            Building prevHop;

            buildingRoute.Add(nextHop.Letter);


            while(nextHop.Letter != destBuilding.Letter)
            {
                var x = 0;
                prevHop = nextHop;
                nextHop = nextHop.ConnectedBuildings[x];
                buildingRoute.Add(nextHop.Letter);

                if (prevHop.Letter == nextHop.ConnectedBuildings[x].Letter)
                {
                    x++;
                    prevHop = nextHop;
                    nextHop = nextHop.ConnectedBuildings[x];
                    buildingRoute.Add(nextHop.Letter);
                    x--;
                }
            }

            return buildingRoute;
        }
        private List<Amenity> ConstructAmenityRoute(char currBuilding, List<char> buildingRoute)
        {
            var amenityList = new List<Amenity>();
            while (true)
            {
                amenityList.Add(_context.Amenities.First(a => a.Building == currBuilding && a.ConnectedBuilding.Letter == buildingRoute[0]));
                break;
            }

            return amenityList;
        }
    }
}

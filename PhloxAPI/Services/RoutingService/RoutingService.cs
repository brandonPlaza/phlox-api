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
            Building currBuilding = _context.Buildings.First(currBuild => currBuild.Letter == currentBuilding);
            Building destBuilding = _context.Buildings.First(dBuild => dBuild.Letter == destinationBuilding);

            if (currBuilding.Letter == destBuilding.Letter)
                return null;

            var buildingRoute = ConstructRoute(currBuilding, destBuilding);
            var amenityRoute = BuildAmenityRoute(currBuilding, buildingRoute, destBuilding);
            return amenityRoute;
        }

        /// <summary>
        /// Construct a route between two buildings
        /// </summary>
        /// <param name="currBuilding"></param>
        /// <param name="destBuilding"></param>
        /// <returns>List<Building></returns>
        private List<Building> ConstructRoute(Building currBuilding, Building destBuilding)
        {
            // Trace route to currBuilding 
            //var currBuildingTraceRoute = TraceRoute(currBuilding);

            // Trace route to destBuilding 
            //var destBuildingTraceRoute = TraceRoute(destBuilding);

            var buildingTraceRoute = TraceRoute(currBuilding, destBuilding);

            var validatedRoute = ValidateRoute(currBuilding, buildingTraceRoute);

            // Return not instersected building route
            return validatedRoute;
        }

        /// <summary>
        /// Trace a given building in the list of buildings and return the route to it
        /// </summary>
        /// <param name="buildingToFind"></param>
        /// <returns>List<Building></returns>
        private List<Building> TraceRoute(Building currBuilding, Building destBuilding)
        {
            // Get all buildings in a list
            var buildingList = _context.Buildings.ToList();

            // Create a new list to hold the route from the beginning of the list to the building 
            var buildingRoute = new List<Building>();

            bool buildingFound = false;

            // Loop through buildingsList until the required building is found
            foreach (Building building in buildingList)
            {
                if (building.Letter == currBuilding.Letter || building.Letter == destBuilding.Letter)
                {
                    buildingRoute.Add(building);

                    //Toggle building found flag to start recording the route
                    buildingFound = buildingFound ? false : true;
                }

                // This else if is meant to trigger only when the loop is passing through the route between curr and dest
                else if (buildingFound)
                {
                    buildingRoute.Add(building);
                }
            }

            // return the route of buildings
            return buildingRoute;
        }

        /// <summary>
        /// Validate that a route is constructed from the current building to the destination building
        /// </summary>
        /// <param name="currBuilding"></param>
        /// <param name="route"></param>
        /// <returns>List<Building></returns>
        private List<Building> ValidateRoute(Building currBuilding, List<Building> route)
        {
            // Check to make sure the route is already valid
            if (currBuilding.Letter == route.First().Letter)
                return route;

            // Create a new list to hold the flipped route
            List<Building> flippedRoute = new List<Building>();

            // Loop through the original route backwards and add elements from the original route to the new route backwards 
            for(int x = route.Count-1; x>=0; x--)
            {
                flippedRoute.Add(route[x]);
            }

            // Return the flipped route
            return flippedRoute;
        }

        /// <summary>
        /// Build a route between two buildings with amenities that connect them
        /// </summary>
        /// <param name="currBuilding"></param>
        /// <param name="route"></param>
        /// <returns></returns>
        private List<Amenity> BuildAmenityRoute(Building currBuilding, List<Building> route, Building destBuilding)
        {
            // Create list to hold amenity route
            var amenityRoute = new List<Amenity>();

            //var prevHop = currBuilding;
            //var nextHop = route.First();

            /*var index = 1;
            while (true)
            {
                var amenity = _context.Amenities.First(a =>  ((a.Building == prevHop.Letter) || (a.Building == nextHop.Letter)) && ((a.ConnectedBuilding.Letter == prevHop.Letter || (a.ConnectedBuilding.Letter == nextHop.Letter) ) ) );
                amenityRoute.Add(amenity);

                prevHop = nextHop;
                if (index == route.Count)
                {
                    amenity = _context.Amenities.FirstOrDefault(a => ( (a.Building == prevHop.Letter) || (a.Building == nextHop.Letter) ) && ( (a.ConnectedBuilding.Letter == prevHop.Letter || (a.ConnectedBuilding.Letter == nextHop.Letter) ) ) );

                    if(amenity != null)
                        amenityRoute.Add(amenity);
                    break;
                }
                nextHop = route[index];
                index++;
            }

            return amenityRoute;
        }

        /*private List<char> ConstructBuildingRoute(Building currBuilding, Building destBuilding)
        {
            List<char> buildingRoute = new List<char>();

            if (currBuilding.Letter == destBuilding.Letter)
                return new List<char>();

            var nextHop = currBuilding.ConnectedBuildings;
            Building prevHop;

            buildingRoute.Add(nextHop.First().Letter);


            while(nextHop.Letter != destBuilding.Letter)
            {
                var x = 0;
                prevHop = nextHop;
                nextHop = nextHop.ConnectedBuilding;
                buildingRoute.Add(nextHop.Letter);

                if (prevHop.Letter == nextHop.ConnectedBuilding.Letter)
                {
                    x++;
                    prevHop = nextHop;
                    nextHop = nextHop.ConnectedBuilding;
                    buildingRoute.Add(nextHop.Letter);
                    x--;
                }
            }

            for (int x = 0; x<route.Count-1; x++)
            {
                var amenity = _context.Amenities.First(a => ((a.Building == route[x].Letter) || (a.ConnectedBuilding.Letter == route[x].Letter)) && ((a.Building == route[x+1].Letter) || (a.ConnectedBuilding.Letter == route[x+1].Letter)));
                amenityRoute.Add(amenity);
            }

            return amenityRoute;
        }
    }
}

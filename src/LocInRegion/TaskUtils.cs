using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace LocInRegion
{
    public static class TaskUtils
    {
        /// <summary>
        /// Out of nested list of doubles (coordinates) makes a polygon
        /// </summary>
        /// <param name="coordOriginal">List of coordinates, outer list holds all of the coordinates, inner a point [long, lati]</param>
        /// <returns> Polygon</returns>
        public static Polygon MakePolygon(List<List<double>> coordOriginal)
        {
            var gf = NetTopologySuite.NtsGeometryServices.Instance.CreateGeometryFactory(4326);

            var coords = new List<Coordinate>();

            foreach (var point in coordOriginal)
            {
                coords.Add(new Coordinate(point[0], point[1]));
            }

            if (!coords.First().Equals2D(coords.Last()))
            {
                coords.Add(coords.First());
            }


            LinearRing ring = gf.CreateLinearRing(coords.ToArray()); //last and first points need to match for LinearRing to be created
            Polygon polygon = gf.CreatePolygon(ring);

            return polygon;
        }

        /// <summary>
        /// Given a location and a region shows if location is in that region
        /// </summary>
        /// <param name="loc"></param>
        /// <param name="region"></param>
        /// <returns>true if loc is in region otherwise false</returns>
        public static bool IsLocInReg(Location location, Region region)
        {
            var gf = NtsGeometryServices.Instance.CreateGeometryFactory(4326);

            var point = gf.CreatePoint(new Coordinate(location.Coordinates[0], location.Coordinates[1]));

            foreach (var ringCoords in region.Coordinates)
            {
                var polygon = MakePolygon(ringCoords);
                if (polygon == null)
                {
                    continue;
                }
                else if (polygon.Contains(point) || polygon.Touches(point))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Loops through list of regions and list of locations. If location belongs in a given region they get paired.
        /// One region can hold many locations or none
        /// </summary>
        /// <param name="regions">list of regions</param>
        /// <param name="locations">list of locations</param>
        /// <returns>List of RegionAndLocations objects</returns>
        public static List<RegionAndLocations> CreateRL(List<Region> regions, List<Location> locations)
        {
            List<RegionAndLocations> regionsAndLocations = new List<RegionAndLocations>();

            foreach(var region in regions)
            {
                List<string> allLocations = new List<string>();
                    
                foreach (var location in locations)
                {
                     if(IsLocInReg(location, region))
                     {
                         allLocations.Add(location.Name);
                     }
                }
                RegionAndLocations regionLocation = new RegionAndLocations(region.Name, allLocations);
                regionsAndLocations.Add(regionLocation);
            }
            return regionsAndLocations;
        }

    }
}

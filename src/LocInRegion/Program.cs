using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.Globalization;

namespace LocInRegion
{
    class Program
    {
        private static string RegionData = "regions.json";
        private static string LocationData = "locations.json";
        private static string FolderName = "Data";
        private static string Results = "output.json";

        private static string precision = "G17";
        static void Main(string[] args)
        {

            if (File.Exists(Results))
            {
                File.Delete(Results);
            }

            var regionsPath = Path.Combine(AppContext.BaseDirectory, FolderName, RegionData);
            var locationsPath = Path.Combine(AppContext.BaseDirectory, FolderName, LocationData);

            List<Region> regions = IOUtils.ReadRegion(regionsPath);
            List<Location> locations = IOUtils.ReadLocation(locationsPath);

            // Initial Data (testing)
            Console.WriteLine("Some inaccuracies due to rounding, but only at the end of the decimal"
               + "\n and only when displaying");
            DisplayRegion(regions);
            DisplayLocation(locations);

            // Writing to json
            List<RegionAndLocations> regionsAndLocations = TaskUtils.CreateRL(regions, locations);
            DisplayRegionAndLocations(regionsAndLocations);

            IOUtils.WriteJSON(Results, regionsAndLocations);
        }

        /// <summary>
        /// Displays initial data of regions list
        /// </summary>
        /// <param name="regions">list of regions</param>
        public static void DisplayRegion(List<Region> regions)
        {
            
            foreach (var region in regions)
            {
                Console.WriteLine("{0} has {1} polygon(s).", region.Name, region.Coordinates.Count);
                Console.WriteLine();

                foreach (var location in region.Coordinates)
                {
                    foreach (var coords in location)
                    {
                        var longitude = coords[0].ToString(precision, CultureInfo.InvariantCulture);
                        var latitude = coords[1].ToString(precision, CultureInfo.InvariantCulture);

                        Console.WriteLine("(Longitude is {0}, Latitude is {1})", longitude, latitude);
                    }

                    Console.WriteLine();
                }
            }

            Console.WriteLine();
        }

        /// <summary>
        /// Displays initial data of locations list
        /// </summary>
        /// <param name="locations">list of locations</param>
        public static void DisplayLocation(List<Location> locations)
        {
            Console.WriteLine("locations initial data:");
            Console.WriteLine();

            foreach (var location in locations)
            {
                // had to specify precision, because it was rounding the double
                var longitude = location.Coordinates[0].ToString(precision, CultureInfo.InvariantCulture);
                var latitude = location.Coordinates[1].ToString(precision, CultureInfo.InvariantCulture);

                Console.WriteLine("{0}: (Longitude is {1}, Latitude is {2})", location.Name, longitude, latitude);
            }

            Console.WriteLine();
        }

        /// <summary>
        /// Displays list of RegionAndLocations objects and its information to the console.
        /// </summary>
        /// <param name="regionsAndLocations">list of RegionAndLocations objects</param>
        public static void DisplayRegionAndLocations(List<RegionAndLocations> regionsAndLocations)
        {

            foreach (var regionAndLocations in regionsAndLocations)
            {
                Console.WriteLine("{0} has these locations:", regionAndLocations.Region);
                List<string> locations = regionAndLocations.MatchedLocations;

                foreach(var location in locations)
                {
                    Console.WriteLine("{0}", location);
                }
                Console.WriteLine();
            }
        }
    }
}

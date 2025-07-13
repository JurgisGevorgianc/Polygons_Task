using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LocInRegion
{
    static class IOUtils
    {
        /// <summary>
        /// Reads region data for JSON file
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public static List<Region> ReadRegion(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("Regions file is not found");
            }

            var json = File.ReadAllText(path);
            var regions = JsonSerializer.Deserialize<List<Region>>(json);

            if (regions == null)
            {
                throw new InvalidOperationException("Failed to parse regions JSON");
            }

            return regions;
        }   

        /// <summary>
        /// Reads location data from JSON file
        /// </summary>
        /// <param name="path">JSON file path</param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public static List<Location> ReadLocation(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("Locations file is not found");
            }

            var json = File.ReadAllText(path);
            var locations = JsonSerializer.Deserialize<List<Location>>(json);

            if (locations == null)
            {
                throw new InvalidOperationException("Failed to parse locations JSON.");
            }

            return locations;
        }

        /// <summary>
        /// Writes data from RegionAndLocation list to JSON file
        /// </summary>
        /// <param name="resultsFile">JSON file path</param>
        /// <param name="results">list of RegionAndLocations objects to be written from</param>
        public static void WriteJSON(string resultsFile, List<RegionAndLocations> results)
        {
            var options = new JsonSerializerOptions();
            options.WriteIndented = true;

            string json = JsonSerializer.Serialize(results, options);
            File.WriteAllText(resultsFile, json);
        }

    }
}

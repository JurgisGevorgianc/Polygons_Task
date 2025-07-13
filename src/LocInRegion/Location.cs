using System.Text.Json.Serialization;

namespace LocInRegion
{
    public class Location
    {
        [JsonPropertyName("name")]  // Needed so no casing issues
        public string Name { get; set; }

        [JsonPropertyName("coordinates")]
        public double[] Coordinates { get; set; } // locations.json contains coordinates: [lon, lat]
        
        public Location() { }
        public Location(string name, double[] coordinates)
        {
            Name = name;
            Coordinates = coordinates;
        }
    }
}

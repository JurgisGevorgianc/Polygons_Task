using System.Text.Json.Serialization;

namespace LocInRegion
{
    public class Region
    {
        [JsonPropertyName("name")]   // Needed so no casing issues
        public string Name { get; set; }

        [JsonPropertyName("coordinates")]
        public List<List<List<double>>> Coordinates { get; set; }  // regions.json contains coordinates: [ [ [lon, lat], ...], [ [lon, lat], ...] ]

        public Region() { }
        public Region(string name, List<List<List<double>>> coordinates)
        {
            this.Name = name;
            this.Coordinates = coordinates;
        }
    }
}

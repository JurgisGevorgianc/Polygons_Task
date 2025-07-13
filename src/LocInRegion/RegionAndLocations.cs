using System.Text.Json.Serialization;

namespace LocInRegion
{
    public class RegionAndLocations
    {
        [JsonPropertyName("region")]
        public string Region { get; set; }

        [JsonPropertyName("matched_locations")]
        public List<string> MatchedLocations { get; set; }

        public RegionAndLocations() { }
        public RegionAndLocations(string region, List<string> matchedLocations)
        {
            this.Region = region;
            this.MatchedLocations = matchedLocations;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocInRegion;

namespace UnitTest1
{
    public class CreateRLTests
    {
        [Fact]
        public void CreateRL_EmptyLists()
        {
            // arrange
            List<Location> locations = new List<Location>();
            List<Region> regions = new List<Region>();
            List<RegionAndLocations> regionAndLocations = new List<RegionAndLocations>();

            // act
            regionAndLocations = TaskUtils.CreateRL(regions, locations);

            // assert
            Assert.Empty(regionAndLocations);
        }

        [Fact]
        public void CreateRL_EmptyRegions()
        {
            // arrange
            double[] coordinates = [0.9, 0.9];
            Location locationOne = new Location("loc1", coordinates);

            List<Location> locations = new List<Location>();
            locations.Add(locationOne);

            List<Region> regions = new List<Region>();

            List<RegionAndLocations> regionAndLocations = new List<RegionAndLocations>();

            // act
            regionAndLocations = TaskUtils.CreateRL(regions, locations);

            // assert
            Assert.Empty(regionAndLocations);
        }

        [Fact]
        public void CreateRL_EmptyLocations()
        {
            // arrange
            List<Location> locations = new List<Location>();

            List<List<List<double>>> coords = new List<List<List<double>>>();
            coords = [[[0.0, 0.0], [1.1, 0.0], [1.1, 1.1], [1.1, 0.0], [0.0, 0.0]]];
            Region regionOne = new Region("region1", coords);

            List<Region> regions = new List<Region>();
            regions.Add(regionOne);

            List<RegionAndLocations> regionAndLocations = new List<RegionAndLocations>();

            // act
            regionAndLocations = TaskUtils.CreateRL(regions, locations);

            // assert
            Assert.Equal(regionAndLocations[0].Region, regionOne.Name);
        }

        [Fact]
        public void CreateRL_PopulatedLists()
        {
            // arrange
            double[] coordinates = [0.9, 0.9];
            Location locationOne = new Location("loc1", coordinates);

            List<Location> locations = new List<Location>(); 
            locations.Add(locationOne);

            List<List<List<double>>> coords = new List<List<List<double>>>();
            coords = [[[0.0, 0.0], [1.1, 0.0], [1.1, 1.1], [1.1, 0.0], [0.0, 0.0]]];
            Region regionOne = new Region("region1", coords);

            List<Region> regions = new List<Region>();
            regions.Add(regionOne);

            List<RegionAndLocations> regionAndLocations = new List<RegionAndLocations>();

            // act
            regionAndLocations = TaskUtils.CreateRL(regions, locations);

            // assert
            Assert.Equal(regionAndLocations[0].Region, regions[0].Name);
            Assert.Equal(regionAndLocations[0].MatchedLocations.Count, locations.Count);

        }

        [Fact]
        public void CreateRL_PopulatedListsWithOverlapingRegionsAndLocations()
        {
            // arrange
            double[] coordinates = [0.9, 0.9];
            Location locationOne = new Location("loc1", coordinates);

            double[] coordinatesTwo = [0.9, 0.9];
            Location locationTwo = new Location("loc2", coordinates);

            double[] coordinatesThree = [2, 2];
            Location locationThree = new Location("loc3", coordinates);

            List<Location> locations = new List<Location>();
            locations.Add(locationOne); locations.Add(locationTwo); locations.Add(locationThree);

            List<List<List<double>>> coords = new List<List<List<double>>>();
            coords = [[[0.0, 0.0], [1.1, 0.0], [1.1, 1.1], [1.1, 0.0], [0.0, 0.0]]];
            Region regionOne = new Region("region1", coords);

            List<List<List<double>>> coords2 = new List<List<List<double>>>();
            coords = [[[0.0, 0.0], [1.2, 0.0], [1.2, 1.2], [1.2, 0.0], [0.0, 0.0]]];
            Region regionTwo = new Region("region2", coords);

            List<Region> regions = new List<Region>();
            regions.Add(regionOne); regions.Add(regionTwo);

            List<RegionAndLocations> regionAndLocations = new List<RegionAndLocations>();

            // act
            regionAndLocations = TaskUtils.CreateRL(regions, locations);

            // assert
            Assert.Equal(regionAndLocations[0].MatchedLocations.Count, regionAndLocations[1].MatchedLocations.Count);
            Assert.NotEqual(regionAndLocations[0].Region, regionAndLocations[1].Region);
        }
    }
}

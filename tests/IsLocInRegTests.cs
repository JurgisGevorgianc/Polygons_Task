using LocInRegion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest1
{
    public class IsLocInRegTests
    {
        [Fact]
        public void IsLocInReg_LocationOutsideRegion()
        {
            // arrange
            double[] coordinate = { 10.1, 10.1 };
            Location location = new Location("loc1", coordinate);

            List<List<List<double>>> coords = new List<List<List<double>>>();
            coords = [[[0.0, 0.0], [1.1, 0.0], [1.1, 1.1], [1.1, 0.0], [0.0, 0.0]]];
            Region region = new Region("region1", coords);

            // act
            bool logic = TaskUtils.IsLocInReg(location, region);

            // assert
            Assert.Equal(5, region.Coordinates[0].Count);
            Assert.False(logic);
        }

        [Fact]
        public void IsLocInReg_LocationInsideRegion()
        {
            // arrange
            double[] coordinate = { 0.5, 0.5 };
            Location location = new Location("loc1", coordinate);

            List<List<List<double>>> coords = new List<List<List<double>>>();
            coords = [[[0.0, 0.0], [1.1, 0.0], [1.1, 1.1], [1.1, 0.0], [0.0, 0.0]]];
            Region region = new Region("region1", coords);

            // act
            bool logic = TaskUtils.IsLocInReg(location, region);

            // assert
            Assert.Equal(5, region.Coordinates[0].Count);
            Assert.True(logic);
        }

        [Fact]
        public void IsLocInReg_LocationoOnRing()
        {
            // arrange
            double[] coordinate = { 1.1, 1.1 };
            Location location = new Location("loc1", coordinate);

            List<List<List<double>>> coords = new List<List<List<double>>>();
            coords = [[[0.0, 0.0], [1.1, 0.0], [1.1, 1.1], [1.1, 0.0], [0.0, 0.0]]];
            Region region = new Region("region1", coords);

            // act
            bool logic = TaskUtils.IsLocInReg(location, region);

            // assert
            Assert.Equal(5, region.Coordinates[0].Count);
            Assert.True(logic);
        }
    }
}
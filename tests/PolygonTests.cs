using LocInRegion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest1
{
    public class PolygonTests
    {
        [Fact]
        public void MakePolygon_CorrectPoligon()
        {
            // arrange
            List<List<double>> coords = new List<List<double>>();
            coords = [[0.0, 0.0], [1.1, 0.0], [1.1, 1.1], [1.1, 0.0], [0.0, 0.0]];

            // act
            var polygon = TaskUtils.MakePolygon(coords);

            // assert
            var shellCoords = polygon.ExteriorRing.Coordinates;
            Assert.Equal(shellCoords.First(), shellCoords.Last());
        }

        [Fact]
        public void MakePolygon_LastAndFirstCoordinatesDontMatchPoligon()
        {
            // arrange
            List<List<double>> coords = new List<List<double>>();
            coords = [[0.0, 0.0], [1.1, 0.0], [1.1, 1.1], [1.1, 0.0], [0.0, 0.0]];

            // act
            var poly = TaskUtils.MakePolygon(coords);

            // assert
            var shellCoords = poly.ExteriorRing.Coordinates;
            Assert.Equal(shellCoords.First(), shellCoords.Last());
        }
    }
}

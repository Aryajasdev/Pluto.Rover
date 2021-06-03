using System;
using NUnit.Framework;
using Rover.Pluto.Core.Impl;

namespace Rover.Pluto.Test
{
    [TestFixture]
    public class PositionTests
    {
        [Test]
        public void Coordinate_Throws_ArgumentException_For_Negative_Latitude() =>
            Assert.Throws<ArgumentException>(() => new Coordinate(-1, 0));

        [Test]
        public void Coordinate_Throws_ArgumentException_For_Negative_Longitude() =>
            Assert.Throws<ArgumentException>(() => new Coordinate(0, -1));

    }
}
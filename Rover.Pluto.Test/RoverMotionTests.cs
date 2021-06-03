using System;
using System.Collections.Generic;
using NUnit.Framework;
using Rover.Pluto.Core.Enums;
using Rover.Pluto.Core.Impl;

namespace Rover.Pluto.Test
{
    [TestFixture]
    public class RoverMotionTests
    {
        [Test]
        public void Rover_Command_Enum_Throws_Argument_Out_Of_Range_Exception()
        {
            var planet = new Planet(100, 100, new List<Coordinate>());
            var position = new Position(new Coordinate(0, 0), Direction.North);
            var rover = new Core.Impl.Rover(position, planet);

            Assert.Throws<ArgumentOutOfRangeException>(() => rover.Move((Command)5));
        }
    }
}

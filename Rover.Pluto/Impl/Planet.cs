using System;
using System.Collections.Generic;
using Rover.Pluto.Core.Interfaces;

namespace Rover.Pluto.Core.Impl
{
    public class Planet : IPlanet
    {
        public int Width { get; }
        public int Length { get; }
        public ISet<Coordinate> Obstacles { get; }

        public Planet(int width, int length, IEnumerable<Coordinate> obstacles)
        {
            if (obstacles == null)
                throw new ArgumentNullException(nameof(obstacles));

            Width = width;
            Length = length;

            //hashset for faster lookups
            Obstacles = new HashSet<Coordinate>(obstacles);
        }
    }
}
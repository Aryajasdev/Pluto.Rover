using System.Collections.Generic;
using Rover.Pluto.Core.Impl;

namespace Rover.Pluto.Core.Interfaces
{
    public interface IPlanet
    {
        int Width { get; }

        int Length { get; }

        ISet<Coordinate> Obstacles { get; }
    }
}

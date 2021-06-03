using System;
using System.Collections.Generic;
using System.Text;

namespace Rover.Pluto.Core.Impl
{
    public class Coordinate
    {
        // double precision floating-point for geo coordinates 
        public double Latitude { get; }

        public double Longitude { get; }

        public Coordinate(double latitude, double longitude)
        {
            // We do defensive checking here because the spec 
            // specifies only positive attitudes for coordinates
            if (latitude < 0)
                throw new ArgumentException(nameof(latitude));

            if (longitude < 0)
                throw new ArgumentException(nameof(longitude));

            this.Latitude = latitude;
            this.Longitude = longitude;
        }

        public override string ToString()
        {
            return $"{Latitude},{Longitude}";
        }
    }
}

using Rover.Pluto.Core.Enums;

namespace Rover.Pluto.Core.Impl
{
    public class Position
    {
        public Coordinate Coordinate { get; set; }
        public Direction Direction { get; set; }

        public Position(Coordinate coordinate, Direction direction)
        {
            Coordinate = coordinate;
            Direction = direction;
        }

        public override string ToString() => $"{Coordinate.Latitude}, {Coordinate.Longitude}, {Direction}";
    }
}
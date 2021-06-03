using System;
using Rover.Pluto.Core.Enums;
using Rover.Pluto.Core.Interfaces;

namespace Rover.Pluto.Core.Impl
{
    public class Rover : IVehicle
    {
        private Position _currentPosition;
        private Position _previousPosition;
        private bool _collisionDetected;
        private readonly IPlanet _planet;

        /// <param name="initialPosition">The position the rover has landed at</param>
        /// <param name="planet">The body on which the rover has landed</param>
        public Rover(Position initialPosition, IPlanet planet)
        {
            _currentPosition = initialPosition;
            this._planet = planet;

            _previousPosition = default;
        }

        public Position CurrentPosition => _currentPosition;
        public Position PreviousPosition => _previousPosition;
        public bool CollisionDetected => _collisionDetected;

        public void Move(Command command)
        {
            _collisionDetected = false;

            switch (command)
            {
                case Command.F:
                    _collisionDetected = MoveForward();
                    break;
                case Command.B:
                    _collisionDetected = MoveBackward();
                    break;
                case Command.L:
                    TurnLeft90Degrees();
                    break;
                case Command.R:
                    TurnRight90Degrees();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(command), command, null);
            }
        }

        /// <summary>
        /// The rover moves back by one grid, maintaining the same direction
        /// Depending by the direction it faces it may move on the latitude or longitude axis
        /// </summary>
        private bool MoveForward()
        {
            var newLatitude = _currentPosition.Coordinate.Latitude;
            var newLongitude = _currentPosition.Coordinate.Longitude;

            switch (_currentPosition.Direction)
            {
                case Direction.North:
                    newLongitude = IncrementLongitudeWithWrapping();
                    break;
                case Direction.West:
                    newLatitude = IncrementLatitudeWithWrapping();
                    break;
                case Direction.South:
                    newLongitude = DecrementLongitudeWithWrapping();
                    break;
                case Direction.East:
                    newLatitude = DecrementLatitudeWithWrapping();
                    break;
                default:
                    newLatitude = _currentPosition.Coordinate.Latitude;
                    newLongitude = _currentPosition.Coordinate.Longitude;
                    break;
            }

            var newPosition = new Position(new Coordinate(newLatitude, newLongitude), _currentPosition.Direction);

            if (_planet.Obstacles.Contains(newPosition.Coordinate))
                return true;

            _previousPosition = _currentPosition;
            _currentPosition = newPosition;

            return false;
        }

        /// <summary>
        /// The rover moves back by one grid, maintaining the same direction
        /// Depending by the direction it faces it may move on the latitude or longitude axis
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private bool MoveBackward()
        {
            var newLatitude = _currentPosition.Coordinate.Latitude;
            var newLongitude = _currentPosition.Coordinate.Longitude;

            switch (_currentPosition.Direction)
            {
                case Direction.North:
                    newLongitude = DecrementLongitudeWithWrapping();
                    break;
                case Direction.West:
                    newLatitude = DecrementLatitudeWithWrapping();
                    break;
                case Direction.South:
                    newLongitude = IncrementLongitudeWithWrapping();
                    break;
                case Direction.East:
                    newLatitude = IncrementLatitudeWithWrapping();
                    break;
                default:
                    newLatitude = _currentPosition.Coordinate.Latitude;
                    newLongitude = _currentPosition.Coordinate.Longitude;
                    break;
            }

            var newPosition = new Position(new Coordinate(newLatitude, newLongitude), _currentPosition.Direction);

            if (_planet.Obstacles.Contains(newPosition.Coordinate))
                return true;

            _previousPosition = _currentPosition;
            _currentPosition = newPosition;

            return false;
        }

        /// <summary>
        /// The rover rotates 90 degrees counter-clockwise, changing direction
        /// longitude/latitude remain the same
        /// </summary>
        /// <example>Direction.North -> Direction.West</example>
        private void TurnLeft90Degrees()
        {
            var newDirection = (int)_currentPosition.Direction + 1 > 3 ? 0 : (int)_currentPosition.Direction + 1;

            _currentPosition = new Position(
                new Coordinate(_currentPosition.Coordinate.Latitude,
                    _currentPosition.Coordinate.Longitude),
                (Direction)newDirection);
        }

        /// <summary>
        /// The rover rotates 90 degrees clockwise, changing direction
        /// longitude/latitude remain the same
        /// </summary>
        /// <example>Direction.North -> Direction.EAST</example>
        private void TurnRight90Degrees()
        {
            var newDirection = (int)_currentPosition.Direction - 1 < 0 ? 3 : (int)_currentPosition.Direction - 1;

            _currentPosition = new Position(
                new Coordinate(_currentPosition.Coordinate.Latitude,
                    _currentPosition.Coordinate.Longitude),
                (Direction)newDirection);
        }


        private double IncrementLongitudeWithWrapping() =>
            _currentPosition.Coordinate.Longitude + 1 > _planet.Width
                ? 0
                : _currentPosition.Coordinate.Longitude + 1;

        private double DecrementLongitudeWithWrapping() =>
            _currentPosition.Coordinate.Longitude - 1 < 0
                ? _planet.Width
                : _currentPosition.Coordinate.Longitude - 1;

        private double IncrementLatitudeWithWrapping() =>
            _currentPosition.Coordinate.Latitude + 1 > _planet.Length
                ? 0
                : _currentPosition.Coordinate.Latitude + 1;

        private double DecrementLatitudeWithWrapping() =>
            _currentPosition.Coordinate.Latitude - 1 < 0
                ? _planet.Length
                : _currentPosition.Coordinate.Latitude - 1;
    }
}
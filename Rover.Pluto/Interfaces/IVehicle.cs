using Rover.Pluto.Core.Enums;
using Rover.Pluto.Core.Impl;

namespace Rover.Pluto.Core.Interfaces
{
    public interface IVehicle
    {
        Position CurrentPosition { get; }
        Position PreviousPosition { get; }
        bool CollisionDetected { get; }
        void Move(Command command);
    }
}
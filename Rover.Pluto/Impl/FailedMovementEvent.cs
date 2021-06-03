using Rover.Pluto.Core.Enums;

namespace Rover.Pluto.Core.Impl
{
    public class FailedMovementEvent : DomainEvent
    {
        public Position CurrentPosition { get; set; }
        public ReasonOfFailure ReasonOfFailure { get; set; }
    }
}
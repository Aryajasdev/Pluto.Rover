namespace Rover.Pluto.Core.Impl
{
    public class SuccessfulMovementEvent : DomainEvent
    {
        public Position PreviousPosition { get; set; }
        public Position NewPosition { get; set; }
    }
}
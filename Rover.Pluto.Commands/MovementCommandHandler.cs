using System.Threading;
using System.Threading.Tasks;
using Rover.Pluto.Core.Enums;
using Rover.Pluto.Core.Impl;

namespace Rover.Pluto.Commands
{
    public class MovementCommandHandler
    {
        public Task<MovementResult> Handle(MovementRequest request, CancellationToken cancellationToken)
        {
            var vehicle = request.Vehicle;
            MovementResult result = new MovementResult();

            foreach (var command in request.CommandQueue)
            {
                vehicle.Move(command);

                if (vehicle.CollisionDetected)
                {
                    result = new MovementResult()
                    {
                        Position = request.Vehicle.CurrentPosition,
                        ReasonOfFailure = ReasonOfFailure.ObstacleDectected
                    };
                    // can be routed to subscribers
                    new FailedMovementEvent()
                    {
                        CurrentPosition = request.Vehicle.CurrentPosition,
                        ReasonOfFailure = ReasonOfFailure.ObstacleDectected
                    };

                    break;
                }

                result = new MovementResult() { Position = request.Vehicle.CurrentPosition };

                // can be routed to subscribers
                new SuccessfulMovementEvent()
                {
                    PreviousPosition = request.Vehicle.PreviousPosition,
                    NewPosition = request.Vehicle.CurrentPosition,
                };
            }

            // could do more stuff here like returning a JourneySuccessfulEvent
            // we would need a journey abstraction with startPosition and endPosition
            return Task.FromResult(result);
        }
    }
}
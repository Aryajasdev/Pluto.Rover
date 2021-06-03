using Rover.Pluto.Core.Enums;
using Rover.Pluto.Core.Impl;

namespace Rover.Pluto.Commands
{
    public class MovementResult
    {
        public Position Position { get; set; }
        public ReasonOfFailure ReasonOfFailure { get; set; }
    }
}

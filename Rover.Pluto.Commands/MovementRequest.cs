using System.Collections.Generic;
using Rover.Pluto.Core.Enums;
using Rover.Pluto.Core.Interfaces;


namespace Rover.Pluto.Commands
{
    public class MovementRequest 
    {
        public IVehicle Vehicle { get; set; }
        public Queue<Command> CommandQueue { get; set; }
    }
}
using System;

namespace Rover.Pluto.Core.Impl
{
    public abstract class DomainEvent
    {
        protected DateTimeOffset TimeStamp { get; set; } = DateTimeOffset.UtcNow;
    }
}
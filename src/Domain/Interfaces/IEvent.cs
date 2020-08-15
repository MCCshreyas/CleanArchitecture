using System;

namespace CleanArchitecture.Domain.Interfaces
{
    /// <summary>
    /// Domain event should inherit <see cref="Event"/> class
    /// </summary>
    public abstract class Event
    {
        /// <summary>
        /// TimeStamp when event created
        /// </summary>
        private DateTimeOffset EventRaisedTimeStamp { get; }

        protected Event()
        {
            EventRaisedTimeStamp = DateTimeOffset.UtcNow;
        }
    }
}
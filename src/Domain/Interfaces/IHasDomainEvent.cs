using System.Collections.Generic;

namespace CleanArchitecture.Domain.Interfaces
{
    /// <summary>
    /// Represents the given domain has some events 
    /// </summary>
    public interface IHasDomainEvent
    {
        /// <summary>
        /// List holding the events of given domain 
        /// </summary>
        List<IEvent> DomainEventStore { get; set; }
    }
}

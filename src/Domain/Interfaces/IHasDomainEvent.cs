using System.Collections.Generic;

namespace CleanArchitecture.Domain.Interfaces
{
    /// <summary>
    /// Implemented by Domain entity who can have domain events 
    /// </summary>
    public interface IHasDomainEvent
    {
        /// <summary>
        /// List holding the events 
        /// </summary>
        public List<Event> DomainEvents { get; set; }
    }
}
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Interfaces
{
    /// <summary>
    /// Represents a domain event.
    /// </summary>
    /// <remarks>
    /// Every domain events should implement the <see cref="IEvent"/> interface 
    /// </remarks>
    public interface IEvent
    {
        /// <summary>
        /// Get called automatically when Event gets fired.
        /// </summary>
        public Task HandleAsync();
    }
}

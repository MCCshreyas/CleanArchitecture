using System.Threading.Tasks;
using CleanArchitecture.Domain.Interfaces;

namespace CleanArchitecture.Application.Common.Interfaces
{
    /// <summary>
    /// Abstraction for event publisher
    /// </summary>
    public interface IDomainEventPublisher
    {
        Task Publish(Event @event);
    }
}
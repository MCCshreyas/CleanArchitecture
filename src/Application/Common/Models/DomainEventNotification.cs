using CleanArchitecture.Domain.Interfaces;
using MediatR;

namespace CleanArchitecture.Application.Common.Models
{
    /// <summary>
    /// Generic <see cref="INotification"/> for <see cref="Event"/>
    /// Reference - https://cfrenzel.com/domain-events-efcore-mediatr/
    /// </summary>
    /// <typeparam name="TDomainEvent"></typeparam>
    public class DomainEventNotification<TDomainEvent> : INotification where TDomainEvent : Event
    {
        public TDomainEvent DomainEvent { get; }

        public DomainEventNotification(TDomainEvent @event)
        {
            DomainEvent = @event;
        }
    }
}
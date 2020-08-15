using System;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Infrastructure.EventPublisher
{
    /// <summary>
    /// Publishes the given event using mediatr
    /// Reference - https://cfrenzel.com/domain-events-efcore-mediatr/
    /// </summary>
    public class DomainEventPublisher : IDomainEventPublisher
    {
        private readonly ILogger<DomainEventPublisher> _logger;
        private readonly IMediator _mediator;

        public DomainEventPublisher(ILogger<DomainEventPublisher> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
        
        public async Task Publish(Event @event)
        {
            _logger.LogInformation("Publishing domain event. Event - {event}", @event.GetType().Name);
            await _mediator.Publish(GetNotificationCorrespondingToDomainEvent(@event));
        }

        private INotification GetNotificationCorrespondingToDomainEvent(Event @event)
        {
            return (INotification) Activator.CreateInstance(
                typeof(DomainEventNotification<>).MakeGenericType(@event.GetType()), @event);
        }
    }
}
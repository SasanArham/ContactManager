using Domain.Modules.ContactManagement.People.Events;
using MassTransit;
using MediatR;
using IntegrationPersonDeletedEvent = Messages.Events.Modules.Contactmanagement.People.PersonDeletedEvent;

namespace Application.Modules.ContactManagement.People.DomainEventHandlers
{
    public class PersonDeletedEvent_CacheUpdate : INotificationHandler<PersonDeletedEvent>
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public PersonDeletedEvent_CacheUpdate(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task Handle(PersonDeletedEvent notification, CancellationToken cancellationToken)
        {
            var integrationEvent = new IntegrationPersonDeletedEvent
            {
                Guid = notification.Guid
            };
            await _publishEndpoint.Publish(integrationEvent);
        }
    }
}

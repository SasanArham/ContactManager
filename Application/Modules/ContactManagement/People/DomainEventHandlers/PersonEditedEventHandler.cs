using Domain.Modules.ContactManagement.People.Events;
using MassTransit;
using MediatR;
using IntegrationPersonEditedEvent = Messages.Events.Modules.Contactmanagement.People.PersonEditedEvent;

namespace Application.Modules.ContactManagement.People.DomainEventHandlers
{
    public class PersonEditedEventHandler : INotificationHandler<PersonEditedEvent>
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public PersonEditedEventHandler(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task Handle(PersonEditedEvent notification, CancellationToken cancellationToken)
        {
            var integrationEvent = new IntegrationPersonEditedEvent
            {
                Guid = notification.Guid
            };
            await _publishEndpoint.Publish(integrationEvent);
        }
    }
}

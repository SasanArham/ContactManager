using MassTransit;
using MediatR;
using PersonCreatedEvent = Domain.Modules.ContactManagement.People.Events.PersonCreatedEvent;
using IntegrationPersonCreatedEvent = Messages.Events.Modules.Contactmanagement.People.PersonCreatedEvent;
using Messages.Events.Modules.Contactmanagement.People;

namespace Application.Modules.ContactManagement.People.DomainEventHandlers
{
    public class PersonCreatedEventHandler : INotificationHandler<PersonCreatedEvent>
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public PersonCreatedEventHandler(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task Handle(PersonCreatedEvent notif, CancellationToken cancellationToken)
        {

            var integrationEvent = new IntegrationPersonCreatedEvent
            {
                Guid = notif.Guid,
                Name = notif.Name,
                LastName = notif.LastName,
                Gendr = (int?)notif.Gendr,
                NationalCode = notif.NationalCode,
                Mobiles = notif.Mobiles.Select(c => c.Number),
                Phones = notif.Phones.Select(c => c.Number),
                Faxes = notif.Faxes.Select(c => c.Number),
                Addresses = notif.Addresses.Select(c => new Address
                {
                    CityID = c.CityID,
                    Detail = c.Details
                })
            };
            await _publishEndpoint.Publish(integrationEvent);
        }
    }
}

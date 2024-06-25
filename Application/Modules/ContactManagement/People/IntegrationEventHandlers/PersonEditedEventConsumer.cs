using MassTransit;
using Messages.Events.Modules.Contactmanagement.People;

namespace Application.Modules.ContactManagement.People.IntegrationEventHandlers
{
    public class PersonEditedEventConsumer : IConsumer<PersonEditedEvent>
    {
        public Task Consume(ConsumeContext<PersonEditedEvent> context)
        {
            throw new NotImplementedException();
        }
    }
}

using MassTransit;
using Messages.Events.Modules.Contactmanagement.People;

namespace Application.Modules.ContactManagement.People.IntegrationEventHandlers
{
    public class PersonDeletedEventConsumer : IConsumer<PersonDeletedEvent>
    {
        public Task Consume(ConsumeContext<PersonDeletedEvent> context)
        {
            throw new NotImplementedException();

        }
    }
}

using Application.Common;
using Application.Modules.ContactManagement.People.Helpers;
using MassTransit;
using Messages.Events.Modules.Contactmanagement.People;

namespace Application.Modules.ContactManagement.People.IntegrationEventHandlers
{
    public class PersonCreatedEventConsumer : IConsumer<PersonCreatedEvent>
    {
        private readonly IDistributedCacheProvider _distributedCachProvider;

        public PersonCreatedEventConsumer(IDistributedCacheProvider distributedCachProvider)
        {
            _distributedCachProvider = distributedCachProvider;
        }

        public async Task Consume(ConsumeContext<PersonCreatedEvent> context)
        {
            await _distributedCachProvider.InvalidateAsync(PersonCacheHelper.DefaultList.Key);
        }
    }
}

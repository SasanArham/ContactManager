using Application.Common;
using Application.Modules.ContactManagement.People.Helpers;
using Application.Modules.ContactManagement.People.Queries.GetPeople;
using MassTransit;
using Messages.Events.Modules.Contactmanagement.People;

namespace Application.Modules.ContactManagement.People.IntegrationEventHandlers
{
    public class PersonDeletedEventConsumer : IConsumer<PersonDeletedEvent>
    {
        private readonly IDistributedCacheProvider _distributedCacheProvider;

        public PersonDeletedEventConsumer(IDistributedCacheProvider distributedCacheProvider)
        {
            _distributedCacheProvider = distributedCacheProvider;
        }

        public async Task Consume(ConsumeContext<PersonDeletedEvent> context)
        {
            var cachedPeople = await _distributedCacheProvider.GetAsync<List<GetPeopleResponse>>(PersonCacheHelper.DefaultList.Key);
            if (cachedPeople is not null && cachedPeople.Any(c => c.Guid == context.Message.Guid))
            {
                await _distributedCacheProvider.InvalidateAsync(PersonCacheHelper.DefaultList.Key);
            }
        }
    }
}

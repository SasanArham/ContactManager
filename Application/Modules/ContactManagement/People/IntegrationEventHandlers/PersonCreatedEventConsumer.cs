using Application.Common;
using Application.Modules.ContactManagement.People.Helpers;
using Application.Modules.ContactManagement.People.Queries.GetPeople;
using AutoMapper;
using MassTransit;
using Messages.Events.Modules.Contactmanagement.People;

namespace Application.Modules.ContactManagement.People.IntegrationEventHandlers
{
    public class PersonCreatedEventConsumer : IConsumer<PersonCreatedEvent>
    {
        private readonly IDistributedCacheProvider _distributedCachProvider;
        private readonly IMapper _mapper;

        public PersonCreatedEventConsumer(IDistributedCacheProvider distributedCachProvider
            , IMapper mapper)
        {
            _distributedCachProvider = distributedCachProvider;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<PersonCreatedEvent> context)
        {
            //ToDo
            //var personToBeCached = _mapper.Map<GetPeopleResponse>(context.Message);
            //await _distributedCachProvider.CacheInSetAsync(PersonCacheHelper.DefaultList.Key, personToBeCached, createdPerson.ID);
            //var cachedLen = await _distributedCachProvider.SetLenghtAsync(PersonCacheHelper.DefaultList.Key);
            //if (cachedLen > PersonCacheHelper.DefaultList.MaxLen)
            //{
            //    await _distributedCachProvider.PopFromSetAsync<GetPeopleResponse>(PersonCacheHelper.DefaultList.Key);
            //}
        }
    }
}

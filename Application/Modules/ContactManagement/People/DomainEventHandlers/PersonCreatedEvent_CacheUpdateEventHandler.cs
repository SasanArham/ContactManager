using Application.Common;
using Application.Modules.ContactManagement.People.Helpers;
using Application.Modules.ContactManagement.People.Queries.GetPeople;
using AutoMapper;
using Domain.Modules.ContactManagement.People.Events;
using Domain.Modules.ContactManagement.People.Services;
using MediatR;

namespace Application.Modules.ContactManagement.People.DomainEventHandlers
{
    public class PersonCreatedEvent_CacheUpdateEventHandler : INotificationHandler<PersonCreatedEvent>
    {
        private readonly IDistributedCacheProvider _distributedCachProvider;
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;

        public PersonCreatedEvent_CacheUpdateEventHandler(IDistributedCacheProvider distributedCachProvider
            , IPersonRepository personRepository
            , IMapper mapper)
        {
            _distributedCachProvider = distributedCachProvider;
            _personRepository = personRepository;
            _mapper = mapper;
        }

        public async Task Handle(PersonCreatedEvent notification, CancellationToken cancellationToken)
        {
            var createdPerson = await _personRepository.GetByGuidAsync(notification.GuiD);
            var personToBeCached = _mapper.Map<GetPeopleResponse>(createdPerson);
            await _distributedCachProvider.CacheInSetAsync(PersonCacheHelper.DefaultList.Key, personToBeCached, createdPerson.ID);
            var cachedLen = await _distributedCachProvider.SetLenghtAsync(PersonCacheHelper.DefaultList.Key);
            if (cachedLen > PersonCacheHelper.DefaultList.MaxLen)
            {
                await _distributedCachProvider.PopFromSetAsync<GetPeopleResponse>(PersonCacheHelper.DefaultList.Key);
            }

        }
    }
}

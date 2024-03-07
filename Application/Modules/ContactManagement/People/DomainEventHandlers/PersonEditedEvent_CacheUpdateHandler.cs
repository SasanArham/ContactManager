using Application.Common;
using Application.Modules.ContactManagement.People.Helpers;
using Application.Modules.ContactManagement.People.Queries.GetPeople;
using AutoMapper;
using Domain.Modules.ContactManagement.People.Events;
using Domain.Modules.ContactManagement.People.Services;
using MediatR;

namespace Application.Modules.ContactManagement.People.DomainEventHandlers
{
    public class PersonEditedEvent_CacheUpdateHandler : INotificationHandler<PersonEditedEvent>
    {
        private readonly IDistributedCacheProvider _distributedCachProvider;
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;

        public PersonEditedEvent_CacheUpdateHandler(IDistributedCacheProvider distributedCachProvider
            , IPersonRepository personRepository
            , IMapper mapper)
        {
            _distributedCachProvider = distributedCachProvider;
            _personRepository = personRepository;
            _mapper = mapper;
        }

        public async Task Handle(PersonEditedEvent notification, CancellationToken cancellationToken)
        {
            await _distributedCachProvider.InvalidateAsync(PersonCacheHelper.Person(notification.ID), cancellationToken);
            await UpdateDefaultPeopleListCacheAsync(notification.ID);
        }

        private async Task UpdateDefaultPeopleListCacheAsync(int personID)
        {
            var person = await _personRepository.GetByIDAsync(personID);
            var invalidatedCount = await _distributedCachProvider.InvalidateFromSetByIDAsync(PersonCacheHelper.DefaultList.Key, personID);
            if (invalidatedCount != 0)
            {
                var personToCache = _mapper.Map<GetPeopleResponse>(person);
                await _distributedCachProvider.CacheInSetAsync(PersonCacheHelper.DefaultList.Key, personToCache, personToCache.ID);
            }
        }
    }
}

using Application.Common;
using Application.Modules.ContactManagement.People.Helpers;
using Application.Modules.ContactManagement.People.Queries.GetPeople;
using AutoMapper;
using Domain.Modules.ContactManagement.People.Events;
using Domain.Modules.ContactManagement.People.Services;
using MediatR;

namespace Application.Modules.ContactManagement.People.DomainEventHandlers
{
    public class PersonDeletedEvent_CacheUpdate : INotificationHandler<PersonDeletedEvent>
    {
        private readonly IDistributedCacheProvider _distributedCachProvider;
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;

        public PersonDeletedEvent_CacheUpdate(IDistributedCacheProvider distributedCachProvider
            , IPersonRepository personRepository
            , IMapper mapper)
        {
            _distributedCachProvider = distributedCachProvider;
            _personRepository = personRepository;
            _mapper = mapper;
        }

        public async Task Handle(PersonDeletedEvent notification, CancellationToken cancellationToken)
        {
            await _distributedCachProvider.InvalidateAsync(PersonCacheHelper.Person(notification.ID), cancellationToken);
            await UpdateDefaultPeopleListCacheAsync(notification.ID);
        }

        private async Task UpdateDefaultPeopleListCacheAsync(int personID)
        {
            var invalidatedCount = await _distributedCachProvider.InvalidateFromSetByIDAsync(PersonCacheHelper.DefaultList.Key, personID);
            if (invalidatedCount != 0)
            {
                var personToBeAddedToDefaultPeopleList = (await _personRepository.GetListAsync(PersonCacheHelper.DefaultList.MaxLen - 1, 1)).FirstOrDefault();
                if (personToBeAddedToDefaultPeopleList is not null)
                {
                    var personToCache = _mapper.Map<GetPeopleResponse>(personToBeAddedToDefaultPeopleList);
                    await _distributedCachProvider.CacheInSetAsync(PersonCacheHelper.DefaultList.Key, personToBeAddedToDefaultPeopleList
                        , personToBeAddedToDefaultPeopleList.ID);
                }
            }
        }
    }
}

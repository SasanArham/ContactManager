using Application.Common;
using Application.Modules.ContactManagement.People.Helpers;
using Domain.Modules.ContactManagement.People.Events;
using MediatR;

namespace Application.Modules.ContactManagement.People.DomainEventHandlers
{
    public class PersonDeletedEvent_CacheInvalidationHandler : INotificationHandler<PersonDeletedEvent>
    {
        private readonly IDistributedCacheProvider _distributedCachProvider;

        public PersonDeletedEvent_CacheInvalidationHandler(IDistributedCacheProvider distributedCachProvider)
        {
            _distributedCachProvider = distributedCachProvider;
        }
        public async Task Handle(PersonDeletedEvent notification, CancellationToken cancellationToken)
        {
            var cachkey = PersonCacheKey.Person(notification.ID);
            await _distributedCachProvider.InvalidateAsync(cachkey, cancellationToken);
        }
    }
}

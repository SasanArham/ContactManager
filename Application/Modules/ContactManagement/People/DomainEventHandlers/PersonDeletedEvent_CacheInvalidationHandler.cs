using Application.Common;
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
            var cachkey = $"person-{notification.ID}";
            await _distributedCachProvider.InvalidateAsync(cachkey, cancellationToken);
        }
    }
}

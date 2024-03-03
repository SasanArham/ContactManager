using Application.Common;
using Domain.Modules.ContactManagement.People.Events;
using MediatR;

namespace Application.Modules.ContactManagement.People.DomainEventHandlers
{
    public class PersonEditedEvent_CacheInvalidationHandler : INotificationHandler<PersonEditedEvent>
    {
        private readonly IDistributedCacheProvider _distributedCachProvider;

        public PersonEditedEvent_CacheInvalidationHandler(IDistributedCacheProvider distributedCachProvider)
        {
            _distributedCachProvider = distributedCachProvider;
        }

        public async Task Handle(PersonEditedEvent notification, CancellationToken cancellationToken)
        {
            var cachkey = $"person-{notification.ID}";
            await _distributedCachProvider.InvalidateAsync(cachkey, cancellationToken);
        }
    }
}

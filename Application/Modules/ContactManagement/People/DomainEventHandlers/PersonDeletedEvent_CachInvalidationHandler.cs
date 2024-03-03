using Application.Common;
using Domain.Modules.ContactManagement.People.Events;
using MediatR;

namespace Application.Modules.ContactManagement.People.DomainEventHandlers
{
    public class PersonDeletedEvent_CachInvalidationHandler : INotificationHandler<PersonDeletedEvent>
    {
        private readonly IDistributedCachProvider _distributedCachProvider;

        public PersonDeletedEvent_CachInvalidationHandler(IDistributedCachProvider distributedCachProvider)
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

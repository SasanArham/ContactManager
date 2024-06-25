using Domain.Base;

namespace Domain.Modules.ContactManagement.People.Events
{
    public record PersonDeletedEvent : BaseEvent
    {
        public string Guid { get; init; }
    }
}

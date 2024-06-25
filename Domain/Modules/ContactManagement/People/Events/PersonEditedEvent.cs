using Domain.Base;

namespace Domain.Modules.ContactManagement.People.Events
{
    public record PersonEditedEvent : BaseEvent
    {
        public string Guid { get; init; }
    }
}

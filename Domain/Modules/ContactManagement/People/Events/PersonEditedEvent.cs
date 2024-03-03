using Domain.Base;

namespace Domain.Modules.ContactManagement.People.Events
{
    public record PersonEditedEvent : BaseEvent
    {
        public int ID { get; init; }
    }
}

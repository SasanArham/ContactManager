using Domain.Base;

namespace Domain.Modules.ContactManagement.People.Events
{
    public record PersonDeletedEvent : BaseEvent
    {
        public int ID { get; init; }
    }
}

using Domain.Base;

namespace Domain.Modules.ContactManagement.People.Events
{
    public sealed record PersonCreatedEvent : BaseEvent
    {
        public string GuiD { get; }

        public PersonCreatedEvent(string guid)
        {
            GuiD = guid;
        }
    }
}

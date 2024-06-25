namespace Messages.Events.Modules.Contactmanagement.People
{
    public record PersonEditedEvent
    {
        public string Guid { get; init; }
    }
}

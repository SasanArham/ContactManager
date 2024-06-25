namespace Messages.Events.Modules.Contactmanagement.People
{
    public record PersonDeletedEvent
    {
        public string Guid { get; init; }
    }
}

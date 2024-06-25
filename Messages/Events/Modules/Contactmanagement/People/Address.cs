namespace Messages.Events.Modules.Contactmanagement.People
{
    public record Address
    {
        public int? CityID { get; init; }
        public string Detail { get; init; } = string.Empty;
    }
}

namespace Messages.Events.Modules.Contactmanagement.People
{
    public record PersonCreatedEvent
    {
        public string Guid { get; init; }
        public string NationalCode { get; init; }
        public string Name { get; init; }
        public string LastName { get; init; }
        public int? Gendr { get; init; }
        public IEnumerable<string> Mobiles { get; init; } = new List<string>();
        public IEnumerable<string> Phones { get; init; } = new List<string>();
        public IEnumerable<string> Faxes { get; init; } = new List<string>();
        public IEnumerable<Address> Addresses { get; init; } = new List<Address>();
    }
}

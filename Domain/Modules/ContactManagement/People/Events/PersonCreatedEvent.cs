using Domain.Base;
using Domain.Modules.Shared;

namespace Domain.Modules.ContactManagement.People.Events
{
    public record PersonCreatedEvent : BaseEvent
    {
        public string Guid { get; init; }
        public string NationalCode { get; init; }
        public string Name { get; init; }
        public string LastName { get; init; }
        public Gender? Gendr { get; init; }
        public IEnumerable<PhoneNumber> Mobiles { get; init; } = new List<PhoneNumber>();
        public IEnumerable<PhoneNumber> Phones { get; init; } = new List<PhoneNumber>();
        public IEnumerable<PhoneNumber> Faxes { get; init; } = new List<PhoneNumber>();
        public IEnumerable<Adress> Addresses { get; init; } = new List<Adress>();
    }
}

using Application.Base;
using Application.Modules.ContactManagement.Common;
using Domain.Modules.ContactManagement.People;
using Domain.Modules.ContactManagement.People.Builders;
using Domain.Modules.ContactManagement.People.Services;
using MediatR;

namespace Application.Modules.ContactManagement.People.Commands.CreatePerson
{
    public class CreatePersonCommand : IRequest<int>
    {
        public string Name { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;
        public Gender? Gender { get; init; }
        public List<string> Mobiles { get; init; } = new();
        public List<string> Phones { get; init; } = new();
        public List<string> Webs { get; init; } = new();
        public List<string> Faxes { get; init; } = new();
        public List<NewAddressDto> Addresses { get; init; } = new();
        public string NationalCode { get; init; } = string.Empty;
    }

    public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, int>
    {
        private readonly IPersonRepository _personRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPersonBuilder _personBuilder;

        public CreatePersonCommandHandler(IPersonRepository personRepository
            , IUnitOfWork unitOfWork
            , IPersonBuilder personBuilder)
        {
            _personRepository = personRepository;
            _unitOfWork = unitOfWork;
            _personBuilder = personBuilder;
        }

        public async Task<int> Handle(CreatePersonCommand command, CancellationToken cancellationToken)
        {
            var person = BuildPerson(command);
            _personRepository.Add(person);
            await _unitOfWork.SaveChangesAsync();
            return person.ID;
        }

        private Person BuildPerson(CreatePersonCommand command)
        {
            _personBuilder
                .Reset()
                .SetNationalCode(command.NationalCode)
                .SetName(command.Name)
                .SetLastName(command.LastName)
                .SetGender(command.Gender)
                .SetMoiles(command.Mobiles)
                .SetPhones(command.Phones)
                .SetFaxes(command.Faxes);

            if (command.Addresses is not null)
            {
                if (command.Addresses.Any())
                {
                    _personBuilder.AddAddresses(command.Addresses[0].CityID, command.Addresses[0].Address);
                }
                for (int i = 1; i < command.Addresses.Count; i++)
                {
                    _personBuilder.AddAddresses(command.Addresses[i].CityID, command.Addresses[i].Address);
                }
            }

            var person = _personBuilder.Get();
            return person;
        }
    }
}

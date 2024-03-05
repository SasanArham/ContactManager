using Domain.Modules.ContactManagement.People;
using MediatR;
using Application.Modules.ContactManagement.Common;
using Domain.Modules.Shared;
using Application.Base;
using Domain.Modules.ContactManagement.People.Services;

namespace Application.Modules.ContactManagement.People.Commands.EditPerson
{
    public class EditPersonCommand : IRequest<Unit>
    {
        public int ID { get; init; }
        public string Name { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;
        public Gender? Gender { get; init; }
        public string NationalCode { get; init; } = string.Empty;
        public List<NewPhoneNumberDto> AddedPhoneNumbers { get; init; } = new();
        public List<NewAddressDto> AddedAddresses { get; init; } = new();
        public int? EducationDegreeID { get; set; }
    }

    public class EditPersonCommandHandler : IRequestHandler<EditPersonCommand, Unit>
    {
        private readonly IPersonRepository _personRepository;
        private readonly int CurrentUserID;
        private readonly IUnitOfWork _unitOfWork;

        public EditPersonCommandHandler(IPersonRepository personRepository
            , IUnitOfWork unitOfWork)
        {
            _personRepository = personRepository;
            CurrentUserID = 1;// ToDo
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(EditPersonCommand qm, CancellationToken cancellationToken)
        {
            var person = await _personRepository.GetByIDAsync(qm.ID);
            person.Edit(qm.Name, qm.LastName, qm.NationalCode, qm.Gender);
            person.EditEducation(qm.EducationDegreeID);

            foreach (var phoneNumber in qm.AddedPhoneNumbers.Where(c => c.Type == PhoneNumberType.phone))
            {
                person.AddPhoneNumber(phoneNumber.Number);
            }
            foreach (var phoneNumber in qm.AddedPhoneNumbers.Where(c => c.Type == PhoneNumberType.mobile))
            {
                person.AddMobile(phoneNumber.Number);
            }
            foreach (var address in qm.AddedAddresses)
            {
                person.AddAddress(CurrentUserID, address.CityID, address.Address, address.PostalCode);
            }
            await _unitOfWork.SaveChangesAsync();
            return Unit.Value;
        }
    }
}

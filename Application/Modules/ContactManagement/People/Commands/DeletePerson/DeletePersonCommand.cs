using Application.Base;
using Domain.Modules.ContactManagement.People.Services;
using MediatR;

namespace Application.Modules.ContactManagement.People.Commands.DeletePerson
{
    public class DeletePersonCommand : IRequest<Unit>
    {
        public int ID { get; set; }
    }

    public class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand, Unit>
    {
        private readonly IPersonRepository _personRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeletePersonCommandHandler(IPersonRepository personRepository
            , IUnitOfWork unitOfWork)
        {
            _personRepository = personRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeletePersonCommand comand, CancellationToken cancellationToken)
        {
            var person = await _personRepository.GetByIDAsync(comand.ID);
            person.Delete();
            await _unitOfWork.SaveChangesAsync();
            return Unit.Value;
        }
    }
}

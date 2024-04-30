using Application.Base;
using Application.Common;
using Application.Modules.ContactManagement.People.Helpers;
using Domain.Modules.ContactManagement.People.Services;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Modules.ContactManagement.People.Commands.AddAttachment
{
    public class AddAttachmentToPersonCommand : IRequest<string>
    {
        public int PersonID { get; set; }
        public IFormFile File { get; set; }
    }

    public class AddAttachmentToPersonCommandHandler : IRequestHandler<AddAttachmentToPersonCommand, string>
    {
        private readonly IFileManager _fileManager;
        private readonly IPersonRepository _personRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddAttachmentToPersonCommandHandler(IFileManager fileManager
            , IPersonRepository personRepository
            , IUnitOfWork unitOfWork)
        {
            _fileManager = fileManager;
            _personRepository = personRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<string> Handle(AddAttachmentToPersonCommand command, CancellationToken cancellationToken)
        {
            var person = await _personRepository.GetByIDAsync(command.PersonID);

            var attachmentUrl = person.GenerateAttachmentUrl(command.File.FileName);
            await _fileManager.Upload(command.File, attachmentUrl);
            var addedAttachment = person.AddAttachment(command.File.FileName, attachmentUrl);
            await _unitOfWork.SaveChangesAsync();
            return addedAttachment;
        }
    }
}

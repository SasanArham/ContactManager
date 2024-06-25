using Application.Base.Exceptions;
using Application.Common;
using Domain.Modules.ContactManagement.People.Services;
using MediatR;

namespace Application.Modules.ContactManagement.People.Queries.GetPersonAttachmentDownloadUrl
{
    public class GetPersonAttachmentDownloadUrlQuery : IRequest<string>
    {
        public string AttachmentID { get; init; } = string.Empty;
        public int PersonID { get; init; }
    }

    public class GetPersonAttachmentDownloadUrlQueryHandler : IRequestHandler<GetPersonAttachmentDownloadUrlQuery, string>
    {
        private readonly IPersonRepository _personRepository;
        private readonly IFileManager _fileManager;

        public GetPersonAttachmentDownloadUrlQueryHandler(IPersonRepository personRepository
            , IFileManager fileManager)
        {
            _personRepository = personRepository;
            _fileManager = fileManager;
        }

        public async Task<string> Handle(GetPersonAttachmentDownloadUrlQuery request, CancellationToken cancellationToken)
        {
            var person = await _personRepository.GetByIDAsync(request.PersonID);
            var attachment = person.FindAttachmentByID(request.AttachmentID) ?? throw new AppLogicException("Person has no such a file");
            string fileUrl = await _fileManager.GetDownloadUrl(attachment.Url);
            return fileUrl;
        }
    }
}
